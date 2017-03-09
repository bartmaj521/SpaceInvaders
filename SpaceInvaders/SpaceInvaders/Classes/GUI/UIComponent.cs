﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SpaceInvaders.Classes.GUI
{
    abstract class UIComponent:Drawable
    {
        public UIComponent()
        {

        }

        public abstract void Draw(RenderTarget target, RenderStates states);
        public abstract void update();
    }
}