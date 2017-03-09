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
        private List<UIComponent> componentList = new List<UIComponent>();
        public Menu(string _title):base(_title)
        {

        }
        

        public override void callOnMouseButtonPressed(object sender,MouseButtonEventArgs e)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
            {
                button.checkClick(new Vector2f(e.X, e.Y), e.Button, sender);
            }
        }

        public override void callOnMouseButtonReleased(MouseButtonEventArgs e)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
                button.checkUnclick(new Vector2f(e.X, e.Y), e.Button);
        }
        

        public override void callOnMoved(MouseMoveEventArgs e)
        {
            foreach (OurButton button in componentList.OfType<OurButton>())
                button.checkHover(new Vector2f(e.X, e.Y));
            foreach (Cursor cursor in componentList.OfType<Cursor>())
                cursor.moveCursor(new Vector2f(e.X, e.Y));
        }

        public override void initialize()
        {
            
            componentList.Add(new OurButton(new Texture("buttonSprite.png"), "Play", new Vector2i(100, 32), new Vector2f(200, 100)));
            componentList.Add(new OurButton(new Texture("buttonSprite.png"), "Scores", new Vector2i(100, 32), new Vector2f(200, 140)));
            componentList.Add(new OurButton(new Texture("buttonSprite.png"), "Exit", new Vector2i(100, 32), new Vector2f(200, 180)));
            componentList.Add(new Cursor(new Texture("cursor.png"), new Vector2f(0.1f, 0.1f)));
        }

        public override void drawComponents(RenderWindow window)
        {
            foreach (var component in componentList)
                window.Draw(component);
        }
        public override void updateComponents()
        {
            foreach (var component in componentList)
            {
                component.update();
            }
            
        }
    }
}
