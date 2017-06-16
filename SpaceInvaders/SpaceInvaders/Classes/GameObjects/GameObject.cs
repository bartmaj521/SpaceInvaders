using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes
{
    abstract class GameObject: Drawable
    {
        // Animacja obiektu
        public Animation animation { get; set; }

        // Hitbox obiektu
        protected FloatRect collider;

        // Abstrakcyjna metoda aktualizująca pozycję obiektu
        abstract public GameObject update(float deltaTime);

        public FloatRect getCollider()
        {
            return collider;
        }

        public void setColliderPosition(Vector2f pos)
        {
            collider.Left = pos.X;
            collider.Top = pos.Y;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(animation.animationSprite);
        }

        // Konstruktor bezargumentowy
        public GameObject()
        {

        }

        // Kostruktor txt - textura obiektu, _frames tablica kolejności klatek animacji, startingPosition - pozycja startowa(lewy górny róg), Scale - skala obiektu
        public GameObject(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale)
        {
            animation = new Animation(ref txt, _frames, _frameTime, startingPosition, Scale);
            animation.updateAnimation(0, new Vector2f(0,0));

            collider = new FloatRect(startingPosition.X, startingPosition.Y, animation.animationSprite.GetGlobalBounds().Width, animation.animationSprite.GetGlobalBounds().Height);
        }
    }
}
