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
            SceneManager.Instance().changeScene(GameScene.Instance());
            SceneManager.Instance().run();
        }
    }
}
