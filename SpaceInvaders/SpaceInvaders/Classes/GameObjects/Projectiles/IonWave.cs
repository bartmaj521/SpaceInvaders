using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Interfaces;

using SFML.System;
using SFML.Graphics;

namespace SpaceInvaders.Classes
{
    class IonWave : Projectile
    {
        public float scaling;
        private float timeToDmg;
        private float defaultTime;
        public IonWave(Texture txt, float _damage, Vector2f scale)
        {
            damage = _damage;
            animation = new Animation();

            animation.animationSprite.Texture = txt;
            animation.animationSprite.Scale = scale;

            collider.Width = animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            collider.Height = 1;

            defaultTime = 0.1f;
            timeToDmg = defaultTime;
            scaling = 1f;
        }

        public override GameObject update(float deltaTime)
        {
            if (animation.animationSprite.Position.Y < topBoundary || animation.animationSprite.Position.Y > botBoundary)
                return null;
            animation.animationSprite.Position += velocity * deltaTime;
            animation.animationSprite.Scale += new Vector2f(scaling * deltaTime, scaling * deltaTime);
            collider.Left = animation.animationSprite.Position.X - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            collider.Top = animation.animationSprite.Position.Y;
            collider.Width = animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            timeToDmg -= deltaTime;
            return this;
        }

        public void fix()
        {
            animation.animationSprite.Origin = new Vector2f(animation.animationSprite.Texture.Size.X / 2, animation.animationSprite.Texture.Size.Y / 2);
        }

        public override GameObject checkCollision(List<IDamageable> colliderList)
        {
            if (timeToDmg <= 0)
            {
                timeToDmg = defaultTime;
                foreach (var c in colliderList)
                {
                    if (c != null && collider.Intersects(c.getCollider()))
                    {
                        c.getDamaged(damage);
                        ParticleSystem.Instance().enemyHitburst(new Vector2f((c as GameObject).animation.animationSprite.Position.X + (c as GameObject).animation.animationSprite.Scale.X * (c as GameObject).animation.animationSprite.Texture.Size.X / 2, (c as GameObject).animation.animationSprite.Position.Y + (c as GameObject).animation.animationSprite.Scale.Y * (c as GameObject).animation.animationSprite.Texture.Size.Y / 2));
                        return this;
                    }
                }
                return this;
            }
            return this;
        }

        public override object Clone()
        {
            IonWave p = (IonWave)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite = new Sprite();
            p.animation.animationSprite.Texture = this.animation.animationSprite.Texture;
            p.animation.animationSprite.Scale = animation.animationSprite.Scale;
            return (object)p;
        }

    }
}
