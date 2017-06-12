using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpaceInvaders.Classes.GUI
{
    public class EscapeMenu : Menu
    {
        #region Singleton 
        private static EscapeMenu instance;
        public static EscapeMenu Instance()
        {
            if(instance == null)
            {
                instance = new EscapeMenu();
            }
            return instance;
        }
        private EscapeMenu() : base()
        {
            background = new Sprite(new Texture(ResourcesManager.resourcesPath + "bgBlank.png"));
        }
        #endregion

        public override void cleanup()
        {
           
        }

        public override void initialize(RenderWindow window)
        {
            if (!initialized)
            {
                Vector2i buttonSize = new Vector2i(300, 99);
                uint fontSize = 40;

                OurButton btnResume = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"), buttonSize, "powrot", fontSize);
                btnResume.setPosition(new Vector2f(633, 150));
                btnResume.MouseReleased += OnBtnResumeMouseReleased;
                componentList.Add(btnResume);

                OurButton btnSave = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"), buttonSize, "zapisz gre", fontSize);
                btnSave.setPosition(new Vector2f(633, 270));
                btnSave.MouseReleased += OnBtnSaveMouseReleased;
                componentList.Add(btnSave);

                OurButton btnLoad = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"), buttonSize, "wczytaj gre", 34);
                btnLoad.setPosition(new Vector2f(633, 390));
                btnLoad.MouseReleased += OnBtnLoadMouseReleased;
                componentList.Add(btnLoad);

                OurButton btnMainMenu = new OurButton(new Texture(ResourcesManager.resourcesPath + "buttonSprite.png"), buttonSize, "wyjdz", fontSize);
                btnMainMenu.setPosition(new Vector2f(633, 520));
                btnMainMenu.MouseReleased += OnBtnMainMenuMouseReleased;
                componentList.Add(btnMainMenu);

                cursor = Cursor.Instance(new Texture(ResourcesManager.resourcesPath + "cursor.png"), new Vector2f(1f, 1f));

                initialized = true;
            }
        }

        private void OnBtnLoadMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            LoadDataFromFile();
            sceneManager.changeScene(PlayerMenu.Instance());
        }

        private void OnBtnMainMenuMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            MainMenu.Instance().cleanup();
            sceneManager.changeScene(MainMenu.Instance());
        }

        private void OnBtnSaveMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            SaveDataToFile();
            sceneManager.changeScene(PlayerMenu.Instance());
        }

        private void OnBtnResumeMouseReleased(object sender, BtnReleasedEventArgs e)
        {
            sceneManager.changeScene(PlayerMenu.Instance());
        }

        public void LoadDataFromFile()
        {
            using (Stream stream = File.Open("ship.txt", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                PlayerManager.Instance = (PlayerManager)bf.Deserialize(stream);
                PlayerHud.Instance().PlayerInfo = PlayerManager.Instance;
            }
            
        }
        private void SaveDataToFile()
        {
            using (Stream stream = File.Open("ship.txt", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, PlayerManager.Instance);
            }
        }
        //wczytanie informacji o przyciskach z panelu workbench





        public override void drawComponents(SceneManager sceneManager)
        {
            PlayerMenu.Instance().drawComponents(sceneManager);
            base.drawComponents(sceneManager);
        }

        public override void callOnKeyPressed(object sender, KeyEventArgs e, SceneManager sceneManager)
        {
            base.callOnKeyPressed(sender, e, sceneManager);
            if (e.Code == Keyboard.Key.Escape)
            {
                sceneManager.changeScene(PlayerMenu.Instance());
            }
        }

        public override void pause()
        {
            
        }

        public override void reasume()
        {

        }
    }
}
