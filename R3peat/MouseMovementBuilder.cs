using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    class MouseMovementBuilder
    {
        public virtual void BuildMouseMovement() { }

        public virtual MouseMovement GetMouseMovement() { return null; }

        protected MouseMovementBuilder() { }

    }
}
