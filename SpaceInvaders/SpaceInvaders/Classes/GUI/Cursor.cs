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
        private Sprite cursorSprite;
        public Vector2f scale { get; set; }

        public Cursor(Texture _texture, Vector2f _scale)
        {
            cursorSprite = new Sprite(_texture);
            cursorSprite.Scale = _scale;
            cursorSprite.Origin = new Vector2f(0,0);
            cursorSprite.Position = new Vector2f((float)Mouse.GetPosition().X, (float)Mouse.GetPosition().Y);
        }
        public override void update()
        {

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(cursorSprite,states);
        }

        public void moveCursor(Vector2f mousePosition)
        {
            cursorSprite.Position = mousePosition;
        }
    }
}
