using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;


namespace SpaceInvaders.Classes
{
    class Bullet: Projectile
    {       
        public Bullet(ref Texture txt, float _damage , Vector2f scale)
        {
            damage = _damage;
            animation = new Animation();

            animation.animationSprite.Texture = txt;
            animation.animationSprite.Scale = scale;

            collider.Width = animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            collider.Height = animation.animationSprite.Texture.Size.Y * animation.animationSprite.Scale.Y;
        }

        public override GameObject update(float deltaTime)
        {
            if (animation.animationSprite.Position.Y < topBoundary  || animation.animationSprite.Position.Y > botBoundary)
                return null;
            animation.animationSprite.Position += velocity * deltaTime;
            collider.Left = animation.animationSprite.Position.X - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            collider.Top = animation.animationSprite.Position.Y - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            animation.animationSprite.Rotation = (float)(Math.Atan(velocity.X / velocity.Y) * -180 / Math.PI);
            return this;
        }

        override public object Clone()
        {
            Bullet p = (Bullet)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite = new Sprite();
            p.animation.animationSprite.Texture = this.animation.animationSprite.Texture;
            p.animation.animationSprite.Scale = animation.animationSprite.Scale;
            p.velocity = this.velocity;
            return (object)p;
        }
    }
}
