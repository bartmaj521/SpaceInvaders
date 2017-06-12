using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;



namespace SpaceInvaders.Classes
{
    class Particle : GameObject
    {
        private Shape par;
        public Vector2f velocity;

        public float lifetime;

        public Particle()
        {

        }

        public Particle(Vector2f pos)
        {
            par = new CircleShape(2);
            par.Position = pos;
            par.FillColor = new Color(255, 255, 255, 128);
        }

        public void setColor(Color col)
        {
            par.FillColor = col;
        }

        public void setRadius(int rad)
        {
            (par as CircleShape).Radius = rad;
        }

        public override GameObject update(float deltaTime)
        {
            lifetime -= deltaTime;
            if (lifetime < 0)
                return null;
            par.Position += velocity * deltaTime;
            return this;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(par, states);
        }
    }
}
