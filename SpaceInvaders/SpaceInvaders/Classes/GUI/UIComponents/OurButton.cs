using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceInvaders.Classes.GUI
{
    public enum State
    {
        normal = 0,
        hovered = 1,
        clicked = 2,
    }
    public class BtnReleasedEventArgs: EventArgs
    {
        public int arg;
    }
    public class OurButton : UIComponent, Drawable
    {
        protected IntRect collider;
        protected State currentState;

        public Color TextNormal { get; set; }
        public Color TextHover { get; set; }
        public Color TextClicked { get; set; }
        public Font Font { get; set; }
        public Text Text { get; set; }
        public int ArgToPass;


        public delegate void MouseHoveredEventHandler(object sender, BtnReleasedEventArgs e);
        public event MouseHoveredEventHandler MouseHovered;
        public event EventHandler MousePressed;
        public delegate void MouseReleasedEventHandler(object sender, BtnReleasedEventArgs e);
        public event MouseReleasedEventHandler MouseReleased;

        protected virtual void OnMouseHovered()
        {
            if(Active)
            {
                if(MouseHovered != null)
                {
                    MouseHovered(this, new BtnReleasedEventArgs() { arg = ArgToPass });
                }
            }
        }
        protected virtual void OnMousePressed()
        {
            if (Active)
            {
                if (MousePressed != null)
                {
                    MousePressed(this, EventArgs.Empty);
                } 
            }
        }
        protected virtual void OnMouseReleased()
        {
            if (Active)
            {
                if (MouseReleased != null)
                {
                    MouseReleased(this, new BtnReleasedEventArgs() { arg = ArgToPass });
                } 
            }
        }

        public OurButton(Texture _texture, Vector2i frameSize,string _text,uint fontSize):base(_texture)
        {
            currentState = 0;
            TextNormal = new Color(91, 209, 255);
            TextHover = new Color(210, 245, 255);
            TextClicked = new Color(99, 200, 255);
            componentSprite.TextureRect = new IntRect(0,0, frameSize.X, frameSize.Y);
            Position = new Vector2f(0, 0);
            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);

            collider = new IntRect(0,0, frameSize.X, frameSize.Y);
            Font = new Font("font.ttf");
            Text = new Text(_text, Font);
            Text.CharacterSize = fontSize;
            Text.Position = new Vector2f(Position.X+Size.X/2-Text.GetGlobalBounds().Width/2-Text.GetGlobalBounds().Left,Position.Y+Size.Y/2-Text.GetGlobalBounds().Height/2-Text.GetGlobalBounds().Top);

        }
        

        public void checkHover(Vector2f position)
        {
            if (collider.Contains((int)position.X, (int)position.Y))
            {
                currentState = State.hovered;
                OnMouseHovered();
            }
            else
                currentState = State.normal;
        }
        public void checkPress(Vector2f position, Mouse.Button button, object sender)
        {
            if (collider.Contains((int)position.X, (int)position.Y) && button == Mouse.Button.Left)
            {
                currentState = State.clicked;

                OnMousePressed();
            }
            else if (collider.Contains((int)position.X, (int)position.Y))
                currentState = State.hovered;
            else
                currentState = State.normal;
        }
        public void checkRelease(Vector2f position, Mouse.Button button, object sender)
        {
            if (collider.Contains((int)position.X, (int)position.Y) && button == Mouse.Button.Left)
            {
                currentState = State.hovered;
                OnMouseReleased();
            }

            else
                currentState = State.normal;
        }


        public override void update()
        {

            switch (currentState)
            {
                case State.normal:
                    {
                        Text.Color = TextNormal;
                        componentSprite.TextureRect = new IntRect(0, 0 * componentSprite.TextureRect.Height, componentSprite.TextureRect.Width, componentSprite.TextureRect.Height);
                    }
                    break;
                case State.hovered:
                    {
                        Text.Color = TextHover;
                        componentSprite.TextureRect = new IntRect(0, 1 * componentSprite.TextureRect.Height + 1, componentSprite.TextureRect.Width, componentSprite.TextureRect.Height);
                    }
                    break;
                case State.clicked:
                    {
                        Text.Color = TextClicked;
                        componentSprite.TextureRect = new IntRect(0, 2 * componentSprite.TextureRect.Height + 2, componentSprite.TextureRect.Width, componentSprite.TextureRect.Height);
                    }
                    break;
                default:
                    break;
            }
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Visible)
            {
                target.Draw(componentSprite, states);
                target.Draw(Text, states); 
            }

        }
        public override void setPosition(Vector2f _position)
        {
            if (Text != null)
            {
                componentSprite.Position = _position;
                Position = _position;
                collider = new IntRect((int)Position.X, (int)Position.Y, componentSprite.TextureRect.Width, componentSprite.TextureRect.Height);
                componentSprite.TextureRect = new IntRect((int)Position.X, (int)Position.Y, componentSprite.TextureRect.Width,componentSprite.TextureRect.Height);
                Text.Position = new Vector2f(Position.X + Size.X / 2 - Text.GetLocalBounds().Width / 2 - Text.GetLocalBounds().Left, Position.Y + Size.Y / 2 - Text.GetLocalBounds().Height / 2 - Text.GetLocalBounds().Top);
            }
        }
    }
}
