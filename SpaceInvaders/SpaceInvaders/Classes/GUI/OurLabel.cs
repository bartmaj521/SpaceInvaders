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
    class OurLabel : UIComponent
    {
        private string text;
        private Text DisplayedText;
        public Font Font { get; set; }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                DisplayedText.DisplayedString = text;
                DisplayedText.Position = new Vector2f(Position.X + Size.X / 2 - DisplayedText.GetLocalBounds().Width / 2 - DisplayedText.GetLocalBounds().Left, Position.Y + Size.Y / 2 - DisplayedText.GetLocalBounds().Height / 2 - DisplayedText.GetLocalBounds().Top);

            }
        }

        //przyjmuje ksztalt podany w _size
        public OurLabel(Texture _texture,string text, uint _fontSize, Vector2i _size) : base(_texture)
        {
            Font = new Font("font.ttf");
            DisplayedText = new Text(text, Font);
            DisplayedText.CharacterSize = _fontSize;
            Text = text;

            componentSprite.Scale = new Vector2f((float)_size.X / (_texture.Size.X), (float)_size.Y / (_texture.Size.Y));

            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);
            Position = componentSprite.Position;
        }

        //przyjmuje ksztalt textury
        public OurLabel(Texture _texture, string text, uint _fontSize) : base(_texture)
        {
            Font = new Font("font.ttf");
            DisplayedText = new Text(text, Font);
            DisplayedText.CharacterSize = _fontSize;

            componentSprite.Scale = new Vector2f(DisplayedText.GetLocalBounds().Width / componentSprite.Texture.Size.X + 0.3f, DisplayedText.GetLocalBounds().Height / componentSprite.Texture.Size.Y + 0.3f);
            
            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);
            Position = componentSprite.Position;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Visible)
            {
                target.Draw(componentSprite, states);
                if (DisplayedText != null)
                    target.Draw(DisplayedText, states);
            }
        }

        public override void update()
        {
        }


        public override void setPosition(Vector2f _position)
        {
            if (DisplayedText != null)
            {
                componentSprite.Position = _position;
                Position = _position;
                DisplayedText.Position = new Vector2f(Position.X + Size.X / 2 - DisplayedText.GetLocalBounds().Width / 2 - DisplayedText.GetLocalBounds().Left, Position.Y + Size.Y / 2 - DisplayedText.GetLocalBounds().Height / 2 - DisplayedText.GetLocalBounds().Top);
            }
        }
    }
}
