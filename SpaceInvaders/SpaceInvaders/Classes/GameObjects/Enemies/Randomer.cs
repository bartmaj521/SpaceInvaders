using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes.Enemies
{
    class Randomer : Enemy
    {

        public Randomer(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale, float enemySpeed) : base(ref txt, _frames, _frameTime, startingPosition, Scale, enemySpeed)
        {
            float tmp = (float)(rand.NextDouble() * 2 * Math.PI);
            velocity = new Vector2f((float)Math.Sin(tmp) * speed, (float)Math.Cos(tmp) * speed);
            moveTime = (float)rand.NextDouble() * 2 + 1;
        }

        public override GameObject update(float deltaTime)
        {
            if (health <= 0)
                return null;
            if (setting)
            {
                if (collider.Left < leftBoundary)
                    collider.Left += speed * deltaTime;
                else if (collider.Left > rightBoundary - collider.Width)
                    collider.Left -= speed * deltaTime;
                if (collider.Top < 0)
                    collider.Top += speed * deltaTime;
                else if (collider.Top > botBoundary - collider.Height)
                    collider.Top -= speed * deltaTime;
                animation.updateAnimation(deltaTime, new Vector2f(collider.Left, collider.Top));
                if (collider.Left > leftBoundary && collider.Left < rightBoundary - collider.Width && collider.Top > topBoundary && collider.Top < botBoundary - collider.Height) setting = false;
            }
            else
            {
                moveTime -= deltaTime;
                collider.Left += velocity.X * deltaTime;
                collider.Top += velocity.Y * deltaTime;
                animation.updateAnimation(deltaTime, new Vector2f(collider.Left, collider.Top));
                if (collider.Left < leftBoundary || collider.Left > rightBoundary - collider.Width || collider.Top < topBoundary || collider.Top > botBoundary - collider.Height || moveTime < 0)
                {
                    float tmp = (float)(rand.NextDouble() * 2 * Math.PI);
                    velocity = new Vector2f((float)Math.Sin(tmp) * speed, (float)Math.Cos(tmp) * speed);
                    moveTime = (float)rand.NextDouble() * 2 + 1;
                }
            }
            return this;
        }

    }
}
