using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes.GUI
{
    public enum align
    {
        centered = 0,
        left = 1
    }
    public class OurLabel : UIComponent, Drawable
    {
        private string text;
        private Color textColor;
        private Text displayedText;

        public align TextAlign { get; set; }
        public Font Font { get; set; }
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                displayedText.Color = textColor;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                displayedText.DisplayedString = text;
                if (TextAlign == align.centered)
                {
                    displayedText.Position = new Vector2f(Position.X + Size.X / 2 - displayedText.GetLocalBounds().Width / 2 - displayedText.GetLocalBounds().Left, Position.Y + Size.Y / 2 - displayedText.GetLocalBounds().Height / 2 - displayedText.GetLocalBounds().Top);
                }
                else
                {
                    displayedText.Position = Position;
                }
                
            }
        }


        //przyjmuje ksztalt podany w _size
        public OurLabel(Texture _texture,string text, uint _fontSize, Vector2i _size) : base(_texture)
        {
            TextAlign = align.centered;
            Font = new Font("font.ttf");
            displayedText = new Text(text, Font);
            displayedText.CharacterSize = _fontSize;
            Text = text;

            componentSprite.Scale = new Vector2f((float)_size.X / (_texture.Size.X), (float)_size.Y / (_texture.Size.Y));

            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);
            Position = componentSprite.Position;
        }
        //przyjmuje ksztalt textury
        public OurLabel(Texture _texture, string text, uint _fontSize) : base(_texture)
        {
            Font = new Font("font.ttf");
            displayedText = new Text(text, Font);
            displayedText.CharacterSize = _fontSize;

            componentSprite.Scale = new Vector2f(displayedText.GetLocalBounds().Width / componentSprite.Texture.Size.X + 0.3f, displayedText.GetLocalBounds().Height / componentSprite.Texture.Size.Y + 0.3f);
            
            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);
            Position = componentSprite.Position;
        }


        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Visible)
            {
                target.Draw(componentSprite, states);
                if (displayedText != null)
                    target.Draw(displayedText, states);
            }
        }
        public override void update() { }
        public override void setPosition(Vector2f _position)
        {
            if (displayedText != null)
            {
                componentSprite.Position = _position;
                Position = _position;
                if (TextAlign == align.centered)
                {
                    displayedText.Position = new Vector2f(Position.X + Size.X / 2 - displayedText.GetLocalBounds().Width / 2 - displayedText.GetLocalBounds().Left, Position.Y + Size.Y / 2 - displayedText.GetLocalBounds().Height / 2 - displayedText.GetLocalBounds().Top);
                }
                else
                {
                    displayedText.Position = _position;
                }
            }
        }
    }
}