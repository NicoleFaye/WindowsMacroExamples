using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using WindowsInput;

namespace R3peat
{
    class MouseMovement : Action
    {

        private InputSimulator Input;
        private List<MouseMovementStep> MouseMovementSteps;
        public override void run()
        {
            foreach (MouseMovementStep mouseMovementStep in this.MouseMovementSteps)
            {
                ushort DestinationAbsoluteX = mouseMovementStep.GetDestinationAbsoluteX();
                ushort DestinationAbsoluteY = mouseMovementStep.GetDestinationAbsoluteY();


                int variance =mouseMovementStep.GetVariance();
                if(variance>ushort.MaxValue)variance=ushort.MaxValue;
                else if (variance < 0) variance = 0;

                //TODO
                //account for varianceariance
                Random random=new Random();
                ushort deltaX = Convert.ToUInt16(random.Next(variance * -1, variance));
                ushort deltaY = Convert.ToUInt16(random.Next(variance * -1, variance));
                //deltas are in pixels. need to convarianceert pixels to varianceirtual desktop ushort variancealue (should just be a multiplication based off screen size)

                //once accounted for, need to make sure that x and y are in bounds of screen and ushort

                this.Input.Mouse.MoveMouseTo(DestinationAbsoluteX, DestinationAbsoluteY);
                Thread.Sleep(mouseMovementStep.GetPauseMillisecondDuration());
            }
        }
        public MouseMovement(InputSimulator Input, List<MouseMovementStep> Steps)
        {
            this.Input = Input;
            this.MouseMovementSteps = Steps;
        }
    }
}
