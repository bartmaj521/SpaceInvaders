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
        public static float topBoundary { get; set; } = 0;

        private Vector2f velocity;

        public Bullet(Vector2f startingPosition, Vector2f _velocity, ref Texture txt)
        {
            velocity = _velocity;
            animation = new Animation();
            animation.animationSprite.Position = startingPosition;
            animation.animationSprite.Texture = txt;
        }
        public override void update(float deltaTime)
        {
            animation.animationSprite.Position += velocity * deltaTime;
        }
    }
}
