using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;


using SpaceInvaders.Interfaces;

namespace SpaceInvaders.Classes.Enemies
{
    abstract class Enemy : GameObject, IDamageable, ICloneable
    {
        protected static Random rand;

        // Granice ruchu przeciwników
        public static float topBoundary { get; set; }
        public static float botBoundary { get; set; }
        public static float leftBoundary { get; set; }
        public static float rightBoundary { get; set; }

        public static List<Projectile> enemyProjectileList;

        protected float speed;
        protected float moveTime;
        protected Vector2f velocity;

        protected bool setting = true;

        public static Player player;

        // Punkty życia przeciwnika
        protected float health = 100;

        public Enemy(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale, float enemySpeed) : base(ref txt, _frames, _frameTime, startingPosition, Scale)
        {
            speed = enemySpeed;
            rand = new Random();
        }

        public void getDamaged(float damage)
        {
            health -= damage;
        }

        public abstract override GameObject update(float deltaTime);
        public abstract object Clone();
    }
}