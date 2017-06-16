using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

using SpaceInvaders.Classes.GUI;

namespace SpaceInvaders.Classes
{
    class Shield
    {
        public Sprite shieldSprite;

        public int charges { get; set; }
        public bool active { get; private set; }
        protected float duration;
        public float durationLeft;

        public Shield(float _duration)
        {
            duration = _duration;
            shieldSprite = new Sprite();
            shieldSprite.Texture = new Texture(ResourcesManager.resourcesPath + "shield.png");
            shieldSprite.Scale = new Vector2f(0.22f, 0.22f);
            shieldSprite.Origin = new Vector2f(shieldSprite.Texture.Size.X / 2, shieldSprite.Texture.Size.Y / 2);
        }

        public void update(float deltaTime)
        {
            durationLeft -= deltaTime;
            if (active && durationLeft < 0)
            {
                active = false;
            }
            else if (active)
            {
                shieldSprite.Rotation += deltaTime * 180;
                byte tmp = (byte)Math.Ceiling(durationLeft * 255 / duration);
                shieldSprite.Color = new Color((byte)(255 - tmp),tmp, tmp);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                if (!active && charges > 0)
                {
                    active = true;
                    durationLeft = duration;
                    charges--;
                    PlayerManager.Instance.usePowerUp(4);
                }
            }
        }
    }
}
