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
    abstract class Scene
    {
        string title;
        public Scene(string _title)
        {
            title = _title;

        }


        public void show()
        {
            initialize();
            RenderWindow window = new RenderWindow(new VideoMode(400, 600), title);
            window.MouseMoved += onMouseMoved;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.Closed += onClosed;

            

            while(window.IsOpen)
            {
                window.DispatchEvents();
                updateComponents();
                drawComponents(window);
                window.Display();
            }
        }

        private void onClosed(object sender, EventArgs e)
        {
            (sender as RenderWindow).Close();
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            callOnMouseButtonReleased(e);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            callOnMouseButtonPressed(sender,e);
        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            callOnMoved(e);
            
        }

        public abstract void callOnMoved(MouseMoveEventArgs e);
        public abstract void callOnMouseButtonPressed(object sender,MouseButtonEventArgs e);
        public abstract void callOnMouseButtonReleased(MouseButtonEventArgs e);

        public abstract void initialize();
        public abstract void drawComponents(RenderWindow window);
        public abstract void updateComponents();
    }
}
