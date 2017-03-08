using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using SpaceInvaders.Classes;
using SpaceInvaders.Classes.GUI;

namespace SpaceInvaders
{
    static class Program
    {

        
    
        static void Main()
        {
            Font myFont = new Font("comic.ttf");
            OurButton btn_hello = new OurButton("Hello", myFont, new Vector2f(100, 100), Style.none);
            RenderWindow window = new RenderWindow(new VideoMode(300, 600), "lol");
            window.Closed += OnClose;

            Event e = new Event();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Display();

                btn_hello.update(e, window);
                window.Draw(btn_hello);
            }
        }

        private static void OnClose(object sender, EventArgs e)
        {
            (sender as RenderWindow).Close();
        }
    }
}
