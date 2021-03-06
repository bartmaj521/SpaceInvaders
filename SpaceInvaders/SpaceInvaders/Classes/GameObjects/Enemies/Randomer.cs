﻿using System;

using SFML.Graphics;
using SFML.System;

using SpaceInvaders.Classes.GUI;

namespace SpaceInvaders.Classes.Enemies
{
    class Randomer : Enemy
    {
        public static Bullet bulletPrefab;
        public static int value;

        public Randomer(Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale, float enemySpeed) : base(ref txt, _frames, _frameTime, startingPosition, Scale, enemySpeed)
        {
            float tmp = (float)(rand.NextDouble() * 2 * Math.PI);
            velocity = new Vector2f((float)Math.Sin(tmp) * speed, (float)Math.Cos(tmp) * speed);
            moveTime = (float)rand.NextDouble() * 2 + 1;
        }

        public override GameObject update(float deltaTime)
        {
            if (health <= 0)
            {
                ParticleSystem.Instance().enemyDiedburst(new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2, animation.animationSprite.Position.Y + animation.animationSprite.Scale.Y * animation.animationSprite.Texture.Size.Y / 2));
                PlayerManager.Instance.donatePlayer(value);
                return null;
            }
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
                if(rand.Next() % 10000 == 0)
                {
                    Bullet p = (Bullet)bulletPrefab.Clone();
                    Vector2f vel = new Vector2f();
                    p.animation.animationSprite.Origin = new Vector2f(p.animation.animationSprite.Scale.X * p.animation.animationSprite.Texture.Size.X / 2, p.animation.animationSprite.Scale.Y * p.animation.animationSprite.Texture.Size.Y / 2);
                    p.animation.animationSprite.Position = new Vector2f(collider.Left + collider.Width / 2, collider.Top + collider.Height / 2);
                    vel.X = collider.Left + collider.Width / 2 - (player.getCollider().Left + player.getCollider().Width / 2);
                    vel.Y = collider.Top + collider.Height / 2 - (player.getCollider().Top + player.getCollider().Height / 2);
                    vel = vel / 1.5f;
                    p.velocity = -vel;

                    enemyProjectileList.Add(p);
          
                }
            }
            return this;
        }

        public override object Clone()
        {
            Randomer p = (Randomer)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite.Texture = this.animation.animationSprite.Texture;
            p.collider.Height = p.animation.animationSprite.GetGlobalBounds().Height;
            p.collider.Width = p.animation.animationSprite.GetGlobalBounds().Width;

            return (object)p;
        }
    }
}
