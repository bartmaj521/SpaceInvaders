using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using SpaceInvaders.Interfaces;

namespace SpaceInvaders.Classes
{
    abstract class Projectile : GameObject, ICloneable
    {
        public static float topBoundary { get; set; }
        public static float botBoundary { get; set; }
        public static float leftBoundary { get; set; }
        public static float rightBoundary { get; set; }

        public Vector2f velocity { get; set; }
        protected float damage;

        public override abstract GameObject update(float deltaTime);
        public abstract object Clone();

        public GameObject checkCollision(List<IDamageable> colliderList)
        {
            foreach (var c in colliderList)
            {
                if (c != null && collider.Intersects(c.getCollider()))
                {
                    c.getDamaged(damage);
                    return null;
                }
            }
            return this;
        }
    }
}
