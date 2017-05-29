using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes.GUI
{
    public class OurImage : UIComponent, Drawable
    {
        public OurImage(Texture _texture) : base(_texture)
        {
            componentSprite = new Sprite(_texture);

        }
        public OurImage(Texture _texture,Vector2f _size) : base(_texture)
        {
            componentSprite = new Sprite(_texture);
            componentSprite.Scale = new Vector2f(_size.X / (_texture.Size.X), _size.Y / _texture.Size.Y);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Visible)
            {
                target.Draw(componentSprite, states); 
            }
        }

        public override void setPosition(Vector2f _position)
        {
            componentSprite.Position = _position;
            Position = componentSprite.Position;
        }

        public override void update()
        {
        }
    }
}
