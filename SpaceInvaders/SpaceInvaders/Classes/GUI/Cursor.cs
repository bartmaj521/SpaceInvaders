using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceInvaders.Classes.GUI
{
    class Cursor:UIComponent, Drawable
    {
        public Vector2f scale { get; set; }

        #region Singleton constructor
        private static Cursor instance;
        public static Cursor Instance(Texture _texture, Vector2f _scale)
        {
            if(instance==null)
            {
                instance = new Cursor(_texture, _scale);
            }
            return instance;
        }

        private Cursor(Texture _texture, Vector2f _scale):base(_texture)
        {
            componentSprite.Scale = _scale;
            componentSprite.Origin = new Vector2f(0,0);
            componentSprite.Position = new Vector2f((float)Mouse.GetPosition().X, (float)Mouse.GetPosition().Y);
        }
        #endregion

        public override void update()
        {

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(componentSprite, states);
        }

        public void moveCursor(Vector2f mousePosition)
        {
            componentSprite.Position = mousePosition;
        }

        public override void setPosition(Vector2f _position)
        {
        }
    }
}
