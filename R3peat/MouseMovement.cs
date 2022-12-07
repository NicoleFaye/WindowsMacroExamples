using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    class MouseMovement : Action
    {

        private List<MouseMovementStep> Steps = new List<MouseMovementStep>();
        public override void run() { 
        }
        public MouseMovement(List<MouseMovementStep> Steps)
        {
            this.Steps = Steps;
        }
    }
}
