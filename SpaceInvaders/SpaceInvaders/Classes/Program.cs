using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

// Majewski
using SpaceInvaders.Classes;
using SpaceInvaders.Classes.Enemies;

using SpaceInvaders.Interfaces;
//=======
using SpaceInvaders.Classes.GUI;
//master

namespace SpaceInvaders
{
    static class Program

    {
        static List<Projectile> bulletList = new List<Projectile>();
        static Player player;
        static Texture shot;

        static List<IDamageable> toDamageableList(List<Enemy> enemyList)
        {
            List<IDamageable> damageableList = new List<IDamageable>();
            damageableList.AddRange(enemyList);
            return damageableList;
        }

        static void Main()
        {
            SceneManager.Instance(new VideoMode(1600, 900), "Space Invaders");
            SceneManager.Instance().changeScene(MainMenu.Instance());
            SceneManager.Instance().run();
          
            //RenderWindow lol = new RenderWindow(new VideoMode(1600, 900), "lol");

           

            /*RenderWindow lol = new RenderWindow(new VideoMode(1000, 800), "lol");

            Texture txt = new Texture("Player.png");
            shot = new Texture("Shot.png");

            int[] frames = new int[] { 0, 1, 2, 3 };
            Player.leftBoundary = 0f;
            Player.rightBoundary = 1600f;

            Projectile.topBoundary = 0;

            player = new Player(ref txt, frames, 0.75f, new Vector2f(450, 800), new Vector2f(5f, 5f), 300f);
            player.addGun(new Gun(new Bullet(ref shot, 10, new Vector2f(1, 1)), 5, 1, 100, 0, 500, 3));

            Clock clock = new Clock();

            Enemy.botBoundary = 700;
            Enemy.topBoundary = 0;
            Enemy.leftBoundary = 0;
            Enemy.rightBoundary = 1600;

            List<Enemy> enemyList = new List<Enemy>();

            enemyList.Add(new Randomer(ref txt, frames, 1f, new Vector2f(1200, -250), new Vector2f(5, 5), 100));
            //enemyList.Add(new Randomer(ref txt, frames, 1f, new Vector2f(400, -250), new Vector2f(5, 5), 100));

            while (lol.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                player.update(deltaTime);
                
                lol.Clear();
                lol.Draw(player.animation.animationSprite);
                for(int i = 0; i< enemyList.Count; i++)
                {
                    lol.Draw(enemyList[0].animation.animationSprite);
                    enemyList[0] = (Enemy)enemyList[0].update(deltaTime);
                }

                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (bulletList[i].GetType() == typeof(Bullet))
                    {
                        lol.Draw(bulletList[i].animation.animationSprite);
                        bulletList[i] = (Bullet)bulletList[i].checkCollision(toDamageableList(enemyList));
                        if (bulletList[i] != null)
                        {
                            bulletList[i] = (Bullet)bulletList[i].update(deltaTime);
                        }
                    }
                }
                List<Projectile> p = player.fire();
                if (p != null)
                    bulletList.AddRange(p);
                lol.Display();
                bulletList.RemoveAll(proj => proj == null);
                enemyList.RemoveAll(proj => proj == null);
            }

            }*/

        }
    }
}
