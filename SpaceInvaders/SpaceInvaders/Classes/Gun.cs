using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;

using SpaceInvaders.Classes;

namespace SpaceInvaders.Classes
{
    class Gun
    {
        // Wzór pocisku którym strzela broń
        private Projectile prefab;

        // Generator liczb losowych
        private Random rand;

        // Szybkostrzelność
        private float fireRate;

        // Odnowienie przed następnym strzałem
        private float cooldown;

        // Ilość pocisków wystrzeliwywana podczas jednego strzału
        private int nbOfProj;

        // Ilość pozostałęj amunicji
        private int ammo;

        // Rozrzut podczas strzelania wieloma pociskami (tylko przy nbOfProj > 1) maksymalne odchylenie w stopniach
        private float dispersion;

        // Niecelność broni maksymalne odchylenie w stopniach
        private float inaccuracy;

        // Prędkość pocisku
        private float speed;

        // Konstruktor parametry jak w polach klasy
        public Gun(Projectile _prefab = null, float _fireRate = 5, int _nbOfProj = 3, int _ammo = 100, float _inaccuracy = 0, float _speed = 300, float _dispersion = 10)
        {
            prefab = _prefab;
            rand = new Random();
            fireRate = _fireRate;
            nbOfProj = _nbOfProj;
            ammo = _ammo;
            inaccuracy = _inaccuracy;
            speed = _speed;
            dispersion = _dispersion;
            cooldown = 0f;
        }

        // Aktualizowanie odnowienia broni
        public void update(float deltaTime)
        {
            cooldown -= deltaTime;
        }

        // Wystrzelenie pocisku
        public List<Projectile> fire(Vector2f firePosition)
        {
            if (cooldown <= 0 && ammo > 0)
            {
                cooldown = 1 / fireRate;
                ammo--;
                List<Projectile> p = new List<Projectile>();
                if (nbOfProj == 1)
                {
                    p.Add((Projectile)prefab.Clone());
                    float angle = -180 + (float)rand.NextDouble() * 2 * inaccuracy - inaccuracy;
                    p[0].velocity = new Vector2f(speed * (float)Math.Sin((angle * Math.PI / 180)), speed * (float)Math.Cos((angle * Math.PI / 180)));
                    p[0].animation.animationSprite.Position = new Vector2f(firePosition.X - p[0].animation.animationSprite.Scale.X * p[0].animation.animationSprite.Texture.Size.X / 2, firePosition.Y);
                }
                else
                {
                    for (int i = 0; i < nbOfProj; i++)
                    {
                        p.Add((Projectile)prefab.Clone());
                        p[i].animation.animationSprite.Position = firePosition;
                        float angle = -180 - dispersion + i * 2 * dispersion / (nbOfProj - 1);
                        angle += (float)rand.NextDouble() * 2 * inaccuracy - inaccuracy;
                        p[i].velocity = new Vector2f(speed * (float)Math.Sin((angle * Math.PI / 180)), speed * (float)Math.Cos((angle * Math.PI / 180)));
                        p[i].animation.animationSprite.Position = new Vector2f(firePosition.X - p[i].animation.animationSprite.Scale.X * p[i].animation.animationSprite.Texture.Size.X / 2, firePosition.Y);
                    }
                }
                return p;

            }
            return null;
        }
    }
}
