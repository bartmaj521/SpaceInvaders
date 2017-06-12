using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using SpaceInvaders.Classes;
using SpaceInvaders.Classes.GUI;
using SpaceInvaders.Classes.Enemies;
using SpaceInvaders.Interfaces;

namespace SpaceInvaders.Classes
{
    class GameScene : Scene
    {

        #region Singleton constructor
        private static GameScene instance;
        public static GameScene Instance()
        {
            if (instance == null)
            {
                instance = new GameScene();
            }
            return instance;
        }
        private GameScene() : base() { }
        #endregion


        Player player;
        List<Projectile> projectileList;
        List<Enemy> enemyList;
        List<Projectile> enemyProjectileList;
        Clock clock;

        OurLabel score;

        float timeToEnd;

        public override void callOnKeyPressed(object sender, KeyEventArgs e, SceneManager sceneManager)
        {
            //throw new NotImplementedException();
        }

        public override void callOnMouseButtonPressed(object sender, MouseButtonEventArgs e, SceneManager sceneManager)
        {
            //throw new NotImplementedException();
        }

        public override void callOnMouseButtonReleased(object sender, MouseButtonEventArgs e, SceneManager sceneManager)
        {
            //throw new NotImplementedException();
        }

        public override void callOnMoved(object sender, MouseMoveEventArgs e, SceneManager sceneManager)
        {
            //throw new NotImplementedException();
        }

        public override void cleanup()
        {
            //throw new NotImplementedException();
        }

        public override void drawComponents(SceneManager sceneManager)
        {
            sceneManager.window.Draw(Background.Instance());

            foreach (var proj in projectileList)
            {
                sceneManager.window.Draw(proj);
            }

            foreach (var proj in enemyProjectileList)
            {
                sceneManager.window.Draw(proj);
            }

            foreach (var enemy in enemyList)
            {
                sceneManager.window.Draw(enemy);
            }

            foreach (var proj in projectileList)
            {
                if (proj.GetType() == typeof(Explosion))
                    sceneManager.window.Draw(proj);
            }

            sceneManager.window.Draw(ParticleSystem.Instance());
            sceneManager.window.Draw(player);
            sceneManager.window.Draw(score);
        }

        public override void initialize(RenderWindow window)
        {
            timeToEnd = 2f;

            Console.WriteLine("inicjalizacja");
            projectileList = new List<Projectile>();
            enemyList = new List<Enemy>();
            enemyProjectileList = new List<Projectile>();

            Player.leftBoundary = 0;
            Player.rightBoundary = window.Size.X;

            Projectile.topBoundary = -50;
            Projectile.botBoundary = window.Size.Y + 100;

            Enemy.botBoundary = window.Size.Y - 200;
            Enemy.topBoundary = 0;
            Enemy.leftBoundary = 0;
            Enemy.rightBoundary = window.Size.X;
            Enemy.enemyProjectileList = enemyProjectileList;

            ParticleSystem.Instance().clear();

            clock = new Clock();

            Background.Instance().initialize();

            Texture tmpPlayer = PlayerManager.Instance.ShipInfo.ShipTexture;
            player = new Player(ref tmpPlayer, new int[1] { 0 }, 100f, new Vector2f(window.Size.X / 2, window.Size.Y - 100), new Vector2f(0.6f, 0.6f), (PlayerManager.Instance.ShipInfo.DefaultSpeed + PlayerManager.Instance.ShipInfo.upgrades[0]) * 100, Convert.ToInt32((1 - PlayerManager.Instance.ShipInfo.ShipHealth) * (PlayerManager.Instance.ShipInfo.DefaultHealth + PlayerManager.Instance.ShipInfo.upgrades[1] * 10)));         // Ustawić prędkość poprawne
            Enemy.player = player;
            Texture shot = new Texture("bullet.png");
            player.setGun(new Gun(new Bullet(ref shot, (PlayerManager.Instance.ShipInfo.upgrades[4] + 1) * 10, new Vector2f(0.5f, 0.5f)), (PlayerManager.Instance.ShipInfo.upgrades[3] + 1) * 2, PlayerManager.Instance.currentShip + 1, Int32.MaxValue, 5 - PlayerManager.Instance.ShipInfo.upgrades[2], 500, 10));
            player.setPowerUps(PlayerManager.Instance.Powerups[4], PlayerManager.Instance.Powerups[1], PlayerManager.Instance.Powerups[0], PlayerManager.Instance.Powerups[2], PlayerManager.Instance.Powerups[3]);
            // Inicjalizacja statku

            // Inicjalizacja poziomu
            EnemySpawner.Instance.initialize(PlayerManager.Instance.MissionProgress, ref enemyList);

            score = new GUI.OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 40);
            score.setPosition(new SFML.System.Vector2f(window.Size.X / 2 - score.Size.X / 2, window.Size.Y / 2 - score.Size.Y / 2));
            score.Visible = false;
            score.Active = false;
        }

        public override void pause()
        {
            throw new NotImplementedException();
        }

        public override void reasume()
        {
            throw new NotImplementedException();
        }

        public override void updateComponents(SceneManager sceneManager)
        {
            float deltaTime = clock.Restart().AsSeconds();

            player.update(deltaTime);

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i] = (Enemy)enemyList[i].update(deltaTime);
            }

            EnemySpawner.Instance.update(deltaTime);
            Background.Instance().update(deltaTime);
            ParticleSystem.Instance().update(deltaTime);

            // Sprawdzenie kolizji pocisków z przeciwnikami
            List<IDamageable> dmgList = toDamageableList(enemyList);
            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i] = (Projectile)projectileList[i].checkCollision(dmgList);
                if (projectileList[i] != null)
                {
                    projectileList[i] = (Projectile)projectileList[i].update(deltaTime);
                }
            }

            // Sprawdzenie kolizji pocisków z graczem
            for (int i = 0; i < enemyProjectileList.Count; i++)
            {
                enemyProjectileList[i] = (Projectile)enemyProjectileList[i].checkCollision(player);
                if (enemyProjectileList[i] != null)
                {
                    enemyProjectileList[i] = (Projectile)enemyProjectileList[i].update(deltaTime);
                }
            }

            List<Projectile> p = player.fire();
            if (p != null)
                projectileList.AddRange(p);

            p = player.useLaser();
            if (p != null)
            {
                (p[0] as LaserBeam).fix();
                projectileList.AddRange(p);
            }
            p = player.useMissile();
            if (p != null)
            {
                (p[0] as Missile).setTarget(enemyList);
                projectileList.AddRange(p);
            }
            p = player.useBomb();
            if (p != null)
            {
                projectileList.AddRange(p);
            }
            p = player.useWave();
            if (p != null)
            {
                (p[0] as IonWave).fix();
                p[0].animation.animationSprite.Position = new Vector2f(player.animation.animationSprite.Position.X + player.animation.animationSprite.Scale.X * player.animation.animationSprite.Texture.Size.X / 2, p[0].animation.animationSprite.Position.Y);
                projectileList.AddRange(p);
            }

            projectileList.RemoveAll(proj => proj == null);
            enemyProjectileList.RemoveAll(proj => proj == null);
            enemyList.RemoveAll(enem => enem == null);

            // Sprawdzenie warunku porażki
            if (player.health <= 0)
            {
                timeToEnd -= deltaTime;
                score.Visible = true;
                score.Active = true;
                score.Text = "Porazka";
                if (timeToEnd < 0)
                {
                    //EndingMenu.Instance().hasWon = false;
                    SceneManager.Instance().changeScene(PlayerMenu.Instance());
                }
            }

            // Sprzawdzenie warunku zwycięstwa
            if (enemyList.Count == 0 && EnemySpawner.Instance.isEmpty)
            {
                timeToEnd -= deltaTime;
                score.Visible = true;
                score.Active = true;
                score.Text = "Wygrana";
                if (timeToEnd < 0)
                {
                    //EndingMenu.Instance().hasWon = true;
                    PlayerManager.Instance.missionCompleted();
                    SceneManager.Instance().changeScene(PlayerMenu.Instance());
                }
            }
        }

        private static List<IDamageable> toDamageableList(List<Enemy> enemyList)
        {
            List<IDamageable> damageableList = new List<IDamageable>();
            damageableList.AddRange(enemyList);
            return damageableList;
        }
    }
}
