﻿using System;
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

                //Account for variance
                Random random=new Random();
                int deltaX = Convert.ToUInt16(random.Next(variance * -1, variance));
                int deltaY = Convert.ToUInt16(random.Next(variance * -1, variance));

                ushort AbsoluteYPixelStepSize = CoordinateConversion.GetAbsoluteYPixelStepSize();
                ushort AbsoluteXPixelStepSize = CoordinateConversion.GetAbsoluteXPixelStepSize();

                //convert deltas from pixels to absolute value
                int finalDeltaX = deltaX * (int)AbsoluteXPixelStepSize;
                int finalDeltaY = deltaY * (int)AbsoluteYPixelStepSize;



                this.Input.Mouse.MoveMouseTo(DestinationAbsoluteX, DestinationAbsoluteY);
                Thread.Sleep(mouseMovementStep.GetPauseMillisecondDuration());
            }
        }
        public MouseMovement(InputSimulator Input, List<MouseMovementStep> Steps)
        {
            this.Input = Input;
            this.MouseMovementSteps = Steps;
        }

        private bool checkForOverflow(ushort startingValue, int delta) {
            bool willOverflow = false;
            if (startingValue > 0 && delta > 0) {
                if(delta>(ushort.MaxValue-startingValue)) willOverflow = true;
            }
            return willOverflow;
        }
    }
}
