using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace R3peat
{
    class MouseMovementBuilder
    {
        private List<MouseMovementStep> Steps;
        private InputSimulator Input;
        public void BuildMouseMovement() {
            this.Steps.Clear();
        }
        public void AddStep(MouseMovementStep newStep) { 
            this.Steps.Add(newStep);
        }
        public MouseMovement GetMouseMovement() {
            return new MouseMovement(this.Input,this.Steps);
        }

        protected MouseMovementBuilder(InputSimulator newInput) {
            this.Input = newInput;
            this.Steps = new List<MouseMovementStep>();
        }

    }
}
