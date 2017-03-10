using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace SpaceInvaders.Classes.GUI
{
    class SceneManager
    {
        //stos scen
        private Stack<Scene> sceneStack;
        //okno gry
        public RenderWindow window;
        private bool running;

        //wzorzec singleton - mozliwa tylko jedna instancja klasy
        #region Singleton Constructor
        private static SceneManager instance;
        

        //niejawne wywolanie konstruktora, gdy nie zostal wczenisej wywolany
        public static SceneManager Instance(VideoMode vidmode, string windowTitle)
        {
            if (instance == null)
            {
                instance = new SceneManager(vidmode,windowTitle);
            }
            return instance;
        }
        public static SceneManager Instance()
        {
            return instance;
        }

        //prywatny konstruktor
        private SceneManager(VideoMode vidmode, string windowTitle)
        {
            sceneStack = new Stack<Scene>();
            window = new RenderWindow(vidmode, windowTitle);
            window.MouseMoved += onMouseMoved;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.Closed += onClosed;
            window.SetMouseCursorVisible(false);
            running = false;
        }
        #endregion

        //obsluga zdarzen, wywolywanie metod przeciazanych w kazdej scenie, odpowiedzialnych za obsluge zdarzen
        #region Event handlers
        private void onClosed(object sender, EventArgs e)
        {
            (sender as RenderWindow).Close();
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            sceneStack.Peek().callOnMouseButtonReleased(sender, e,this);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            sceneStack.Peek().callOnMouseButtonPressed(sender, e,this);
        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            sceneStack.Peek().callOnMoved(sender,e,this);

        } 
        #endregion

        public void pushScene(Scene scene)
        {
            //zatrzymanie poprzedniej sceny
            sceneStack.Peek().pause();
            //dodanie nowej sceny na stos
            sceneStack.Push(scene);
            sceneStack.Peek().initialize();
        }
        public void popScene()
        {
            //zdjecie obecnej sceny ze stosu
            sceneStack.Pop();
            //wznowienie poprzedniej sceny
            sceneStack.Peek().reasume();
        }
        public void changeScene(Scene scene)
        {
            //zdjecie obecnej sceny
            if (sceneStack.Count != 0)
            {
                sceneStack.Pop();
            }
            //dodanie nowej sceny na stos
            sceneStack.Push(scene);
            sceneStack.Peek().initialize();
        }

        //zmiana managera gdy zmienia sie scena
        public void setManager()
        {
            sceneStack.Peek().setManager(this);
        }
        public void update()
        {
            //wywolanie update() na czubku stosu
            sceneStack.Peek().updateComponents(this);
        }
        public void draw()
        {
            //wywolanie draw() na czubku stosu
            sceneStack.Peek().drawComponents(this);
        }

        //glowna petla gry
        public void run()
        {
            running = true;
            while(running)
            {
                window.Clear();
                window.DispatchEvents();
                setManager();
                update();
                draw();
                window.Display();
            }
            cleanup();
        }

        //wyczyszczenie stosu scen
        private void cleanup()
        {
            sceneStack.Clear();
        }

        //wylaczenie gry
        public void quit()
        {
            running = false;
            window.Close();
        }
    }
}
