using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceInvaders.Classes.Enemies;
using SpaceInvaders.Classes;

using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes
{
    class Missile : Projectile
    {
        private Enemy target;

        public Missile(Texture txt, float _damage, Vector2f scale)
        {
            damage = _damage;
            animation = new Animation();

            animation.animationSprite.Texture = txt;
            animation.animationSprite.Scale = scale;

            collider.Width = animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            collider.Height = animation.animationSprite.Texture.Size.Y * animation.animationSprite.Scale.Y;
        }

        private Vector2f getDirVector(Enemy enemy)
        {
            Vector2f tmp = new Vector2f(enemy.animation.animationSprite.Position.X + enemy.animation.animationSprite.Scale.X * enemy.animation.animationSprite.Texture.Size.X / 2, enemy.animation.animationSprite.Position.Y + enemy.animation.animationSprite.Scale.Y * enemy.animation.animationSprite.Texture.Size.Y / 2);
      
            tmp = tmp - animation.animationSprite.Position;

            tmp.X /= (float)Math.Sqrt(tmp.X*tmp.X + tmp.Y*tmp.Y);
            tmp.Y /= (float)Math.Sqrt(tmp.X * tmp.X + tmp.Y * tmp.Y);
            tmp *= speed;

            return tmp;
        }

        private float getDist(Vector2f targetPos)
        {
            Vector2f tmp = new Vector2f();
            tmp = targetPos - animation.animationSprite.Position;
            return (float)Math.Sqrt(tmp.X * tmp.X + tmp.Y * tmp.Y);
        }

        public override GameObject update(float deltaTime)
        {
            if (animation.animationSprite.Position.Y < topBoundary || animation.animationSprite.Position.Y > botBoundary)
                return null;
            if (target != null)
            {
                velocity = getDirVector(target);
            }

            animation.animationSprite.Position += velocity * deltaTime;
            collider.Left = animation.animationSprite.Position.X - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            collider.Top = animation.animationSprite.Position.Y - animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2;
            animation.animationSprite.Rotation = (float)(Math.Atan(velocity.X / velocity.Y) * -180 / Math.PI);
            return this;
        }

        public void setTarget(List<Enemy> enemyList)
        {
            float closestDist = float.MaxValue;
            float tmp;

            foreach (Enemy enemy in enemyList)
            {
                if((tmp = getDist(enemy.animation.animationSprite.Position)) < closestDist)
                {
                    target = enemy;
                    closestDist = tmp;
                }
            }
        }

        override public object Clone()
        {
            Missile p = (Missile)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite = new Sprite();
            p.animation.animationSprite.Texture = animation.animationSprite.Texture;
            p.animation.animationSprite.Scale = animation.animationSprite.Scale;
            p.velocity = velocity;
            return (object)p;
        }
    }
}
