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

            RenderWindow window = new RenderWindow(new VideoMode(300, 600), "lol");
            window.Closed += OnClose;
            window.MouseMoved += onMouseMoved;
            window.MouseButtonPressed += onMouseButtonPressed;
            OurButton btnHello = new OurButton("Hello", new Vector2f(100, 100), 0);
            Event e = new Event();
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Display();
                

                btnHello.update(e, window);
                window.Draw(btnHello);
            }
        }

        private static void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            
        }

        private static void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnClose(object sender, EventArgs e)
        {
            (sender as RenderWindow).Close();
        }
    }
}
