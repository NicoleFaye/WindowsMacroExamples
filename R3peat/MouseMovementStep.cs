using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    class MouseMovementStep
    {
        private ushort DestinationX;
        private ushort DestinationY;
        private int PauseDuration;
        private int Variance;

        public MouseMovementStep(ushort x, ushort y,int newPauseDuration=500,int newVariance=0) {
            this.DestinationX = x;
            this.DestinationY = y;
            this.PauseDuration = newPauseDuration;
            this.Variance = newVariance;
        }
        public ushort GetDesinationX() {
            return this.DestinationX;
        }
        public ushort GetDesinationY() {
            return this.DestinationY;
        }
        public int GetPauseDuration() {
            return this.PauseDuration;
        }
        public int GetVariance() {
            return this.Variance;
        }
        public void SetDesinationX(ushort newX) {
             this.DestinationX=newX;
        }
        public void SetDesinationY(ushort newY) {
             this.DestinationY=newY;
        }
        public void SetPauseDuration(int newDuration) {
             this.PauseDuration=newDuration;
        }
        public void SetVariance(int newVariance) {
             this.Variance=newVariance;
        }
    }
}
