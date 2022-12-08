using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;

namespace R3peat
{
    class Pause: Action
    {
        private int Delay;
        public override void run() {
            Thread.Sleep(this.Delay);
        }
        public Pause(int milliseconds)
        {
            this.Delay= milliseconds;
        }
    }
}
