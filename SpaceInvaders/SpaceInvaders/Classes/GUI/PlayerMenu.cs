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
    class PlayerMenu:Menu
    {
        private PlayerHud playerHud;
        private List<OurButton> workbenchBtnList = new List<OurButton>();
        private int firstElemIndex = 0;
        public string playerName { get; set; }

        #region Singleton constructor
        private static PlayerMenu instance;

        public static PlayerMenu Instance()
        {
            return instance;
        }

        public static PlayerMenu Instance(string _playerName)
        {
            if(instance==null)
            {
                instance = new PlayerMenu(_playerName);
            }
            return instance;
        }
        private PlayerMenu(string _playerName):base()
        {
            playerName = _playerName;

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
        public override void initialize(RenderWindow window)
        {
            if (!initialized)
            {
                playerHud = PlayerHud.Instance();
                playerHud.ShipName = "Audi Yeah";
                playerHud.ShipValue = 55400;
                playerHud.MaxShipSpeed = 1;
                playerHud.ShipSpeed = 0;
                playerHud.MaxFireSpeed = 2;
                playerHud.FireSpeed = 2;
                playerHud.MaxAccuracy = 5;
                playerHud.Accuracy = 1;
                playerHud.MaxArmor = 4;
                playerHud.Armor = 2;
                playerHud.update();


                workbenchBtnList.Add(new OurButton(new Texture("btnRepairSprite.png"), new Vector2i(200, 199), "", 0));
                workbenchBtnList.Add(new OurButton(new Texture("btnSpeedSprite.png"), new Vector2i(200, 199), "", 0));
                workbenchBtnList.Add(new OurButton(new Texture("btnArmorSprite.png"), new Vector2i(200, 199), "", 0));
                workbenchBtnList.Add(new OurButton(new Texture("btnFireDmgSprite.png"), new Vector2i(200, 199), "", 0));
                workbenchBtnList.Add(new OurButton(new Texture("btnAccuracySprite.png"), new Vector2i(200, 199), "", 0));
                workbenchBtnList.Add(new OurButton(new Texture("btnFireRateSprite.png"), new Vector2i(200, 199), "", 0));

                foreach(OurButton button in workbenchBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    componentList.Add(button);
                }
                for (int i = 0; i < 5; i++)
                {
                    workbenchBtnList[i].Visible = true;
                    workbenchBtnList[i].Active = true;
                    workbenchBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }


                OurButton btnLeftScroll = new OurButton(new Texture("btnLeftSprite.png"), new Vector2i(40, 199), "", 0);
                btnLeftScroll.setPosition(new Vector2f(28, 647));
                btnLeftScroll.componentID = "left";
                btnLeftScroll.MouseReleased += OnBtnLeftScrollMouseReleased;
                componentList.Add(btnLeftScroll);

                OurButton btnRightScroll = new OurButton(new Texture("btnRightSprite.png"),new Vector2i(40, 199), "", 0);
                btnRightScroll.componentID = "right";
                btnRightScroll.setPosition(new Vector2f(1212, 647));
                btnRightScroll.MouseReleased += OnBtnRightScrollMouseReleased;
                componentList.Add(btnRightScroll);


                Vector2i buttonSize = new Vector2i(300, 99);
                uint fontSize = 40;
                OurButton btnExit = new OurButton(new Texture("buttonSprite.png"), buttonSize, "wyjdz", fontSize);
                btnExit.setPosition(new Vector2f(100,100));
                btnExit.MouseReleased += OnBtnExitMouseReleased;
                componentList.Add(btnExit);

                //cursor
                cursor = Cursor.Instance(new Texture("cursor.png"), new Vector2f(1f, 1f));

            }

        }
        //eventy

        //przesuwanie listy
        private void OnBtnRightScrollMouseReleased(object sender, EventArgs e)
        {
            firstElemIndex++;
            foreach (OurButton button in workbenchBtnList)
            {
                button.Visible = false;
                button.Active = false;
            }
            for (int i = 0; i < 5; i++)
            {
                workbenchBtnList[i+firstElemIndex].Visible = true;
                workbenchBtnList[i + firstElemIndex].Active = true;
                workbenchBtnList[i+firstElemIndex].setPosition(new Vector2f(80 + 230 * i, 647));
            }
        }

        private void OnBtnLeftScrollMouseReleased(object sender, EventArgs e)
        {
            firstElemIndex--;
            foreach (OurButton button in workbenchBtnList)
            {
                button.Visible = false;
                button.Active = false;
            }
            for (int i = 0; i < 5; i++)
            {
                workbenchBtnList[i+firstElemIndex].Visible = true;
                workbenchBtnList[i + firstElemIndex].Active = true;
                workbenchBtnList[i+firstElemIndex].setPosition(new Vector2f(80 + 230 * i, 647));
            }
        }

        //wyjscie
        private void OnBtnExitMouseReleased(object sender, EventArgs e)
        {
            sceneManager.quit();
        }

        //obsluga zwolnienia przycisku exit
        private void OnExitButtonReleased(object sender, EventArgs e)
        {
            sceneManager.quit();
        }



        public override void cleanup()
        {
            componentList.Clear();
        }
        public override void drawComponents(SceneManager sceneManager)
        {
            base.drawComponents(sceneManager);
            sceneManager.window.Draw(playerHud);
            sceneManager.window.Draw(cursor);
        }
        public override void updateComponents(SceneManager sceneManager)
        {
            base.updateComponents(sceneManager);
            if(firstElemIndex == 0)
            {
                componentList.Find(x => x.componentID == "left").Visible = false;
                componentList.Find(x => x.componentID == "left").Active = false;
            }
            else
            {
                componentList.Find(x => x.componentID == "left").Visible = true;
                componentList.Find(x => x.componentID == "left").Active = true;
            }
            if(firstElemIndex ==workbenchBtnList.Count - 5)
            {
                componentList.Find(x => x.componentID == "right").Visible = false;
                componentList.Find(x => x.componentID == "right").Active = false;
            }
            else
            {
                componentList.Find(x => x.componentID == "right").Visible = true;
                componentList.Find(x => x.componentID == "right").Active = true;
            }
            playerHud.update();
            cursor.update();
        }
    }
}
