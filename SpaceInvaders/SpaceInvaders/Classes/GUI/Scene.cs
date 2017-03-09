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
        public Scene()
        {
        }

        //obsluga zdarzen
        public abstract void callOnMoved(MouseMoveEventArgs e);
        public abstract void callOnMouseButtonPressed(object sender,MouseButtonEventArgs e);
        public abstract void callOnMouseButtonReleased(object sender, MouseButtonEventArgs e);


        //ponizsze metody wywolywane sa przez sceneManagera
        public abstract void initialize();
        public abstract void drawComponents(SceneManager sceneManager);
        public abstract void updateComponents(SceneManager sceneManager);
        public abstract void pause();
        public abstract void reasume();       

    }
}
