﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace SpaceInvaders.Classes
{
    interface IDamageable
    {
        void getDamaged(float damage);
        FloatRect getCollider();
    }
}
