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
            if(instance ==null)
            {
                instance = new MainMenu();
            }
            return instance;
        }
        private MainMenu() : base() { }
        #endregion

        public override void pause()
        {
            throw new NotImplementedException();
        }

        public override void reasume()
        {
            throw new NotImplementedException();
        }

        public override void initialize()
        {
            componentList.Add(new OurButton(new Texture("buttonSprite.png"), "Play", new Vector2i(100, 32), new Vector2f(200, 100)));
            componentList.Add(Cursor.Instance(new Texture("cursor.png"), new Vector2f(1f, 1f)));
        }
    }
}
