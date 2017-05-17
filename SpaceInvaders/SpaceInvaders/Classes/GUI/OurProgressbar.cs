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
    enum ProgressBarStyle
    {
        normal,
        reversed,
    }
    public class OurProgressbar : UIComponent, Drawable
    {
        private float progress;
        private IntRect max;
        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
                if (progress >= 1)
                    progress = 1;
                else if (progress <= 0)
                    progress = 0;
            }
        }
        public OurProgressbar(Texture _texture, Vector2f _size) : base(_texture)
        {
            Progress = 0;
            componentSprite.TextureRect = new IntRect(0, 0, (int)componentSprite.Texture.Size.X/2, (int)componentSprite.Texture.Size.Y);
            max = new IntRect((int)componentSprite.Texture.Size.X / 2, (int)componentSprite.Texture.Size.Y, (int)componentSprite.Texture.Size.X / 2, (int)componentSprite.Texture.Size.Y);
            componentSprite.Scale = new Vector2f(_size.X / _texture.Size.X*2, _size.Y / _texture.Size.Y);
            Size = _size;

        }



        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(componentSprite, states);

        }

        public override void setPosition(Vector2f _position)
        {
            componentSprite.Position = _position;
            Position = _position;
        }

        public override void update()
        {

            componentSprite.TextureRect = new IntRect((int)((1-progress )* max.Left), 0, componentSprite.TextureRect.Width, componentSprite.TextureRect.Height);

        }
    }

}
