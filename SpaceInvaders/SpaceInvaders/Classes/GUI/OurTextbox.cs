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
    public class TextboxEventArgs : EventArgs
    {
        public string Text { get; set; }
    }
    class OurTextbox : UIComponent, Drawable
    {
        private Text DisplayedText;
        private Font Font;

        public string Text { get; set; }
        private bool isEmpty;
        private int maxTextLength;
        public delegate void TextconfirmedEventHandler(object sender, TextboxEventArgs e);
        public event TextconfirmedEventHandler TextConfirmed;

        protected virtual void onTextureconfirmed()
        {
            if (TextConfirmed != null)
            {
                TextConfirmed(this, new TextboxEventArgs() { Text = Text });
            }
        }

        public OurTextbox(Texture _texture, Vector2f _boxsize, uint _fontSize) : base(_texture)
        {
            isEmpty = true;
            maxTextLength = (int)(_boxsize.X / _fontSize) + 1;
            Text = "";
            if (_boxsize.X == 0 && _boxsize.Y == 0)
            {
                componentSprite.Scale = new Vector2f(1, 1);
            }
            else if (_boxsize.X != 0 && _boxsize.Y == 0)
            {
                componentSprite.Scale = new Vector2f(_boxsize.X / _texture.Size.X, 1);
            }
            else if (_boxsize.X == 0 && _boxsize.Y != 0)
            {
                componentSprite.Scale = new Vector2f(1, _boxsize.Y / _texture.Size.Y);
            }
            else
                componentSprite.Scale = new Vector2f(_boxsize.X / _texture.Size.X, _boxsize.Y / _texture.Size.Y);


            DisplayedText = new Text(Text, new Font("font.ttf"));
            DisplayedText.CharacterSize = _fontSize;
            Position = componentSprite.Position;
            Size = new Vector2f(componentSprite.GetGlobalBounds().Width, componentSprite.GetGlobalBounds().Height);

        }


        public void checkKey(KeyEventArgs e)
        {
            if (Selected)
            {
                if (e.Code.ToString().Length == 1)
                {
                    if (Text.Length < maxTextLength)
                        Text += e.Code.ToString().ToLower();
                }
                else if (e.Code == Keyboard.Key.Space)
                {
                    Text += " ";
                }
                else if (e.Code == Keyboard.Key.BackSpace)
                {
                    if (!isEmpty)
                    {
                        Text = Text.Remove(Text.Length - 1, 1);
                    }
                }
                else if (e.Code == Keyboard.Key.Return)
                {
                    onTextureconfirmed();
                }
            }
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Visible)
            {
                target.Draw(componentSprite);
                target.Draw(DisplayedText);
            }
        }

        public override void update()
        {
            DisplayedText.Position = new Vector2f(Position.X + Size.X / 2 - DisplayedText.GetLocalBounds().Width / 2 - DisplayedText.GetLocalBounds().Left, Position.Y + Size.Y / 2 - DisplayedText.GetLocalBounds().Height / 2 - DisplayedText.GetLocalBounds().Top);
            isEmpty = Text.Length == 0;

            DisplayedText.DisplayedString = Text;
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
