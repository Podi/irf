using fd64yt_irf_week6.Abstractions;
using fd64yt_irf_week6.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fd64yt_irf_week6
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        

        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory();
            createTimer.Tick += createTimer_Tick;
            conveyorTimer.Tick += conveyorTimer_Tick;
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            toy.Left = -toy.Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _toys)
            {
                ball.MoveToy();
                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition >= 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }
    }
}
