using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using SpaceInvaders.Classes.GUI;

namespace SpaceInvaders
{
    static class Program
    {
        
        static void Main()
        {
            SceneManager.Instance(new VideoMode(1600, 900), "Space Invaders");
            SceneManager.Instance().changeScene(MainMenu.Instance());
            SceneManager.Instance().run();

        }
    }
}
