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
        static List<Projectile> bulletList = new List<Projectile>();
        static Player player;
        static Texture shot;
        static void Main()
        {
            RenderWindow lol = new RenderWindow(new VideoMode(1000, 800), "lol");
            Texture txt = new Texture("Player.png");
            shot = new Texture("Shot.png");

            int[] frames = new int[] { 0, 1, 2, 3 };
            Player.leftBoundary = 0f;
            Player.rightBoundary = 1000f;

            Projectile.topBoundary = 0;

            player = new Player(ref txt, frames, 0.75f, new Vector2f(450, 700), new Vector2f(5f, 5f));
            player.addGun(new Gun(new Bullet(ref shot, 1, new Vector2f(1, 1)), 3, 2, 100, 0, 500, 3));
            Clock clock = new Clock();

            while (lol.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                player.update(deltaTime);
                lol.Clear();
                lol.Draw(player.animation.animationSprite);
                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (bulletList[i] != null && bulletList[i].GetType() == typeof(Bullet))
                    {
                        lol.Draw(bulletList[i].animation.animationSprite);
                        bulletList[i] = (Bullet)bulletList[i].update(deltaTime);
                    }
                }
                List<Projectile> p = player.fire();
                if (p != null)
                    bulletList.AddRange(p);
                lol.Display();
                bulletList.RemoveAll(proj => proj == null);
            }
        }
    }
}
