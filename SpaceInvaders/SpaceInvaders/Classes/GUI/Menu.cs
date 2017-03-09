using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace SpaceInvaders.Classes.GUI
{
    class Menu: Scene
    {
        private List<OurButton> buttonList = new List<OurButton>();
        public Menu(string _title):base(_title)
        {

        }
        

        public override void callOnMouseButtonPressed(object sender,MouseButtonEventArgs e)
        {
            foreach (var button in buttonList)
                button.checkClick(new Vector2f(e.X,e.Y),e.Button,sender);
        }

        public override void callOnMouseButtonReleased(MouseButtonEventArgs e)
        {
            foreach (var button in buttonList)
                button.checkUnclick(new Vector2f(e.X, e.Y), e.Button);
        }
        

        public override void callOnMoved(MouseMoveEventArgs e)
        {
            foreach (var button in buttonList)
                button.checkHover(new Vector2f(e.X, e.Y));
        }

        public override void initialize()
        {
            buttonList.Add(new OurButton(new Texture("buttonSprite.png"), "Play", new Vector2i(100, 32), new Vector2f(200, 100)));
            buttonList.Add(new OurButton(new Texture("buttonSprite.png"), "Scores", new Vector2i(100, 32), new Vector2f(200, 140)));
            buttonList.Add(new OurButton(new Texture("buttonSprite.png"), "Exit", new Vector2i(100, 32), new Vector2f(200, 180)));
        }

        public override void drawComponents(RenderWindow window)
        {
            foreach (var button in buttonList)
                window.Draw(button);
        }
        public override void updateComponents()
        {
            foreach (var button in buttonList)
            {
                button.update();
            }
        }
    }
}
