using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SpaceInvaders.Classes.GUI
{
    class MainMenu : Menu
    {
        #region Singleton constructor
        private static MainMenu instance;
        public static MainMenu Instance()
        {
            if (instance == null)
            {
                instance = new MainMenu();
            }
            return instance;
        }
        private MainMenu() : base() { }
        #endregion



        //obsluga gdy scena zostanie wstrzymana
        public override void pause()
        {
            throw new NotImplementedException();
        }

        //obsluga gdy scena zostanie wznowiona
        public override void reasume()
        {
            throw new NotImplementedException();
        }

        //inicjalizacja, dodawanie komponentów do listy komponentów
        public override void initialize(RenderWindow window)
        {
            if (!initialized)
            {
                Vector2i buttonSize = new Vector2i(300,99);
                uint fontSize = 26;
                OurTextbox txbUsername = new OurTextbox(new Texture("txbSprite.png"), new Vector2i(400, 60), new Vector2f(window.Size.X * 0.5f,window.Size.Y*0.5f), 30);
                componentList.Add(txbUsername);
                OurButton btnNewGame = new OurButton(new Texture("buttonSprite.png"), "nowa gra", buttonSize, new Vector2f(window.Size.X * 0.87f, window.Size.Y * 0.5f),fontSize);
                componentList.Add(btnNewGame);
                OurButton btnLoadGame = new OurButton(new Texture("buttonSprite.png"), "wczytaj gre", buttonSize, new Vector2f(window.Size.X * 0.87f, window.Size.Y * 0.64f),fontSize);
                componentList.Add(btnLoadGame);
                OurButton btnOptions = new OurButton(new Texture("buttonSprite.png"), "opcje", buttonSize, new Vector2f(window.Size.X * 0.87f, window.Size.Y * 0.78f),fontSize);
                componentList.Add(btnOptions);
                OurButton btnExit = new OurButton(new Texture("buttonSprite.png"), "wyjdz", buttonSize, new Vector2f(window.Size.X * 0.87f, window.Size.Y * 0.92f),fontSize);
                componentList.Add(btnExit);
                componentList.Add(Cursor.Instance(new Texture("cursor.png"), new Vector2f(1f, 1f)));

                btnNewGame.MousePressed += OnPlayButtonPressed;
                btnExit.MouseReleased += OnExitButtonReleased;
                initialized = true;
            }

        }

        //wyczyszczenie listy componentów
        public override void cleanup()
        {
            componentList.Clear();
        }
        //obsluga zwolnienia przycisku exit
        private void OnExitButtonReleased(object sender, EventArgs e)
        {
            sceneManager.quit();
        }

        //obsluga klikniecia przycisku 
        private void OnPlayButtonPressed(object sender, EventArgs e)
        {
            sceneManager.changeScene(PlayMenu.Instance());
        }
    }
}
