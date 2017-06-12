using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

using SpaceInvaders.Interfaces;

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
        private List<Gun> guns = new List<Gun>();

        // Aktualna broń
        int currGun = 0;

        // Konstruktor txt - Tekstura gracza, _frames - kolejność klatek animacji, _frameTime - czas trawania klatki, startingPosition - pozycja startowa, Scale -skala
        public Player(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale, float _playerSpeed): base(ref txt, _frames, _frameTime, startingPosition, Scale)
        {
            playerSpeed = _playerSpeed;  
        }

        //aktualizowanie pozycji gracza
        public override GameObject update(float deltaTime)
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

            foreach (var gun in guns)
            {
                gun.update(deltaTime);
            }
            return this;
        }

        // Dodanie broni dla gracza gun - broń do dodania
        public void addGun(Gun gun)
        {
            guns.Add(gun);
        }

        // Wystrzelenie pocisku z aktualnej broni
        public List<Projectile> fire()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Vector2f firePosition = new Vector2f(animation.animationSprite.Position.X + animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X / 2, animation.animationSprite.Position.Y);
                return guns[currGun].fire(firePosition);
            }
            return null;
        }

        public void switchGun(int x)
        {
            currGun += x;
            currGun %= guns.Count;
        }

        public void getDamaged(float damage)
        {
            health -= damage;
        }
    }
}
