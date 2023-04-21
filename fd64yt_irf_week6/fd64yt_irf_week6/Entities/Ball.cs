using fd64yt_irf_week6.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fd64yt_irf_week6.Entities
{
    public class Ball : Toy
    {
        public SolidBrush BallColor { get; private set; }

        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
        }
        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }
    }
}
