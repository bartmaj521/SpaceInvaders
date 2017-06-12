using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

using SpaceInvaders.Interfaces;

namespace SpaceInvaders.Classes
{
    class LaserBeam : Projectile
    {
        private float durationLeft;
        public bool damageDealt;

        public LaserBeam(Texture txt, float _damage, float scale)
        {
            damage = _damage;
            animation = new Animation();
            durationLeft = 0.15f;
            damageDealt = false;

            animation.animationSprite.Texture = txt;
            animation.animationSprite.Scale = new Vector2f(scale, 14);

            collider.Width = animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            collider.Height = animation.animationSprite.Texture.Size.Y * animation.animationSprite.Scale.Y;
        }

        public override GameObject update(float deltaTime)
        {
            durationLeft -= deltaTime;
            if (durationLeft < 0)
                return null;
            return this;
        }
        
        public void fix()
        {
            animation.animationSprite.Origin = new Vector2f(0, animation.animationSprite.Texture.Size.Y);
            collider.Left = animation.animationSprite.Position.X;
            collider.Top = 0;
        }

        public override GameObject checkCollision(List<IDamageable> colliderList)
        {
            if (!damageDealt)
            {
                damageDealt = true;
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
            LaserBeam p = (LaserBeam)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite = new Sprite();
            p.animation.animationSprite.Texture = this.animation.animationSprite.Texture;
            p.animation.animationSprite.Scale = animation.animationSprite.Scale;
            return (object)p;
        }
    }
}
