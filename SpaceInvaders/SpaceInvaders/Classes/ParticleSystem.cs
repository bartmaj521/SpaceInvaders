using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;

namespace SpaceInvaders.Classes
{
    class ParticleSystem: Drawable
    {
        #region Singleton constructor
        private static ParticleSystem instance;
        public static ParticleSystem Instance()
        {
            if (instance == null)
            {
                instance = new ParticleSystem();
            }
            return instance;
        }
        private ParticleSystem()
        {
            rand = new Random();
            particleList = new List<Particle>();
        }
        #endregion

        private List<Particle> particleList;
        private Random rand;

        public void clear()
        {
            particleList.Clear();
        }

        public void update(float deltaTime)
        {
            for(int i = 0; i < particleList.Count; i++)
            {
                particleList[i] = (Particle)particleList[i].update(deltaTime);
            }
            particleList.RemoveAll(part => part == null);
        }

        public void enemyHitburst(Vector2f pos)
        {
            int count = rand.Next() % 10 + 5;
            for(int i = 0; i < count; i++)
            {
                Particle p = new Particle(pos);
                float angle = (float)(rand.NextDouble() * 2 * Math.PI);
                float speed = (float)rand.NextDouble() * 300 + 200;
                p.velocity = new Vector2f(speed * (float)Math.Sin(angle), speed * (float)Math.Cos(angle));
                p.lifetime = (float)rand.NextDouble() * 0.8f + 0.2f;
                p.setColor(new Color(Convert.ToByte(rand.Next() % 128 + 128), 0, 0, 200));
                p.setRadius(rand.Next() % 4 + 1);
                particleList.Add(p);
            }
        }

        public void enemyDiedburst(Vector2f pos)
        {
            int count = rand.Next() % 50 + 50;
            for (int i = 0; i < count; i++)
            {
                Particle p = new Particle(pos);
                float angle = (float)(rand.NextDouble() * 2 * Math.PI);
                float speed = (float)rand.NextDouble() * 300 + 200;
                p.velocity = new Vector2f(speed * (float)Math.Sin(angle), speed * (float)Math.Cos(angle));
                p.lifetime = (float)rand.NextDouble() * 0.8f + 0.2f;
                p.setColor(new Color(Convert.ToByte(rand.Next() % 128 + 128), 0, 0, 200));
                p.setRadius(rand.Next() % 4 + 1);
                particleList.Add(p);
            }
        }

        public void playerHitburst(Vector2f pos)
        {
            int count = rand.Next() % 10 + 5;
            for (int i = 0; i < count; i++)
            {
                Particle p = new Particle(pos);
                float angle = (float)(rand.NextDouble() * 2 * Math.PI);
                float speed = (float)rand.NextDouble() * 300 + 200;
                p.velocity = new Vector2f(speed * (float)Math.Sin(angle), speed * (float)Math.Cos(angle));
                p.lifetime = (float)rand.NextDouble() * 0.8f + 0.2f;
                p.setColor(new Color(0, Convert.ToByte(rand.Next() % 128 + 128), 0, 200));
                p.setRadius(rand.Next() % 4 + 1);
                particleList.Add(p);
            }
        }

        public void playerDiedburst(Vector2f pos)
        {
            int count = rand.Next() % 50 + 50;
            for (int i = 0; i < count; i++)
            {
                Particle p = new Particle(pos);
                float angle = (float)(rand.NextDouble() * 2 * Math.PI);
                float speed = (float)rand.NextDouble() * 300 + 200;
                p.velocity = new Vector2f(speed * (float)Math.Sin(angle), speed * (float)Math.Cos(angle));
                p.lifetime = (float)rand.NextDouble() * 0.8f + 0.2f;
                p.setColor(new Color(0, Convert.ToByte(rand.Next() % 128 + 128), 0, 200));
                p.setRadius(rand.Next() % 4 + 1);
                particleList.Add(p);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var part in particleList)
            {
                target.Draw(part);
            }
        }
    }
}
