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
    class OurTextbox : UIComponent, Drawable
    {
        private Text displayedText;
        private Sprite txbSprite;
        private Font font;

        public string text { get; set; }
        private bool isEmpty;
        public bool isSelected;
        public bool isVisible;

        public OurTextbox(Texture _texture, Vector2i _texturesize,Vector2f _position, uint _fontSize)
        {
            isEmpty = true;
            isSelected = false;
            isVisible = false;
            text = "";
            displayedText = new Text(text, new Font("font.ttf"));
            txbSprite = new Sprite(_texture);
            txbSprite.TextureRect = new IntRect(0, 0, _texturesize.X, _texturesize.Y);
            txbSprite.Origin = new Vector2f(_texturesize.X / 2, _texturesize.Y / 2);
            txbSprite.Position = _position;

            font = new Font("font.ttf");
            displayedText = new Text(text, font);
            displayedText.CharacterSize = _fontSize;
            displayedText.Origin = new Vector2f(displayedText.GetGlobalBounds().Left + displayedText.GetLocalBounds().Width / 2, displayedText.GetGlobalBounds().Top + displayedText.GetLocalBounds().Height / 2);
            displayedText.Position = new Vector2f(_position.X - displayedText.GetGlobalBounds().Width, _position.Y-displayedText.GetGlobalBounds().Height);


        }


        public void checkKey(KeyEventArgs e)
        {
            if(isSelected)
            {
                if(e.Code.ToString().Length==1)
                {
                    if (text.Length < 15) 
                    text += e.Code.ToString().ToLower();
                }
                else if(e.Code == Keyboard.Key.Space)
                {
                    text += " ";
                }
                else if(e.Code == Keyboard.Key.BackSpace)
                {
                    if (!isEmpty)
                    {
                        text = text.Remove(text.Length - 1, 1);
                    }
                }
            }
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (isVisible)
            {
                target.Draw(txbSprite);
                target.Draw(displayedText); 
            }
        }

        public override void update()
        {
            displayedText.Position = new Vector2f(txbSprite.Position.X - displayedText.GetGlobalBounds().Width/2, txbSprite.Position.Y - displayedText.GetGlobalBounds().Height);
            isEmpty = text.Length == 0;

            displayedText.DisplayedString = text;
        }
    }
}
