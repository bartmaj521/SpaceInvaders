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
    class PlayMenu:Menu
    {
        #region Singleton constructor
        private static PlayMenu instance;
        public static PlayMenu Instance()
        {
            if(instance==null)
            {
                instance = new PlayMenu();
            }
            return instance;
        }
        private PlayMenu()
        {

        }
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
        public override void initialize()
        {
            if (!initialized)
            {
                OurButton btnMain = new OurButton(new Texture("buttonSprite.png"), "Main", new Vector2i(100, 32), new Vector2f(200, 100));
                componentList.Add(btnMain);
                OurButton btnExit = new OurButton(new Texture("buttonSprite.png"), "Exit", new Vector2i(100, 32), new Vector2f(200, 150));
                componentList.Add(btnExit);
                componentList.Add(Cursor.Instance(new Texture("cursor.png"), new Vector2f(1f, 1f)));

                btnMain.MousePressed += OnMainButtonPressed;
                btnExit.MouseReleased += OnExitButtonReleased;
                initialized = true;
            }

        }

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
        private void OnMainButtonPressed(object sender, EventArgs e)
        {
            sceneManager.changeScene(MainMenu.Instance());
        }
    }
}
