using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WindowsInput;

namespace R3peat
{
    class MouseMovement : Action
    {

        private InputSimulator Input;
        private List<MouseMovementStep> Steps;
        public override void run()
        {
            foreach (MouseMovementStep s in this.Steps)
            {
                ushort x = s.GetDesinationX();
                ushort y = s.GetDesinationY();
                this.Input.Mouse.MoveMouseTo(x, y);
                Thread.Sleep(s.GetPauseDuration());
            }
        }
        public MouseMovement(InputSimulator newInput, List<MouseMovementStep> newSteps)
        {
            Input = newInput;
            this.Steps = newSteps;
        }
    }
}
