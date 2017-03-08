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
    class Animation
    {
    
        public Sprite animationSprite { get; set; }
        protected int[] frames;
        protected int currentFrame;
        protected float frameTime;
        protected float currFrameTime;

        public Animation(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale)
        {
            animationSprite = new Sprite();
            frames = _frames;
            animationSprite.Texture = txt;
            IntRect llolol = new IntRect(0, 0, Convert.ToInt32(animationSprite.Texture.Size.X), Convert.ToInt32(animationSprite.Texture.Size.Y / frames.Length));
            animationSprite.TextureRect = llolol;
            frameTime = _frameTime;
            animationSprite.Position = startingPosition;
            animationSprite.Scale = Scale;
            currFrameTime = 0;
            currentFrame = 0;

        }
        public void updateAnimation(float deltaTime, Vector2f newPos)
        {
            animationSprite.Position = newPos;
            currFrameTime += deltaTime;
            if(currFrameTime > frameTime)
            {
                currFrameTime = 0;
                currentFrame++;
                if(currentFrame >= frames.Length)
                {
                    currentFrame = 0;
                }
                animationSprite.TextureRect = new IntRect(0, Convert.ToInt32(animationSprite.Texture.Size.Y / frames.Length * frames[currentFrame]), Convert.ToInt32(animationSprite.Texture.Size.X), Convert.ToInt32(animationSprite.Texture.Size.Y / frames.Length));
            }
        }

    }
}
