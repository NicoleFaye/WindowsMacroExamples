using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    class MouseMovementBuilder
    {
        private List<MouseMovementStep> Steps;
        public void BuildMouseMovement() {
            this.Steps.Clear();
        }
        public void AddStep(MouseMovementStep newStep) { 
            this.Steps.Add(newStep);
        }
        public MouseMovement GetMouseMovement() {
            return new MouseMovement(this.Steps);
        }

        protected MouseMovementBuilder() { 
            this.Steps = new List<MouseMovementStep>();
        }

    }
}
