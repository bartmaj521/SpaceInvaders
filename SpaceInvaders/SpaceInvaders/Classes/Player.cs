using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

using SpaceInvaders.Interfaces;
using SpaceInvaders.Classes;
using SpaceInvaders.Classes.GUI;

namespace SpaceInvaders.Classes
{
    class Player : GameObject, IDamageable
    {
        // Lewa i prawa granica ruchu gracza
        public static float rightBoundary { get; set; } 
        public static float leftBoundary { get; set; }

        // Aktualne i maksymalne zdrowie gracza
        public float health { get; set; }
        public float maxHealth { get; set; }

        // Prędkość poruszania gracza
        public float playerSpeed { get; set; }

        // Lista broni gracza
        private Gun gun;
        public List<Gun> powerUps = new List<Gun>();
        public Shield shield;

        bool isDead = false;

        // Konstruktor txt - Tekstura gracza, _frames - kolejność klatek animacji, _frameTime - czas trawania klatki, startingPosition - pozycja startowa, Scale -skala
        public Player(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale, float _playerSpeed, int _health): base(ref txt, _frames, _frameTime, startingPosition, Scale)
        {
            playerSpeed = _playerSpeed;
            health = _health;
        }

        //aktualizowanie pozycji gracza
        public override GameObject update(float deltaTime)
        {
            if(health <= 0 && !isDead)
            {
                isDead = true;
                collider.Top = 1500;
                ParticleSystem.Instance().playerDiedburst(new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Scale.X * animation.animationSprite.Texture.Size.X / 2, animation.animationSprite.Position.Y + animation.animationSprite.Scale.Y * animation.animationSprite.Texture.Size.Y / 2));
            }
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

            shield.shieldSprite.Position = new Vector2f(collider.Left + collider.Width / 2, collider.Top + collider.Height / 2 );

            animation.updateAnimation(deltaTime, new Vector2f(collider.Left, collider.Top));

            gun.update(deltaTime);
            shield.update(deltaTime);

            foreach (var powUp in powerUps)
            {
                powUp.update(deltaTime);
            }

            return this;
        }

        // Dodanie broni dla gracza gun - broń do dodania
        public void setGun(Gun _gun)
        {
            gun = _gun;
        }

        // Ustawienie wzmocnień dla gracza
        public void setPowerUps(int shieldCharges, int laserAmmo, int missileAmmo, int bombAmmo, int waveAmmo)
        {
            shield = new Shield(10);
            shield.charges = shieldCharges;

            Texture laser = new Texture(ResourcesManager.resourcesPath + "laser.png");
            powerUps.Add(new Gun(new LaserBeam(laser, 100, 1), 0.5f, 1, laserAmmo, 0, 0, 0));

            Texture missile = new Texture(ResourcesManager.resourcesPath + "missile.png");
            powerUps.Add(new Gun(new Missile(missile, 100, new Vector2f(1,1)), 0.5f, 1, missileAmmo, 0, 400, 0));

            Texture bomb = new Texture(ResourcesManager.resourcesPath + "bomb.png");
            powerUps.Add(new Gun(new Bomb(bomb, 20, new Vector2f(1, 1)), 0.5f, 1, bombAmmo, 0, 200, 0));

            Texture wave = new Texture(ResourcesManager.resourcesPath + "ionwave.png");
            powerUps.Add(new Gun(new IonWave(wave, 10, new Vector2f(0.25f, 0.25f)), 0.5f, 1, waveAmmo, 0, 300, 0));
        }

        // Wystrzelenie pocisku z aktualnej broni
        public List<Projectile> fire()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Vector2f firePosition = new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X / 2, animation.animationSprite.Position.Y);
                return gun.fire(firePosition);
            }
            return null;
        }

        public List<Projectile> useLaser()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                Vector2f firePosition = new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X / 2, animation.animationSprite.Position.Y);
                List<Projectile> tmp = powerUps[0].fire(firePosition);
                if(tmp != null)
                    PlayerManager.Instance.usePowerUp(1);
                return tmp;
            }
            return null;
        }

        public List<Projectile> useMissile()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                Vector2f firePosition = new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X / 2, animation.animationSprite.Position.Y);
                List<Projectile> tmp = powerUps[1].fire(firePosition);
                if (tmp != null)
                    PlayerManager.Instance.usePowerUp(0);
                return tmp;
            }
            return null;
        }

        public List<Projectile> useBomb()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                Vector2f firePosition = new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X / 2, animation.animationSprite.Position.Y);
                List<Projectile> tmp = powerUps[2].fire(firePosition);
                if (tmp != null)
                    PlayerManager.Instance.usePowerUp(2);
                return tmp;
            }
            return null;
        }

        public List<Projectile> useWave()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.T))
            {
                Vector2f firePosition = new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X / 2, animation.animationSprite.Position.Y);
                List<Projectile> tmp = powerUps[3].fire(firePosition);
                if (tmp != null)
                    PlayerManager.Instance.usePowerUp(3);
                return tmp;
            }
            return null;
        }

        public void getDamaged(float damage)
        {
            if (shield.active)
            {
                damage /= 2;
            }
            health -= damage;
            PlayerManager.Instance.damageShip(damage);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (health > 0)
            {
                target.Draw(animation.animationSprite);
                if (shield.active)
                {
                    target.Draw(shield.shieldSprite);
                }
            }
        }
    }
}
