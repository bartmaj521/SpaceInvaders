using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SpaceInvaders.Classes
{
    abstract class GameObject
    {
        
        protected Sprite animationSprite { get; set; }
        protected int nbAnimationFrames;
        protected int currentFrame;
        protected float frameTime;
        protected float currFrameTime;

        protected FloatRect collider;

        abstract public void update(float deltaTime);

        public GameObject()
        {

        }

        public GameObject(ref Texture txt, int _nbAnimationFrames, float _frameTime, Vector2f startingPosition, Vector2f Scale)
        {
            nbAnimationFrames = _nbAnimationFrames;

            animationSprite.Texture = txt;
            animationSprite.Position = startingPosition;
            animationSprite.Scale = Scale;
            animationSprite.TextureRect = new IntRect(0, 0, (int)animationSprite.Texture.Size.X, (int)animationSprite.Texture.Size.Y / nbAnimationFrames);

            collider.Left = startingPosition.X;
            collider.Top = startingPosition.Y;    
            collider.Width = animationSprite.Texture.Size.X * Scale.X;
            collider.Height = animationSprite.Texture.Size.Y * Scale.Y;

            frameTime = _frameTime;
        }


        







    }
}
