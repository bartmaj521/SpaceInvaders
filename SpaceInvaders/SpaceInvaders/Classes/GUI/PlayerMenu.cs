using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SpaceInvaders.Classes.GUI
{
    public class PlayerMenu : Menu
    {
        //private List<OurButton> workbenchBtnList = new List<OurButton>();
        //private List<OurLabel> workbenchLblList = new List<OurLabel>();
        private int firstElemIndex = 0;
        private List<Panel> panelList;
        private Panel powerupsPanel;
        private Panel activePanel;

        #region Singleton constructor
        private static PlayerMenu instance;

        public static PlayerMenu Instance()
        {
            if (instance == null)
            {
                instance = new PlayerMenu("");
            }
            return instance;
        }

        public static PlayerMenu Instance(string _playerName)
        {
            if (instance == null)
            {
                instance = new PlayerMenu(_playerName);
            }
            return instance;
        }
        private PlayerMenu(string _playerName) : base()
        {
            PlayerManager.Instance.PlayerName = _playerName;

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
                panelList = new List<Panel>();
                PlayerHud.Instance().PlayerInfo = PlayerManager.Instance;
                PlayerHud.Instance().update();




                #region workbenchPanel
                Panel workbenchPanel = new Panel();
                string[] statsPaths =
                {
                    "btnRepairSprite.png",
                    "btnSpeedSprite.png",
                    "btnArmorSprite.png",
                    "btnAccuracySprite.png",
                    "btnFireRateSprite.png",
                    "btnFireDmgSprite.png"
                };
                int[] statsArgs = { -1, 0, 1, 2, 3, 4 };

                for (int i = 0; i < statsPaths.Length; i++)
                {
                    OurButton button = new OurButton(new Texture(RESNAME + statsPaths[i]), new Vector2i(200, 199), "", 0);
                    button.componentID = "btnStat" + i;
                    button.ArgToPass = statsArgs[i];
                    workbenchPanel.panelBtnList.Add(button);
                }

                foreach (OurButton button in workbenchPanel.panelBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    button.MouseReleased += OnStatBtnMouseReleased;
                    componentList.Add(button);
                }
                for (int i = 0; i < 5; i++)
                {
                    workbenchPanel.panelBtnList[i].Visible = true;
                    workbenchPanel.panelBtnList[i].Active = true;
                    workbenchPanel.panelBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }
                for (int i = 0; i < 6; i++)
                {
                    workbenchPanel.panelLblList.Add(new OurLabel(new Texture(RESNAME + "blank.png"), PlayerManager.Instance.upgradeCost((stats)(i - 1)).ToString() + " $", 32));
                    workbenchPanel.panelLblList[i].setPosition(new Vector2f(workbenchPanel.panelBtnList[i].Position.X + 47, workbenchPanel.panelBtnList[i].Position.Y + 165));
                    workbenchPanel.panelLblList[i].Visible = false;
                    componentList.Add(workbenchPanel.panelLblList[i]);
                }
                for (int i = 0; i < 5; i++)
                {
                    workbenchPanel.panelLblList[i].Visible = true;
                }
                panelList.Add(workbenchPanel);
                #endregion

                #region shipshopPanel
                Panel shipshopPanel = new Panel();
                string[] shipsPaths =
                {
                    "ship1BtnSprite.png",
                    "ship2BtnSprite.png",
                    "ship3BtnSprite.png",
                    "ship4BtnSprite.png",
                    "ship5BtnSprite.png"
                };

                for (int i = 0; i < shipsPaths.Length; i++)
                {
                    OurButton button = new OurButton(new Texture(RESNAME + shipsPaths[i]), new Vector2i(200, 199), "", 0);
                    button.componentID = "btnShip" + i;
                    button.ArgToPass = i;
                    shipshopPanel.panelBtnList.Add(button);
                }
                foreach (OurButton button in shipshopPanel.panelBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    button.MouseReleased += OnShipBtnMouseReleased;
                    componentList.Add(button);
                }
                for (int i = 0; i < 5; i++)
                {
                    shipshopPanel.panelBtnList[i].Visible = true;
                    shipshopPanel.panelBtnList[i].Active = true;
                    shipshopPanel.panelBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }
                panelList.Add(shipshopPanel);
                #endregion

                #region powerupsPanel
                powerupsPanel = new Panel();
                string[] pupsPaths =
                {
                    "btnRocketSprite.png",
                    "btnLaserSprite.png",
                    "btnBombSprite.png",
                    "btnWaveSprite.png",
                    "btnShieldSprite.png"
                };
                for (int i = 0; i < pupsPaths.Length; i++)
                {
                    OurButton button = new OurButton(new Texture(RESNAME + pupsPaths[i]), new Vector2i(200, 199), "", 0);
                    button.componentID = "btnPUp" + i;
                    button.ArgToPass = i;
                    powerupsPanel.panelBtnList.Add(button);
                }

                foreach (OurButton button in powerupsPanel.panelBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    button.MouseReleased += OnShipBtnMouseReleased;
                    componentList.Add(button);
                }
                for (int i = 0; i < 5; i++)
                {
                    powerupsPanel.panelBtnList[i].Visible = true;
                    powerupsPanel.panelBtnList[i].Active = true;
                    powerupsPanel.panelBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }
                panelList.Add(powerupsPanel);
                #endregion

                ChangePanel(workbenchPanel);
                
                OurButton btnMission = new OurButton(new Texture(RESNAME + "btnMission.png"), new Vector2i(300, 99), "", 0);
                btnMission.setPosition(new Vector2f(633, 21));
                btnMission.componentID = "mission";
                componentList.Add(btnMission);
                
                OurButton btnLeftScroll = new OurButton(new Texture(RESNAME + "btnLeftSprite.png"), new Vector2i(40, 199), "", 0);
                btnLeftScroll.setPosition(new Vector2f(28, 647));
                btnLeftScroll.componentID = "left";
                btnLeftScroll.MouseReleased += OnBtnLeftScrollMouseReleased;
                componentList.Add(btnLeftScroll);
                
                OurButton btnRightScroll = new OurButton(new Texture(RESNAME + "btnRightSprite.png"), new Vector2i(40, 199), "", 0);
                btnRightScroll.componentID = "right";
                btnRightScroll.setPosition(new Vector2f(1212, 647));
                btnRightScroll.MouseReleased += OnBtnRightScrollMouseReleased;
                componentList.Add(btnRightScroll);

                OurButton btnScrollPanelLeft = new OurButton(new Texture(RESNAME + "btnLeftPanelSprite.png"), new Vector2i(40, 449), "", 0);
                btnScrollPanelLeft.setPosition(new Vector2f(28, 171));
                btnScrollPanelLeft.MouseReleased += OnScrollPanelLeftMouseReleased;
                componentList.Add(btnScrollPanelLeft);

                OurButton btnScrollPanelRight = new OurButton(new Texture(RESNAME + "btnRightPanelSprite.png"), new Vector2i(40, 449), "", 0);
                btnScrollPanelRight.setPosition(new Vector2f(1212, 171));
                btnScrollPanelRight.MouseReleased += OnScrollPanelRightMouseReleased;
                componentList.Add(btnScrollPanelRight);

                Vector2i buttonSize = new Vector2i(300, 99);
                uint fontSize = 40;

                //OurButton btnExit = new OurButton(new Texture(RESNAME + "buttonSprite.png"), buttonSize, "wyjdz", fontSize);
                //btnExit.setPosition(new Vector2f(100, 100));
                //btnExit.MouseReleased += OnBtnExitMouseReleased;
                //componentList.Add(btnExit);

                //OurButton btnSaveShipInfo = new OurButton(new Texture(RESNAME + "buttonSprite.png"), buttonSize, "Zapisz", fontSize);
                //btnSaveShipInfo.setPosition(new Vector2f(100, 200));
                //btnSaveShipInfo.MouseReleased += OnBtnSaveMouseReleased;
                //componentList.Add(btnSaveShipInfo);

                //cursor
                cursor = Cursor.Instance(new Texture(RESNAME + "cursor.png"), new Vector2f(1f, 1f));

            }

        }

        private void OnShipBtnMouseReleased(object sender, btnReleasedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void ChangePanel(Panel panel)
        {
            foreach (var pnl in panelList)
            {
                pnl.Disable();
            }
            activePanel = panel;
            activePanel.Enable();
        }

        private void OnScrollPanelRightMouseReleased(object sender, btnReleasedEventArgs e)
        {
            int index = panelList.IndexOf(activePanel);
            if (++index >= panelList.Count)
            {
                activePanel = panelList[0];
            }
            else
            {
                activePanel = panelList[index];
            }

            if (activePanel == panelList[0])
            {
                EnableScrollBtns();
            }
            else
            {
                DisableScrollBtns();
            }
            ChangePanel(activePanel);
        }

        private void OnScrollPanelLeftMouseReleased(object sender, btnReleasedEventArgs e)
        {
            int index = panelList.IndexOf(activePanel);
            if (--index < 0)
            {
                activePanel = panelList[panelList.Count - 1];
            }
            else
            {
                activePanel = panelList[index];
            }
            if(activePanel == panelList[0])
            {
                EnableScrollBtns();
            }
            else
            {
                DisableScrollBtns();
            }
            ChangePanel(activePanel);
        }

        private void DisableScrollBtns()
        {
            var btns = from x in componentList
                       where x.componentID == "left" || x.componentID == "right"
                       select x;
            foreach (var btn in btns)
            {
                btn.Active = false;
                btn.Visible = false;
            }
        }

        private void EnableScrollBtns()
        {
            var btns = from x in componentList
                       where x.componentID == "left" || x.componentID == "right"
                       select x;
            foreach (var btn in btns)
            {
                btn.Active = true;
                btn.Visible = true;
            }
        }

        //wczytanie stanu gry z pliku
        public void ReadDataFromFile(object sender, btnReleasedEventArgs e)
        {
            using (Stream stream = File.Open("ship.txt", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                PlayerManager.Instance = (PlayerManager)bf.Deserialize(stream);
                PlayerHud.Instance().PlayerInfo = PlayerManager.Instance;
            }
        }

        //zapis stanu gry do pliku
        private void OnBtnSaveMouseReleased(object sender, btnReleasedEventArgs e)
        {
            using (Stream stream = File.Open("ship.txt", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, PlayerManager.Instance);
            }
        }

        //ulepszanie statów
        private void OnStatBtnMouseReleased(object sender, btnReleasedEventArgs e)
        {
            stats stat;
            if (e.arg >= 0)
            {
                stat = (stats)e.arg;
                PlayerManager.Instance.upgradeShip(stat);
            }
            if (e.arg == -1)
                PlayerManager.Instance.repairShip(0.1f);
        }

        //przesuwanie listy
        private void OnBtnRightScrollMouseReleased(object sender, EventArgs e)
        {
            firstElemIndex++;
            foreach (OurButton button in activePanel.panelBtnList)
            {
                button.Visible = false;
                button.Active = false;
            }
            foreach (OurLabel label in activePanel.panelLblList)
            {
                label.Visible = false;
            }
            for (int i = 0; i < 5; i++)
            {
                activePanel.panelBtnList[i + firstElemIndex].Visible = true;
                activePanel.panelBtnList[i + firstElemIndex].Active = true;
                activePanel.panelBtnList[i + firstElemIndex].setPosition(new Vector2f(80 + 230 * i, 647));

                activePanel.panelLblList[i + firstElemIndex].Visible = true;
                activePanel.panelLblList[i + firstElemIndex].setPosition(new Vector2f(activePanel.panelBtnList[i + firstElemIndex].Position.X + 47, activePanel.panelBtnList[i + firstElemIndex].Position.Y + 165));
            }
        }

        private void OnBtnLeftScrollMouseReleased(object sender, EventArgs e)
        {
            firstElemIndex--;
            foreach (OurButton button in activePanel.panelBtnList)
            {
                button.Visible = false;
                button.Active = false;
            }
            foreach (OurLabel label in activePanel.panelLblList)
            {
                label.Visible = false;
            }
            for (int i = 0; i < 5; i++)
            {
                activePanel.panelBtnList[i + firstElemIndex].Visible = true;
                activePanel.panelBtnList[i + firstElemIndex].Active = true;
                activePanel.panelBtnList[i + firstElemIndex].setPosition(new Vector2f(80 + 230 * i, 647));

                activePanel.panelLblList[i + firstElemIndex].Visible = true;
                activePanel.panelLblList[i + firstElemIndex].setPosition(new Vector2f(activePanel.panelBtnList[i + firstElemIndex].Position.X + 47, activePanel.panelBtnList[i + firstElemIndex].Position.Y + 165));
            }

        }

        //wyjscie
        private void OnBtnExitMouseReleased(object sender, EventArgs e)
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
            sceneManager.window.Draw(PlayerHud.Instance());
            sceneManager.window.Draw(cursor);
        }
        public override void updateComponents(SceneManager sceneManager)
        {
            base.updateComponents(sceneManager);
            if (activePanel == panelList[0])
            {
                if (firstElemIndex == 0)
                {
                    componentList.Find(x => x.componentID == "left").Visible = false;
                    componentList.Find(x => x.componentID == "left").Active = false;
                }
                else
                {
                    componentList.Find(x => x.componentID == "left").Visible = true;
                    componentList.Find(x => x.componentID == "left").Active = true;
                }
                if (firstElemIndex == panelList[0].panelBtnList.Count - 5)
                {
                    componentList.Find(x => x.componentID == "right").Visible = false;
                    componentList.Find(x => x.componentID == "right").Active = false;
                }
                else
                {
                    componentList.Find(x => x.componentID == "right").Visible = true;
                    componentList.Find(x => x.componentID == "right").Active = true;
                } 
            }

            for (int i = 0; i < panelList[0].panelLblList.Count; i++)
            {
                panelList[0].panelLblList[i].Text = PlayerManager.Instance.upgradeCost((stats)(i - 1)).ToString() + " $";
            }
            PlayerHud.Instance().update();
            cursor.update();
        }

        class Panel
        {
            public List<OurButton> panelBtnList = new List<OurButton>();
            public List<OurLabel> panelLblList = new List<OurLabel>();
            public int firstelemIndex;
            public Panel()
            {

            }
            public void Disable()
            {
                foreach (var btn in panelBtnList)
                {
                    btn.Active = false;
                    btn.Visible = false;
                }
                foreach (var lbl in panelLblList)
                {
                    lbl.Visible = false;
                    lbl.Active = false;
                }
            }
            public void Enable()
            {
                int rangeBtn = Math.Min(panelBtnList.Count, 5);
                int rangeLbl = Math.Min(panelLblList.Count, 5);
                for (int i = 0; i < rangeBtn; i++)
                {
                    panelBtnList[i].Active = true;
                    panelBtnList[i].Visible = true;
                }
                for (int i = 0; i < rangeLbl; i++)
                {
                    panelLblList[i].Active = true;
                    panelLblList[i].Visible = true;
                }
            }

        }
    }
}
