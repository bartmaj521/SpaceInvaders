﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SFML.Graphics;
using SFML.Window;


namespace SpaceInvaders
{
    static class Program
    {
        static void Main()
        {
            RenderWindow lol = new RenderWindow(new VideoMode(300, 600), "lol");
            while (lol.IsOpen)
            {
                lol.Display();
            }
        }
    }
}
