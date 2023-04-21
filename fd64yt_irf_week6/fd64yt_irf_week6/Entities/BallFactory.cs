using fd64yt_irf_week6.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd64yt_irf_week6.Entities
{
    public class BallFactory : IToyFactory
    {
        public Color BallColor { get; set; }

        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }
    }
}
