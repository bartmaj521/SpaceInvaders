using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Classes
{
    class Enemy: GameObject
    {
        public override GameObject update(float deltaTime)
        {
            return this;
        }
    }
}
