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
    enum origin
    {
        center,
        leftTop,
    }
    public abstract class UIComponent:Drawable
    {
        private static int counter = 0;
        public bool Visible { get; set; }
        public bool Selected { get; set; }
        public bool Active { get; set; }
        private Vector2f position;
        public Vector2f Size { get; set; }
        public string  componentID { get; set; }

        public Vector2f Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        protected Sprite componentSprite;
        public UIComponent(Texture _texture)
        {
            counter++;
            Visible = true;
            Selected = false;
            Active = true;
            componentSprite = new Sprite(_texture);
            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);


        }

        public abstract void Draw(RenderTarget target, RenderStates states);
        public abstract void update();

        public abstract void setPosition(Vector2f _position);
    }
}