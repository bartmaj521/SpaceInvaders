using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SpaceInvaders.Classes;

namespace SpaceInvaders.Classes
{
    abstract class GameObject
    {
        public Animation animation { get; set; }

        protected FloatRect collider;

        abstract public void update(float deltaTime);

        public GameObject()
        {

        }

        public GameObject(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale)
        {
            animation = new Animation(ref txt, _frames, _frameTime, startingPosition, Scale);
            animation.updateAnimation(0, new Vector2f(0,0));

            collider = new FloatRect(startingPosition.X, startingPosition.Y, animation.animationSprite.Texture.Size.X * Scale.X, animation.animationSprite.Texture.Size.Y * Scale.Y);
        }
    }
}
