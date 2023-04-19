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
        private List<Ball> _balls = new List<Ball>();
        /*
        private Ballfactory _factory;

        public Ballfactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        */

        public Form1()
        {
            InitializeComponent();
        }
    }
}
