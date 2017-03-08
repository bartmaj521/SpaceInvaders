using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes
{
    class Player : GameObject
    {
        public static float rightBoundary { get; set; }
        public static float leftBoundary { get; set; }

        public float playerSpeed { get; set; }

        public override void update(float deltaTime)
        {
            Vector2f offset = new Vector2f(0,0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                offset.X = -playerSpeed * deltaTime;
            }
            else if(Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                offset.X = playerSpeed * deltaTime;
            }
            collider.Left += offset.X;
            if(collider.Left + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X > rightBoundary)
            {
                collider.Left = rightBoundary - animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            }
            if(collider.Left < 0)
            {
                collider.Left = 0;
            }
            animation.updateAnimation(deltaTime, new Vector2f(collider.Left, collider.Top));
        }
    }
}
