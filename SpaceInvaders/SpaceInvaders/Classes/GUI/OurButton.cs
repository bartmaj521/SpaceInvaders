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
    enum State
    {
        normal = 0,
        hovered = 1,
        clicked = 2,
    }
    class OurButton : UIComponent, Drawable
    {
        private IntRect collider;
        private Sprite buttonSprite;
        private State currentState;

        public Color textNormal { get; set; }
        public Color textHover { get; set; }
        public Color textClicked { get; set; }

        public Font font { get; set; }

        public Text text;
        public OurButton(Texture _texture, string _text, Vector2i frameSize, Vector2f position)
        {
            currentState = 0;
            textNormal = new Color(91, 209, 255);
            textHover = new Color(210, 245, 255);
            textClicked = new Color(99, 200, 255);
            buttonSprite = new Sprite();
            buttonSprite.Texture = _texture;
            buttonSprite.TextureRect = new IntRect(0, 32, frameSize.X, frameSize.Y);
            buttonSprite.Origin = new Vector2f(frameSize.X / 2, frameSize.Y / 2);
            buttonSprite.Position = position;

            collider = new IntRect((int)position.X - frameSize.X / 2, (int)position.Y - frameSize.Y / 2, frameSize.X, frameSize.Y);
            font = new Font("comic.ttf");
            text = new Text(_text, font);
            text.CharacterSize = 16;
            text.Origin = new Vector2f(text.GetGlobalBounds().Left + text.GetLocalBounds().Width / 2, text.GetGlobalBounds().Top + text.GetLocalBounds().Height / 2);
            text.Position = position;

        }
        public override void update()
        {
            switch (currentState)
            {
                case State.normal:
                    {
                        text.Color = textNormal;
                        buttonSprite.TextureRect = new IntRect(0, 0, buttonSprite.TextureRect.Width, buttonSprite.TextureRect.Height);
                    }
                    break;
                case State.hovered:
                    {
                        text.Color = textHover;
                        buttonSprite.TextureRect = new IntRect(0, 32, buttonSprite.TextureRect.Width, buttonSprite.TextureRect.Height);
                    }
                    break;
                case State.clicked:
                    {
                        text.Color = textClicked;
                        buttonSprite.TextureRect = new IntRect(0, 64, buttonSprite.TextureRect.Width, buttonSprite.TextureRect.Height);
                    }
                    break;
                default:
                    break;
            }
        }
        public void checkHover(Vector2f position)
        {
            if (collider.Contains((int)position.X, (int)position.Y))
                currentState = State.hovered;
            else
                currentState = State.normal;
        }
        public void checkClick(Vector2f position, Mouse.Button button, object sender)
        {
            if (collider.Contains((int)position.X, (int)position.Y) && button == Mouse.Button.Left)
            {
                currentState = State.clicked;
            }
            else if (collider.Contains((int)position.X, (int)position.Y))
                currentState = State.hovered;
            else
                currentState = State.normal;
        }
        public void checkUnclick(Vector2f position, Mouse.Button button, object sender)
        {
            if (collider.Contains((int)position.X, (int)position.Y) && button == Mouse.Button.Left)
            {
                currentState = State.hovered;
                if (this.text.DisplayedString == "Exit")
                    (sender as RenderWindow).Close();
            }

            else
                currentState = State.normal;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(buttonSprite, states);
            target.Draw(text, states);

        }
    }
}
