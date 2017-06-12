using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;

using SFML.Window;

using System.Xml;

using System.Runtime.Serialization;
using System.IO;
using System.Xml;

using System.Runtime.Serialization.Formatters.Binary;
using SFML.Window;

namespace SpaceInvaders.Classes.GUI
{
    public class PlayerMenu : Menu
    {
        private int firstElemIndex = 0;
        private List<Panel> panelList;

        private Panel activePanel;
        private string[] workbenchInfo;
        private string[] powerupInfo;

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

        //inicjalizacja, dodawanie komponentów do listy komponentów
        public override void initialize(RenderWindow window)
        {
            if (!initialized)
            {
                panelList = new List<Panel>();
                PlayerHud.Instance().PlayerInfo = PlayerManager.Instance;
                PlayerHud.Instance().ShipImg = new Sprite(new Texture(PlayerManager.Instance.ShipInfo.ShipTexture));
                PlayerHud.Instance().ShipImg.Position = new Vector2f(1428 - PlayerHud.Instance().ShipImg.Texture.Size.X/2, 200 - PlayerHud.Instance().ShipImg.Texture.Size.Y / 2);
                PlayerHud.Instance().update();

                OurLabel pnlInfo = new OurLabel(new Texture(ResourcesManager.resourcesPath + "infoPanel.png"), "", 0, new Vector2i(1114, 452));
                pnlInfo.setPosition(new Vector2f(83, 171));
                componentList.Add(pnlInfo);

                #region workbenchPanel
                Panel workbenchPanel = new Panel();
                workbenchPanel.PanelName = "Warsztat";
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
                    OurButton button = new OurButton(new Texture(ResourcesManager.resourcesPath + statsPaths[i]), new Vector2i(200, 199), "", 0);
                    button.componentID = "btnStat" + i;
                    button.ArgToPass = statsArgs[i];
                    workbenchPanel.panelBtnList.Add(button);
                }

                foreach (OurButton button in workbenchPanel.panelBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    button.MouseHovered += OnStatBtnMouseHovered;
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
                    workbenchPanel.panelLblList.Add(new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), PlayerManager.Instance.upgradeCost((stats)(i - 1)).ToString() + " cr", 28));

                    workbenchPanel.panelLblList[i].setPosition(new Vector2f(workbenchPanel.panelBtnList[i].Position.X + 35, workbenchPanel.panelBtnList[i].Position.Y + 165));
                    workbenchPanel.panelLblList[i].Visible = false;
                    componentList.Add(workbenchPanel.panelLblList[i]);
                }
                for (int i = 0; i < 5; i++)
                {
                    workbenchPanel.panelLblList[i].Visible = true;
                }

                workbenchInfo = readPanelInfo("workbenchInfo.xml");
                OurLabel workbenchDisplayedInfo = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 32);
                workbenchDisplayedInfo.TextAlign = align.left;
                workbenchDisplayedInfo.componentID = "workbenchDisplayedInfo";
                workbenchDisplayedInfo.setPosition(new Vector2f(250,300));
                componentList.Add(workbenchDisplayedInfo);
                workbenchPanel.panelInfoLblList.Add(workbenchDisplayedInfo);

                panelList.Add(workbenchPanel);
                #endregion

                #region shipshopPanel
                Panel shipshopPanel = new Panel();
                shipshopPanel.PanelName = "Salon";
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
                    OurButton button = new OurButton(new Texture(ResourcesManager.resourcesPath + shipsPaths[i]), new Vector2i(200, 199), "", 0);
                    button.componentID = "btnShip" + i;
                    button.ArgToPass = i;
                    shipshopPanel.panelBtnList.Add(button);
                }
                foreach (OurButton button in shipshopPanel.panelBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    button.MouseHovered += OnShipMouseHovered;
                    button.MouseReleased += OnShipBtnMouseReleased;
                    componentList.Add(button);
                }

                
                for (int i = 0; i < 5; i++)
                {
                    shipshopPanel.panelBtnList[i].Visible = true;
                    shipshopPanel.panelBtnList[i].Active = true;
                    shipshopPanel.panelBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }
                for (int i = 0; i < 5; i++)
                {
                    shipshopPanel.panelLblList.Add(new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 28));
                    shipshopPanel.panelLblList[i].Text = PlayerManager.Instance.ShipPrefabs[i].Price + " cr";
                    shipshopPanel.panelLblList[i].setPosition(new Vector2f(shipshopPanel.panelBtnList[i].Position.X + 100, shipshopPanel.panelBtnList[i].Position.Y + 180));
                    shipshopPanel.panelLblList[i].Visible = false;
                    componentList.Add(shipshopPanel.panelLblList[i]);
                }

                OurLabel ShipshopDisplayedInfo = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 30);
                ShipshopDisplayedInfo.componentID = "ShipShopDisplayedInfo";
                ShipshopDisplayedInfo.Text = "";
                ShipshopDisplayedInfo.setPosition(new Vector2f(470, 400));
                componentList.Add(ShipshopDisplayedInfo);
                shipshopPanel.panelInfoLblList.Add(ShipshopDisplayedInfo);

                OurLabel shipImg = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"),"",0,new Vector2i(160,117));
                shipImg.componentID = "ShipShopShipImg";
                shipImg.setPosition(new Vector2f(900, 330));
                componentList.Add(shipImg);
                shipshopPanel.panelInfoLblList.Add(shipImg);

                panelList.Add(shipshopPanel);
                #endregion

                #region powerupsPanel

                Panel powerupsPanel = new Panel();

                powerupsPanel.PanelName = "Sklep";
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
                    OurButton button = new OurButton(new Texture(ResourcesManager.resourcesPath + pupsPaths[i]), new Vector2i(200, 199), "", 0);
                    button.componentID = "btnPUp" + i;
                    button.ArgToPass = i;
                    powerupsPanel.panelBtnList.Add(button);
                }

                foreach (OurButton button in powerupsPanel.panelBtnList)
                {
                    button.Visible = false;
                    button.Active = false;
                    button.MouseHovered += OnPowerupMouseHovered;
                    button.MouseReleased += OnPowerupBtnMouseReleased;
                    componentList.Add(button);
                }
                for (int i = 0; i < 5; i++)
                {
                    powerupsPanel.panelBtnList[i].Visible = true;
                    powerupsPanel.panelBtnList[i].Active = true;
                    powerupsPanel.panelBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }

                for (int i = 0; i < 5; i++)
                {
                    powerupsPanel.panelLblList.Add(new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 28));
                    powerupsPanel.panelLblList[i].Text = PlayerManager.Instance.powerupCost((powerups)i) + " cr";
                    powerupsPanel.panelLblList[i].setPosition(new Vector2f(powerupsPanel.panelBtnList[i].Position.X + 100, powerupsPanel.panelBtnList[i].Position.Y + 180));
                    powerupsPanel.panelLblList[i].Visible = false;
                    componentList.Add(powerupsPanel.panelLblList[i]);
                }

                powerupInfo = readPanelInfo("powerupInfo.xml");
                OurLabel powerupDisplayedInfo = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 32);
                powerupDisplayedInfo.TextAlign = align.left;
                powerupDisplayedInfo.componentID = "powerupDisplayedInfo";
                powerupDisplayedInfo.setPosition(new Vector2f(250, 300));
                componentList.Add(powerupDisplayedInfo);
                powerupsPanel.panelInfoLblList.Add(powerupDisplayedInfo);

                OurLabel currentPowerups = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"),"",32);
                currentPowerups.componentID = "currentPowerups";
                currentPowerups.TextAlign = align.left;
                currentPowerups.Text = "Posiadasz: ";
                currentPowerups.setPosition(new Vector2f(250, 520));
                componentList.Add(currentPowerups);

                powerupsPanel.panelInfoLblList.Add(currentPowerups);


                panelList.Add(powerupsPanel);
                #endregion

                activePanel = workbenchPanel;
                ChangePanel(workbenchPanel);


                #region SomeButtons
                OurLabel pnlName = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), workbenchPanel.PanelName, 60);
                pnlName.setPosition(new Vector2f(640 - pnlName.Size.X / 2, 210));
                pnlName.TextColor = new Color(225, 57, 6);
                pnlName.componentID = "panelName";
                componentList.Add(pnlName);


                OurButton btnMission = new OurButton(new Texture(ResourcesManager.resourcesPath + "btnMission.png"), new Vector2i(300, 99), "", 0);
                btnMission.setPosition(new Vector2f(633, 21));
                btmMission.MouseReleased += OnBtnMissionMouseReleased;
                btnMission.componentID = "mission";

                componentList.Add(btnMission);

                OurButton btnLeftScroll = new OurButton(new Texture(ResourcesManager.resourcesPath + "btnLeftSprite.png"), new Vector2i(40, 199), "", 0);
                btnLeftScroll.setPosition(new Vector2f(28, 647));
                btnLeftScroll.componentID = "left";
                btnLeftScroll.MouseReleased += OnBtnLeftScrollMouseReleased;
                componentList.Add(btnLeftScroll);

                OurButton btnRightScroll = new OurButton(new Texture(ResourcesManager.resourcesPath + "btnRightSprite.png"), new Vector2i(40, 199), "", 0);
                btnRightScroll.componentID = "right";
                btnRightScroll.setPosition(new Vector2f(1212, 647));
                btnRightScroll.MouseReleased += OnBtnRightScrollMouseReleased;
                componentList.Add(btnRightScroll);



                OurButton btnScrollPanelLeft = new OurButton(new Texture(ResourcesManager.resourcesPath + "btnLeftPanelSprite.png"), new Vector2i(40, 449), "", 0);
                btnScrollPanelLeft.setPosition(new Vector2f(28, 171));
                btnScrollPanelLeft.MouseReleased += OnScrollPanelLeftMouseReleased;
                componentList.Add(btnScrollPanelLeft);

                OurButton btnScrollPanelRight = new OurButton(new Texture(ResourcesManager.resourcesPath + "btnRightPanelSprite.png"), new Vector2i(40, 449), "", 0);
                btnScrollPanelRight.setPosition(new Vector2f(1212, 171));
                btnScrollPanelRight.MouseReleased += OnScrollPanelRightMouseReleased;
                componentList.Add(btnScrollPanelRight);
                #endregion

                //cursor
                cursor = Cursor.Instance(new Texture(ResourcesManager.resourcesPath + "cursor.png"), new Vector2f(1f, 1f));

                initialized = true;
            }

        }

        private void OnBtnMissionMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            SceneManager.Instance().changeScene(GameScene.Instance());
        }
        
        //wyświetlanie informacji o przyciskach
        private void OnPowerupMouseHovered(object sender, BtnReleasedEventArgs e)
        {
            var lbl = componentList.Find(x => x.componentID == "powerupDisplayedInfo") as OurLabel;
            lbl.Text = powerupInfo[e.arg];
            (componentList.Find(x => x.componentID == "currentPowerups") as OurLabel).Text = string.Format("Posiadasz: {0}", PlayerManager.Instance.Powerups[e.arg]);
        }
        private void OnStatBtnMouseHovered(object sender, BtnReleasedEventArgs e)
        {
            var lbl = componentList.Find(x => x.componentID == "workbenchDisplayedInfo") as OurLabel;
            lbl.Text = workbenchInfo[e.arg + 1];
        }
        private void OnShipMouseHovered(object sender, BtnReleasedEventArgs e)
        {
            var ship = PlayerManager.Instance.ShipPrefabs[e.arg];
            (componentList.Find(x=>x.componentID == "ShipShopDisplayedInfo") as OurLabel).Text = string.Format("{8}\n\nPredkosc statku: {0}\nOpancerzenie: {1}\nCelnosc: {2}\nSzybkostrzelnosc: {3}\nObrazenia broni: {4}\n\nPredkosc bazowa: {5}c\nWytrzymalosc bazowa: {6}\n\nCena: {7}", ship.MaxUpgrades[0], ship.MaxUpgrades[1], ship.MaxUpgrades[2], ship.MaxUpgrades[3], ship.MaxUpgrades[4], ship.DefaultSpeed, ship.DefaultHealth, ship.Price,ship.ShipName);
            componentList.Find(x => x.componentID == "ShipShopShipImg").ChangeTexture(new Texture(ResourcesManager.resourcesPath + ship.TexturePath));
            componentList.Find(x => x.componentID == "ShipShopShipImg").setPosition(new Vector2f(900, 300));
        }


        //obsluga przycisniecia przyciskow
        //kupienie statku
        private void OnShipBtnMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            PlayerManager.Instance.changeShip(e.arg);
            PlayerHud.Instance().ShipImg = new Sprite(new Texture(PlayerManager.Instance.ShipInfo.ShipTexture));
            PlayerHud.Instance().ShipImg.Position = new Vector2f(1428 - PlayerHud.Instance().ShipImg.Texture.Size.X / 2, 200 - PlayerHud.Instance().ShipImg.Texture.Size.Y / 2);
            PlayerHud.Instance().update();
        }
        //ulepszanie statów
        private void OnStatBtnMouseReleased(object sender, BtnReleasedEventArgs e)
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
        //kupowanie powerupów
        private void OnPowerupBtnMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            PlayerManager.Instance.addPowerup((powerups)e.arg);
            (componentList.Find(x => x.componentID == "currentPowerups") as OurLabel).Text = string.Format("Posiadasz: {0}", PlayerManager.Instance.Powerups[e.arg]);
        }

/*
        //wczytanie stanu gry z pliku
        public void ReadDataFromFile(object sender, BtnReleasedEventArgs e)
        {
            using (Stream stream = File.Open("ship.txt", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                PlayerManager.Instance = (PlayerManager)bf.Deserialize(stream);
                PlayerHud.Instance().PlayerInfo = PlayerManager.Instance;
            }
        }
        //zapis stanu gry do pliku
        private void SaveDataToFile(object sender, BtnReleasedEventArgs e)
        {
            using (Stream stream = File.Open("ship.txt", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, PlayerManager.Instance);
            }
        }
        //wczytanie informacji o przyciskach z panelu workbench
        private string[] readPanelInfo(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);

            XmlNode mainNode = xmldoc.SelectSingleNode("/main");

            int count = mainNode.ChildNodes.Count;
            XmlNode currNode;
            string[] info = new string[count];
            for (int i = 0; i < count; i++)
            {
                currNode = mainNode.SelectSingleNode(string.Format("n{0}", i));
                info[i] = currNode.InnerText;
            }

            return info;
        }*/
        
        //przesuwanie listy i zmiana paneli
        //male przyciski
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

        //zmiana panelu
        private void ChangePanel(Panel panel)
        {
            foreach (var pnl in panelList)
            {
                pnl.Disable();
            }
            (componentList.Find(x => x.componentID == "ShipShopDisplayedInfo") as OurLabel).Text = "";
            (componentList.Find(x => x.componentID == "workbenchDisplayedInfo") as OurLabel).Text = "";
            (componentList.Find(x => x.componentID == "powerupDisplayedInfo") as OurLabel).Text = "";
            (componentList.Find(x => x.componentID == "currentPowerups") as OurLabel).Text = "";

            componentList.Find(x => x.componentID == "ShipShopShipImg").ChangeTexture(new Texture(ResourcesManager.resourcesPath + "blank.png"));
            firstElemIndex = 0;
            activePanel.Enable();

            if (activePanel == panelList[0])
            {
                for (int i = 0; i < 5; i++)
                {
                    activePanel.panelLblList[i + firstElemIndex].setPosition(new Vector2f(activePanel.panelBtnList[i + firstElemIndex].Position.X + 47, activePanel.panelBtnList[i + firstElemIndex].Position.Y + 165));
                }
            }
        }
        private void OnScrollPanelRightMouseReleased(object sender, BtnReleasedEventArgs e)
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
            var panelName = (componentList.Find(x => x.componentID == "panelName")) as OurLabel;
            panelName.Text = activePanel.PanelName;
            panelName.setPosition(new Vector2f(640 - panelName.Size.X / 2, 210));
        }
        private void OnScrollPanelLeftMouseReleased(object sender, BtnReleasedEventArgs e)
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
            if (activePanel == panelList[0])
            {
                EnableScrollBtns();
            }
            else
            {
                DisableScrollBtns();
            }
            ChangePanel(activePanel);
            var panelName = (componentList.Find(x => x.componentID == "panelName")) as OurLabel;
            panelName.Text = activePanel.PanelName;
            panelName.setPosition(new Vector2f(640 - panelName.Size.X / 2, 210));
        }


        //klikniecie escape
        public override void callOnKeyPressed(object sender, KeyEventArgs e, SceneManager sceneManager)
        {
            base.callOnKeyPressed(sender, e, sceneManager);
            if(e.Code == Keyboard.Key.Escape)
            {
                sceneManager.changeScene(EscapeMenu.Instance());
            }
        }

        //wczytanie informacji o przyciskach z panelu workbench
        private string[] readPanelInfo(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);

            XmlNode mainNode = xmldoc.SelectSingleNode("/main");

            int count = mainNode.ChildNodes.Count;
            XmlNode currNode;
            string[] info = new string[count];
            for (int i = 0; i < count; i++)
            {
                currNode = mainNode.SelectSingleNode(string.Format("n{0}", i));
                info[i] = currNode.InnerText;
            }

            return info;
        }

        //spełnienie klasy abstracyjnej
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
                for (int i = 0; i < panelList[0].panelLblList.Count; i++)
                {
                    panelList[0].panelLblList[i].Text = PlayerManager.Instance.upgradeCost((stats)(i - 1)).ToString() + " cr";
                }
            }
            else if (activePanel == panelList[2])
            {
                for (int i = 0; i < panelList[2].panelLblList.Count; i++)
                {
                    panelList[2].panelLblList[i].Text = PlayerManager.Instance.powerupCost((powerups)i) + " cr";
                }
            }
            PlayerHud.Instance().update();
            cursor.update();
        }

        public void cleanUp()
        {
            if (initialized)
            {
                componentList.Clear();
                foreach (var pnl in panelList)
                {
                    pnl.panelBtnList.Clear();
                    pnl.panelInfoLblList.Clear();
                    pnl.panelLblList.Clear();
                }
                panelList.Clear();
                PlayerManager.Instance.restartProgress();
            }
            initialized = false;
        }
        //klasa pomocnicza panel

        class Panel
        {
            public List<OurButton> panelBtnList = new List<OurButton>();
            public List<OurLabel> panelLblList = new List<OurLabel>();
            public List<OurLabel> panelInfoLblList = new List<OurLabel>();
            public string PanelName { get; set; }

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
                foreach (var lbl in panelInfoLblList)
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
                    panelBtnList[i].setPosition(new Vector2f(80 + 230 * i, 647));
                }
                for (int i = 0; i < rangeLbl; i++)
                {
                    panelLblList[i].Active = true;
                    panelLblList[i].Visible = true;
                }
                foreach (var lbl in panelInfoLblList)
                {
                    lbl.Visible = true;
                    lbl.Active = true;
                }
               
            }

        }
    }
}
