using System;
using System.Globalization;

using Microsoft.Speech;
using Microsoft.Speech.Synthesis;
using Microsoft.Speech.Recognition;

namespace SunSiri
{
    public class SunSiri
    {
        public event Action<string> Listened;

        private SpeechRecognitionEngine _recog;
        private SpeechSynthesizer _synth;

        private bool _isReady = false;

        public SunSiri()
        {
            InitRecognition();
            InitSynthesizer();
        }

        private void InitRecognition()
        {
            _recog = new SpeechRecognitionEngine(new CultureInfo("ko-KR"));
            var g = LoadGrammar();
            _recog.LoadGrammar(g);

            _recog.SpeechRecognized += Sre_SpeechRecognized;
            _recog.SetInputToDefaultAudioDevice();
            _recog.RecognizeAsync(RecognizeMode.Multiple);
        }

        private Grammar LoadGrammar()
        {
            return new Grammar("grammar.xml");
        }

        private void InitSynthesizer()
        {
            _synth = new SpeechSynthesizer();
            _synth.SelectVoice("Microsoft Server Speech Text to Speech Voice (ko-KR, Heami)");
            _synth.SetOutputToDefaultAudioDevice();
            _synth.Volume = 100;
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "순실아")
            {
                _isReady = true;
                SpeakAsync("준비되었습니다.");
            }
            else if (_isReady)
            {
                _isReady = false;
                Listened?.Invoke(e.Result.Text);
            }
        }

        public void SpeakAsync(string text)
        {
            _synth.SpeakAsync(text);
        }
    }
}
