using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using SpaceInvaders.Classes;

namespace SpaceInvaders
{
    static class Program
    {
        static void Main()
        {
            RenderWindow lol = new RenderWindow(new VideoMode(1000, 800), "lol");
            Texture txt = new Texture("Player.png");
            Texture shot = new Texture("Shot.png");

            int[] frames = new int[] { 0, 1, 2, 3 };
            Player.leftBoundary = 0f;
            Player.rightBoundary = 1000f;
            Player player = new Player(ref txt, frames, 0.75f, new Vector2f(150, 740), new Vector2f(5f,5f));
            Clock clock = new Clock();

            List<Bullet> bulletList = new List<Bullet>();

            bulletList.Add(new Classes.Bullet(new Vector2f(500, 750), new Vector2f(0, -500), ref shot));


            while (lol.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                player.update(deltaTime);
                lol.Clear();
                lol.Draw(player.animation.animationSprite);
                foreach(Bullet bul in bulletList)
                {
                    bul.update(deltaTime);
                    lol.Draw(bul.animation.animationSprite);
                }
                lol.Display();
            }
        }
    }
}
