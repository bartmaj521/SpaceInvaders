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
    class EndingMenu : Menu
    {
        public bool hasWon;

        #region Singleton 
        private static EndingMenu instance;
        public static EndingMenu Instance()
        {
            if (instance == null)
            {
                instance = new EndingMenu();
            }
            return instance;
        }
        private EndingMenu() : base()
        {
            background = new Sprite(new Texture(ResourcesManager.resourcesPath + "bgBlank.png"));
        }
        #endregion

        public override void cleanup()
        {
            //throw new NotImplementedException();
        }

        public override void initialize(RenderWindow window)
        {
            string won = hasWon ? "Zwyciestwo": "Porazka"; 
            OurLabel score = new GUI.OurLabel(new Texture(ResourcesManager.resourcesPath+"blank.png"),won, 40);
            score.setPosition(new SFML.System.Vector2f(window.Size.X / 2 - score.Size.X / 2, window.Size.Y / 2 - score.Size.Y / 2));
            componentList.Add(score);

            score = new GUI.OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "Nacisnij enter...", 40);
            score.setPosition(new SFML.System.Vector2f(window.Size.X / 2 - score.Size.X / 2, window.Size.Y / 2 - score.Size.Y / 2 + 50));
            componentList.Add(score);
        }

        public override void updateComponents(SceneManager sceneManager)
        {
            base.updateComponents(sceneManager);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                SceneManager.Instance().changeScene(PlayerMenu.Instance());
        }

        public override void drawComponents(SceneManager sceneManager)
        {
            GameScene.Instance().drawComponents(sceneManager);
            base.drawComponents(sceneManager);
        }

        public override void pause()
        {
            //throw new NotImplementedException();
        }

        public override void reasume()
        {
            //throw new NotImplementedException();
        }
    }
}
