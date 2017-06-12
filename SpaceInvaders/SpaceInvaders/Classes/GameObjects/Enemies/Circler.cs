//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using SFML.Graphics;
//using SFML.System;

//namespace SpaceInvaders.Classes.Enemies
//{
//    class Circler : Enemy
//    {
//        private float radius;
//        private float angularVelocity;
//        private float currentAngle;
//        private float x, y;

//        public Circler(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale, float enemySpeed) : base(ref txt, _frames, _frameTime, startingPosition, Scale, enemySpeed)
//        {
//            float tmp = (float)(rand.NextDouble() * 2 * Math.PI);
//            velocity = new Vector2f((float)Math.Sin(tmp) * speed, (float)Math.Cos(tmp) * speed);
//            moveTime = (float)rand.NextDouble() * 2 + 1;
//        }

//        public override GameObject update(float deltaTime)
//        {
//            if (health <= 0)
//                return null;
//            if (setting)
//            {
//                if (x - radius < leftBoundary)
//                    x += speed * deltaTime;
//                else if (x + radius > rightBoundary - collider.Width)
//                    x -= speed * deltaTime;
//                if (y - radius < 0)
//                    y += speed * deltaTime;
//                else if (y + radius > botBoundary - collider.Height)
//                    y -= speed * deltaTime;

//                animation.updateAnimation(deltaTime, new Vector2f(collider.Left, collider.Top));
//                if (collider.Left > leftBoundary && collider.Left < rightBoundary - collider.Width && collider.Top > topBoundary && collider.Top < botBoundary - collider.Height) setting = false;
//            }
//            else
//            {
//                moveTime -= deltaTime;
//                collider.Left += velocity.X * deltaTime;
//                collider.Top += velocity.Y * deltaTime;
//                animation.updateAnimation(deltaTime, new Vector2f(collider.Left, collider.Top));
//                if (collider.Left < leftBoundary || collider.Left > rightBoundary - collider.Width || collider.Top < topBoundary || collider.Top > botBoundary - collider.Height || moveTime < 0)
//                {
//                    float tmp = (float)(rand.NextDouble() * 2 * Math.PI);
//                    velocity = new Vector2f((float)Math.Sin(tmp) * speed, (float)Math.Cos(tmp) * speed);
//                    moveTime = (float)rand.NextDouble() * 2 + 1;
//                }
//            }
//            return this;
//        }
//    }
//}
