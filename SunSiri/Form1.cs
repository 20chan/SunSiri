using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SunSiri
{
    public partial class Form1 : Form
    {
        private SunSiri _siri;
        public Form1()
        {
            InitializeComponent();
            this.TransparencyKey = BackColor;
            _siri = new SunSiri();
            _siri.Listened += _siri_Listened;
            Speak("안녕하세요, 주인님");
        }

        private void _siri_Listened(string obj)
        {
            switch(obj)
            {
                case "안녕":
                    Speak("안녕하세요");
                    break;
            }
        }

        private Point _point;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _point = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(Left - (_point.X - e.X),
                    Top - (_point.Y - e.Y));
            }
        }

        private void Speak(string text)
        {
            this.toolTip1.Show(text, this, 160, 0, 5000);
            _siri.SpeakAsync(text);
        }
    }
}
