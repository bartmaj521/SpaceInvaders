using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceInvaders.Classes;
using SpaceInvaders.Interfaces;

using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes
{
    class Bomb : Projectile
    {
        public Bomb(Texture txt, float _damage, Vector2f scale)
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
            if (animation.animationSprite.Position.Y < 100 || animation.animationSprite.Position.Y > botBoundary)
                return new Explosion(animation.animationSprite.Position);
            animation.animationSprite.Position += velocity * deltaTime;
            collider.Left = animation.animationSprite.Position.X - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            collider.Top = animation.animationSprite.Position.Y - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            animation.animationSprite.Rotation = (float)(Math.Atan(velocity.X / velocity.Y) * -180 / Math.PI);
            return this;
        }

        public override GameObject checkCollision(List<IDamageable> colliderList)
        {
            foreach (var c in colliderList)
            {
                if (c != null && collider.Intersects(c.getCollider()))
                {
                    c.getDamaged(damage);
                    ParticleSystem.Instance().enemyHitburst(new Vector2f(animation.animationSprite.Position.X, animation.animationSprite.Position.Y));
                    return new Explosion(animation.animationSprite.Position);
                }
            }
            return this;
        }

        public override object Clone()
        {
            Bomb p = (Bomb)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite = new Sprite();
            p.animation.animationSprite.Texture = this.animation.animationSprite.Texture;
            p.animation.animationSprite.Scale = animation.animationSprite.Scale;
            p.velocity = this.velocity;
            return (object)p;
        }

    }
}
