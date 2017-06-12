using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;

namespace SpaceInvaders.Classes
{
    class Background: Drawable
    {
        #region Singleton constructor
        private static Background instance;
        public static Background Instance()
        {
            if (instance == null)
            {
                instance = new Background();
            }
            return instance;
        }
        private Background(){ }
        #endregion

        private Random rand;
        private List<Particle> stars;
        private int nbOfStars;

        public void initialize()
        {
            stars = new List<Particle>();
            rand = new Random();
            nbOfStars = 50;
        }

        public void update(float deltaTime)
        {
            for(int i = 0; i < stars.Count; i++)
            {
                stars[i] = (Particle)stars[i].update(deltaTime);
            }
            stars.RemoveAll(proj => proj == null);
            if(stars.Count < nbOfStars)
            {
                Particle p = new Particle(new Vector2f(rand.Next() % 1590 + 5, -rand.Next() % 500));
                p.velocity = new Vector2f(0, rand.Next() % 400 + 100);
                p.setRadius((int)p.velocity.Y / 100);
                p.lifetime = (float)rand.NextDouble() * 7.5f + 2.5f;
                p.setColor(new Color(255,255,255,Convert.ToByte(rand.Next() % 128 + 64)));
                stars.Add(p);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var star in stars)
            {
                target.Draw(star);
            }
        }
    }
}
