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
                //labels
                string lblWelcomeText = "Witaj na pokladzie Kapitanie!\nNie bylo cie przez dlugi czas\nZanim wyruszymy w podroz\nPrzypomnij nam swoje imie".ToLower();
                OurLabel lblWelcome = new OurLabel(new Texture("labelBg2.png"), lblWelcomeText, 30);
                lblWelcome.setPosition(new Vector2f(window.Size.X*0.45f-lblWelcome.Size.X/2, window.Size.Y*0.4f-lblWelcome.Size.Y/2));
                lblWelcome.Visible = false;
                lblWelcome.componentID = "label";
                componentList.Add(lblWelcome);


                //texboxes
                OurTextbox txbUsername = new OurTextbox(new Texture("txbSprite.png"), new Vector2f(lblWelcome.Size.X, 0), 30);
                txbUsername.setPosition(new Vector2f(lblWelcome.Position.X, lblWelcome.Position.Y+lblWelcome.Size.Y+10));
                txbUsername.componentID = "textbox";
                txbUsername.Visible = false;
                componentList.Add(txbUsername);


                //obrazek
                OurImage imgCapral = new OurImage(new Texture("capral.png"), new Vector2f(230, 230));
                imgCapral.setPosition(new Vector2f(lblWelcome.Position.X + lblWelcome.Size.X +5, lblWelcome.Position.Y-20));
                imgCapral.componentID = "capral";
                imgCapral.Visible = false;
                componentList.Add(imgCapral);
                

                
                

                //buttons
                Vector2i buttonSize = new Vector2i(300, 99);
                uint fontSize = 40;

                OurButton btnNewGame = new OurButton(new Texture("buttonSprite.png"),buttonSize,"nowa gra",fontSize);
                btnNewGame.setPosition(new Vector2f(window.Size.X * 0.67f+btnNewGame.Size.X/2, window.Size.Y * 0.30f+btnNewGame.Size.Y/2));
                componentList.Add(btnNewGame);

                OurButton btnLoadGame = new OurButton(new Texture("buttonSprite.png"),buttonSize,"wczytaj gre", 35);
                btnLoadGame.setPosition(new Vector2f(window.Size.X * 0.67f + btnNewGame.Size.X / 2, window.Size.Y * 0.45f + btnNewGame.Size.Y / 2));
                componentList.Add(btnLoadGame);

                OurButton btnOptions = new OurButton(new Texture("buttonSprite.png"),buttonSize,"opcje", fontSize);
                btnOptions.setPosition(new Vector2f(window.Size.X * 0.67f + btnNewGame.Size.X / 2, window.Size.Y * 0.6f + btnNewGame.Size.Y / 2));
                componentList.Add(btnOptions);

                OurButton btnExit = new OurButton(new Texture("buttonSprite.png"),buttonSize,"wyjdz", fontSize);
                btnExit.setPosition(new Vector2f(window.Size.X * 0.67f + btnNewGame.Size.X / 2, window.Size.Y * 0.75f + btnNewGame.Size.Y / 2));
                componentList.Add(btnExit);


                //cursor
                cursor = Cursor.Instance(new Texture("cursor.png"), new Vector2f(1f, 1f));



                //events
                txbUsername.TextConfirmed += onTxbUserNameTextConfirmed;
                btnNewGame.MousePressed += OnNewGameButtonPressed;
                btnExit.MouseReleased += OnExitButtonReleased;


                initialized = true;
            }

        }

        private void onTxbUserNameTextConfirmed(object sender, TextboxEventArgs e)
        {
            sceneManager.changeScene(PlayerMenu.Instance(e.Text));
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
        private void OnNewGameButtonPressed(object sender, EventArgs e)
        {
            foreach(OurButton button in componentList.OfType<OurButton>())
            {
                button.Visible = false;
            }
            var txb = (componentList.Find(x => x.componentID == "textbox") as OurTextbox);
            var lbl = (componentList.Find(x => x.componentID == "label") as OurLabel);
            var capral = (componentList.Find(x => x.componentID == "capral") as OurImage);
            capral.Visible = true;
            lbl.Visible = true;
            txb.Selected = true;
            txb.Visible = true;
        }
    }
}
