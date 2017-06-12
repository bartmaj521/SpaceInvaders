using System;

using System.Linq;
using SFML.Graphics;
using SFML.System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.IO;

using System.Windows.Forms;


namespace SpaceInvaders.Classes.GUI
{
    public class MainMenu : Menu
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

        //inicjalizacja, dodawanie komponentów do listy komponentów
        public override void initialize(RenderWindow window)
        {
            if (!initialized)
            {
                #region Labels
                string lblWelcomeText = "Witaj na pokladzie Kapitanie!\nNie bylo cie przez dlugi czas\nZanim wyruszymy w podroz\nPrzypomnij nam swoje imie".ToLower();
                OurLabel lblWelcome = new OurLabel(new Texture(ResourcesManager.resourcesPath + "labelBg2.png"), lblWelcomeText, 30);
                lblWelcome.setPosition(new Vector2f(window.Size.X * 0.45f - lblWelcome.Size.X / 2, window.Size.Y * 0.4f - lblWelcome.Size.Y / 2));
                lblWelcome.Visible = false;
                lblWelcome.componentID = "label";
                componentList.Add(lblWelcome);
                #endregion
                
                #region Textboxes
                OurTextbox txbUsername = new OurTextbox(new Texture(ResourcesManager.resourcesPath + "txbSprite.png"), new Vector2f(lblWelcome.Size.X, 0), 30);
                txbUsername.setPosition(new Vector2f(lblWelcome.Position.X, lblWelcome.Position.Y+lblWelcome.Size.Y+10));
                txbUsername.TextConfirmed += onTxbUserNameTextConfirmed;
                txbUsername.componentID = "textbox";
                txbUsername.Visible = false;
                componentList.Add(txbUsername);
                #endregion

                #region Images

                OurImage imgCapral = new OurImage(new Texture(ResourcesManager.resourcesPath + "capral.png"), new Vector2f(230, 230));
                imgCapral.setPosition(new Vector2f(lblWelcome.Position.X + lblWelcome.Size.X +5, lblWelcome.Position.Y-20));
                imgCapral.componentID = "capral";
                imgCapral.Visible = false;
                componentList.Add(imgCapral);

                #endregion
                
                #region Buttons

                Vector2i buttonSize = new Vector2i(300, 99);
                uint fontSize = 40;

                OurButton btnNewGame = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"),buttonSize,"nowa gra",fontSize);

                btnNewGame.MouseReleased += OnNewGameButtonReleased;

                btnNewGame.setPosition(new Vector2f(window.Size.X * 0.67f+btnNewGame.Size.X/2, window.Size.Y * 0.30f+btnNewGame.Size.Y/2));
                componentList.Add(btnNewGame);

                OurButton btnLoadGame = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"),buttonSize,"wczytaj gre", 35);
                btnLoadGame.setPosition(new Vector2f(window.Size.X * 0.67f + btnNewGame.Size.X / 2, window.Size.Y * 0.45f + btnNewGame.Size.Y / 2));

                btnLoadGame.MouseReleased += OnBtnLoadMouseReleased;

                componentList.Add(btnLoadGame);

                OurButton btnOptions = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"),buttonSize,"opcje", fontSize);
                btnOptions.setPosition(new Vector2f(window.Size.X * 0.67f + btnNewGame.Size.X / 2, window.Size.Y * 0.6f + btnNewGame.Size.Y / 2));
                componentList.Add(btnOptions);

                OurButton btnExit = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"),buttonSize,"wyjdz", fontSize);
                btnExit.setPosition(new Vector2f(window.Size.X * 0.67f + btnNewGame.Size.X / 2, window.Size.Y * 0.75f + btnNewGame.Size.Y / 2));

                btnExit.MouseReleased += OnExitButtonReleased;
                componentList.Add(btnExit);
                #endregion


                //cursor
                cursor = Cursor.Instance(new Texture(ResourcesManager.resourcesPath + "cursor.png"), new Vector2f(1f, 1f));

                initialized = true;
            }

        }

        //eventy
        private void OnNewGameButtonReleased(object sender, EventArgs e)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
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
        private void OnBtnLoadMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            try
            {
                PlayerMenu.Instance().cleanUp();  //napisac koniecznie wczytywanie teksturki jak sie wczytuje stan gry z pliku
                using (Stream stream = File.Open("ship.txt", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    PlayerManager.Instance = (PlayerManager)bf.Deserialize(stream);
                    int index = PlayerManager.Instance.currentShip;
                    PlayerManager.Instance.ShipInfo.ShipTexture = new Texture(ResourcesManager.resourcesPath + PlayerManager.Instance.ShipPrefabs[index].TexturePath);
                    PlayerHud.Instance().PlayerInfo = PlayerManager.Instance;
                }
                sceneManager.changeScene(PlayerMenu.Instance());
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Brak pliku zapisu");
            }
        }
        private void onTxbUserNameTextConfirmed(object sender, TextboxEventArgs e)
        {
            PlayerMenu.Instance().cleanUp();
            sceneManager.changeScene(PlayerMenu.Instance(e.Text));
        }
        private void OnExitButtonReleased(object sender, EventArgs e)
        {
            sceneManager.quit();
        }

        //wyczyszczenie listy componentów i wymuszenie ponownej inicjalizacji
        public void cleanup()
        {
            initialized = false;
            componentList.Clear();
        }
    }
}
