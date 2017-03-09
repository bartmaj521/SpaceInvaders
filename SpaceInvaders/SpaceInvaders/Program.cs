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
            Menu mainMenu = new Menu("Main menu");
            mainMenu.show();   
        }
    }
}
