using SFML.Graphics;
using SFML.Window;


namespace SpaceInvaders.Classes.GUI
{
    public abstract class Scene
    {
        public Scene() { }
        protected SceneManager sceneManager = SceneManager.Instance();

        //obsluga zdarzen
        public abstract void callOnMoved(object sender,MouseMoveEventArgs e,SceneManager sceneManager);
        public abstract void callOnMouseButtonPressed(object sender,MouseButtonEventArgs e, SceneManager sceneManager);
        public abstract void callOnMouseButtonReleased(object sender, MouseButtonEventArgs e, SceneManager sceneManager);
        public abstract void callOnKeyPressed(object sender, KeyEventArgs e, SceneManager sceneManager);

        //ponizsze metody wywolywane sa przez sceneManagera
        public abstract void initialize(RenderWindow window);
        public abstract void drawComponents(SceneManager sceneManager);
        public abstract void updateComponents(SceneManager sceneManager);
    }
}