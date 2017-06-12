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
    class Explosion : Projectile
    {
        CircleShape shape;
        private float explosionTime;
        private bool dealDamage;
        private bool damageDealt;

        public Explosion(Vector2f pos)
        {
            damage = 1000;
            shape = new CircleShape(0);
            shape.Position = pos;

            explosionTime = 2f;
            dealDamage = false;
            damageDealt = false;

            collider.Width = 0;
            collider.Height = 0;

            shape.FillColor = new Color(255,0,0,128);
        }

        public override GameObject update(float deltaTime)
        {
            if (damageDealt)
                return null;
            explosionTime -= deltaTime;
            shape.Radius += 50 * deltaTime;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            collider.Left = shape.Position.X - shape.Radius;
            collider.Top = shape.Position.Y - shape.Radius;
            collider.Height = collider.Width = 2 * shape.Radius;
            if (explosionTime <= 0)
                dealDamage = true;
            return this;
        }

        public override GameObject checkCollision(List<IDamageable> colliderList)
        {
            if (dealDamage)
            {
                foreach (var c in colliderList)
                {
                    if (c != null && collider.Intersects(c.getCollider()))
                    {
                        c.getDamaged(damage);
                        ParticleSystem.Instance().enemyHitburst(new Vector2f((c as GameObject).animation.animationSprite.Position.X + (c as GameObject).animation.animationSprite.Scale.X * (c as GameObject).animation.animationSprite.Texture.Size.X / 2, (c as GameObject).animation.animationSprite.Position.Y + (c as GameObject).animation.animationSprite.Scale.Y * (c as GameObject).animation.animationSprite.Texture.Size.Y / 2));
                        return this;
                    }
                }
                damageDealt = true;
            }
            return this;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape);
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

    }
}
