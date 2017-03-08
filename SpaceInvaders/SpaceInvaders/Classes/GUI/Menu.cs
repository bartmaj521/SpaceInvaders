using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceInvaders.Classes.GUI
{
    class Menu
    {


        public Menu()
        {
            RenderWindow window = new RenderWindow(new VideoMode(400, 600), "Menu");
            OurButton button1 = new OurButton("Hello", new Vector2f(100, 100), btnStyle.blueish);
            while (window.IsOpen)
            {
                
            }

        }
    }
}
