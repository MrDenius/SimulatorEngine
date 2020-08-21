using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine.Logic.Interface
{
    public interface IMovable : IEntity
    {
        Speed Speed { get; set; }

        bool IsFlying { get; set; }

        bool Collision { get; set; }
    }
}
