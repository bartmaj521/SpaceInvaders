using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using SpaceInvaders.Classes;

namespace SpaceInvaders
{
    static class Program
    {
        static void Main()
        {
            RenderWindow lol = new RenderWindow(new VideoMode(1000, 800), "lol");
            Texture txt = new Texture("Player.png");
            int[] frames = new int[] { 0, 1 };
            Player.leftBoundary = 0f;
            Player.rightBoundary = 1000f;
            Player player = new Player(ref txt, frames, 0.75f, new Vector2f(150, 540), new Vector2f(0.25f,0.5f));
            Clock clock = new Clock();

            while (lol.IsOpen)
            {
                player.update(clock.Restart().AsSeconds());

                lol.Clear();
                lol.Draw(player.animation.animationSprite);
                lol.Display();
            }
        }
    }
}
