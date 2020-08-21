using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine.Logic
{
    public class Speed
    {
        private float speedX;
        private float speedY;
        private float maxSpeedX;
        private float maxSpeedY;
        private float acceleration;

        public Speed(float speedX, float speedY, float maxSpeedX, float maxSpeedY, float acceleration)
        {
            this.speedX = speedX;
            this.speedY = speedY;
            this.maxSpeedX = maxSpeedX;
            this.maxSpeedY = maxSpeedY;
            this.acceleration = acceleration;
        }

        public float SpeedX
        {
            get => speedX;
            set
            {
                if (Math.Abs(value) <= maxSpeedX)
                    speedX = value;
                else
                    if (value < 0)
                    speedX = -maxSpeedX;
                else
                    speedX = maxSpeedX;
            }
        }
        public float SpeedY
        {
            get => speedY;
            set
            {
                if (Math.Abs(value) <= maxSpeedY)
                    speedY = value;
                else
                    if (value < 0)
                    speedY = -maxSpeedY;
                else
                    speedY = maxSpeedY;
            }
        }
        public float MaxSpeedX { get => maxSpeedX; set => maxSpeedX = value; }
        public float MaxSpeedY { get => maxSpeedY; set => maxSpeedY = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
    }
}
