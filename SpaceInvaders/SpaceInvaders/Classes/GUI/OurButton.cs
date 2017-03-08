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
    enum State
    {
        normal = 0,
        hovered = 1,
        clicked = 2
    }
    class OurButton: Drawable
    {

        public Vector2f size { get; set; }
        public Vector2f position { get; set; }
        public Text text { get; set; }

        private ButtonStyle buttonStyle;
        private Shape buttonShape;
        private State btnstate;


        public OurButton(string _text, Vector2f _position, btnStyle style, RenderWindow window)
        {
            position = _position;
            btnstate = State.normal;
            buttonStyle = new ButtonStyle(style);       
            

             
            text = new Text();
            text.DisplayedString = _text;
            text.Font = buttonStyle.font;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
            text.Color = buttonStyle.textNormal;
            text.Position = position;


            size = new Vector2f(text.GetGlobalBounds().Width*1.5f, text.GetGlobalBounds().Height*1.5f);
            buttonShape = new RectangleShape(size);
            buttonShape.Origin = new Vector2f(buttonShape.GetGlobalBounds().Width / 2, buttonShape.GetGlobalBounds().Height / 2);
            buttonShape.Position = position;

            window.MouseMoved += onMouseMoved;
            window.MouseButtonPressed += onMousePressed;
            window.MouseButtonReleased += onMouseReleased;
        }

        private void onMouseReleased(object sender, MouseButtonEventArgs e)
        {
            Vector2f mousePosition = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition().Y);
            bool mouseInButton = mousePosition.X >= buttonShape.Position.X - buttonShape.GetGlobalBounds().Width / 2
                            && mousePosition.X <= buttonShape.Position.X + buttonShape.GetGlobalBounds().Width / 2
                            && mousePosition.Y >= buttonShape.Position.Y - buttonShape.GetGlobalBounds().Height / 2
                            && mousePosition.Y <= buttonShape.Position.Y + buttonShape.GetGlobalBounds().Height / 2;

            if (e.Type == EventType.MouseMoved)
            {
                if (mouseInButton)
                {
                    btnstate = State.hovered;
                }
                else
                {
                    btnstate = State.normal;
                }
            }
            else if (e.Type == EventType.MouseButtonPressed)
            {
                if (mouseInButton)
                {
                    btnstate = State.clicked;
                }
                else
                {
                    btnstate = State.normal;
                }
            }
        }

        private void onMousePressed(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void setSize(uint _size)
        {
            buttonStyle.fontSize = _size;
            text.CharacterSize = buttonStyle.fontSize;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);

            size = new Vector2f(text.GetGlobalBounds().Width * 1.5f, text.GetGlobalBounds().Height * 1.5f);
            buttonShape = new RectangleShape(size);


        }

        public void update(Event e, RenderWindow window)
        {
            switch (btnstate)
            {
                case State.normal:
                    {
                        text.Color = buttonStyle.textNormal;
                        buttonShape.FillColor = buttonStyle.bgNormal;
                    }
                    break;
                case State.hovered:
                    {
                        text.Color = buttonStyle.textHover;
                        buttonShape.FillColor = buttonStyle.bgHover;
                    }
                    break;
                case State.clicked:
                    {
                        text.Color = buttonStyle.textClicked;
                        buttonShape.FillColor = buttonStyle.bgClicked;
                    }
                    break;
                default:
                    break;
            }
            

        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(buttonShape, states);
            target.Draw(text, states);
        }
    }
}
