using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    class MouseMovementBuilder
    {
        private List<MouseMovementStep> Steps = new List<MouseMovementStep>();
        public void BuildMouseMovement() {
            Steps.Clear();
        }

        public MouseMovement GetMouseMovement() {
            return new MouseMovement(GetSteps());
        }

        protected MouseMovementBuilder() { }
        private List<MouseMovementStep> GetSteps() { 
            return Steps;
        }

    }
}
