﻿using System;
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
            if(instance ==null)
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
        public override void initialize()
        {
            if (!initialized)
            {
                OurButton btnPlay = new OurButton(new Texture("buttonSprite.png"), "Play", new Vector2i(100, 32), new Vector2f(200, 100));
                componentList.Add(btnPlay);
                OurButton btnExit = new OurButton(new Texture("buttonSprite.png"), "Exit", new Vector2i(100, 32), new Vector2f(200, 150));
                componentList.Add(btnExit);
                componentList.Add(Cursor.Instance(new Texture("cursor.png"), new Vector2f(1f, 1f)));

                btnPlay.MousePressed += OnPlayButtonPressed;
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
