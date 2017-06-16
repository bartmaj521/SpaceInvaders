using System;

using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes
{
    class Animation
    {
        // Sprajt animacji
        public Sprite animationSprite { get; set; }

        // Tablica kolejności klatek animacji
        protected int[] frames;

        // Aktualna klatka animacji
        protected int currentFrame;

        // Czas trwania klatki
        protected float frameTime;

        // Czas trwania aktualnej klatki
        protected float currFrameTime;

        // Konstruktor bezargumentowy
        public Animation()
        {
            animationSprite = new Sprite();
            animationSprite.Position = new Vector2f(-500f,-500f);
            frames = new int[1] { 0 };
        }

        // Kostruktor txt - textura obiektu, _frames tablica kolejności klatek animacji, _frameTime czastrwania klatki,startingPosition - pozycja startowa(lewy górny róg), Scale - skala obiektu
        public Animation(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale)
        {
            animationSprite = new Sprite();
            frames = _frames;
            animationSprite.Position = startingPosition;
            animationSprite.Texture = txt;
            animationSprite.TextureRect = new IntRect(0, 0, Convert.ToInt32(animationSprite.Texture.Size.X), Convert.ToInt32(animationSprite.Texture.Size.Y / frames.Length));
            frameTime = _frameTime;
            animationSprite.Scale = Scale;
            currFrameTime = 0;
            currentFrame = 0;
        }

        // Ustalenie klatki animacji i położenia deltaTime - zmiana czasu, newPos - nowa pozycja
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
