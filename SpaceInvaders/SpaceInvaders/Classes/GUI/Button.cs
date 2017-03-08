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
    enum Style
    {
        none = 0,
        save = 1,
        cancel = 2,
        clean = 3
    }
    enum State
    {
        normal = 0,
        hovered = 1,
        clicked = 2
    }
    class Button: Drawable
    {
        #region Properties
        private Color m_bgNormal;
        private Color m_bgHover;
        private Color m_bgClicked;
        private Color m_textNormal;
        private Color m_textHover;
        private Color m_textClicked;
        private Color m_border;

        private Vector2f m_size;
        private Vector2f m_position;

        private State m_btnstate;
        private Style m_style;

        private float m_borderThickness;

        private RectangleShape m_buttonShape;

        private Font m_font;
        private Text m_text;
        private uint m_fontSize;

        public Color BgNormal
        {
            get
            {
                return m_bgNormal;
            }

            set
            {
                m_bgNormal = value;
            }
        }

        public Color BgHover
        {
            get
            {
                return m_bgHover;
            }

            set
            {
                m_bgHover = value;
            }
        }

        public Color BgClicked
        {
            get
            {
                return m_bgClicked;
            }

            set
            {
                m_bgClicked = value;
            }
        }

        public Color TextNormal
        {
            get
            {
                return m_textNormal;
            }

            set
            {
                m_textNormal = value;
            }
        }

        public Color TextHover
        {
            get
            {
                return m_textHover;
            }

            set
            {
                m_textHover = value;
            }
        }

        public Color TextClicked
        {
            get
            {
                return m_textClicked;
            }

            set
            {
                m_textClicked = value;
            }
        }

        public Color Border
        {
            get
            {
                return m_border;
            }

            set
            {
                m_border = value;
            }
        }

        public Vector2f Size
        {
            get
            {
                return m_size;
            }

            set
            {
                m_size = value;
            }
        }

        public Vector2f Position
        {
            get
            {
                return m_position;
            }

            set
            {
                m_position = value;
            }
        }

        public State Btnstate
        {
            get
            {
                return m_btnstate;
            }
        }

        public float BorderThickness
        {
            get
            {
                return m_borderThickness;
            }

            set
            {
                m_borderThickness = value;
            }
        }
        

        public Font Font
        {
            get
            {
                return m_font;
            }

            set
            {
                m_font = value;
            }
        }

        public Text Text
        {
            get
            {
                return m_text;
            }
        }

        public void setText(string s)
        {
            m_text.DisplayedString = s;
        }
        public uint FontSize
        {
            get
            {
                return m_fontSize;
            }

            set
            {
                m_fontSize = value;
            }
        }

        public Vector2f getDimensions()
        {
            return new Vector2f(m_buttonShape.GetGlobalBounds().Width, m_buttonShape.GetGlobalBounds().Height);
        }
        #endregion

        Button(string text, Font font, Vector2f position, Style style)
        {
            m_position = position;
            m_font = font;
            m_style = style;
            m_btnstate = State.normal;

            #region Style
            switch (m_style)
            {
                case Style.none:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(255, 255, 255, 100);
                        m_bgHover = new Color(200, 200, 200, 100);
                        m_bgClicked = new Color(150, 150, 150);
                        m_border = new Color(255, 255, 255, 100);
                    }
                    break;

                case Style.save:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(0, 255, 0, 100);
                        m_bgHover = new Color(0, 200, 0, 100);
                        m_bgClicked = new Color(0, 150, 0);
                        m_border = new Color(0, 0, 0, 100);
                    }
                    break;

                case Style.cancel:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(255, 0, 0, 100);
                        m_bgHover = new Color(200, 0, 0, 100);
                        m_bgClicked = new Color(150, 0, 0);
                        m_border = new Color(255, 255, 255, 100);
                    }
                    break;

                case Style.clean:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(0, 255, 255, 100);
                        m_bgHover = new Color(0, 200, 200, 100);
                        m_bgClicked = new Color(0, 150, 150);
                        m_border = new Color(255, 255, 255, 100);
                    }
                    break;

                default:
                    break;
            }
            #endregion 

            m_text.DisplayedString = text;
            m_text.Font = font;
            m_text.Origin = new Vector2f(m_text.GetGlobalBounds().Width / 2, m_text.GetGlobalBounds().Height / 2);
            m_text.Color = m_bgNormal;

            m_borderThickness = 0;
            m_size = new Vector2f(m_text.GetGlobalBounds().Width*1.5f, m_text.GetGlobalBounds().Height*1.5f);
            m_buttonShape = new RectangleShape(m_size);
            m_buttonShape.Origin = new Vector2f(m_buttonShape.GetGlobalBounds().Width / 2, m_buttonShape.GetGlobalBounds().Height / 2);
            m_buttonShape.Position = m_position;

        }
        public void setSize(uint size)
        {
            m_fontSize = size;
            m_text.CharacterSize = m_fontSize;
            m_text.Origin = new Vector2f(m_text.GetGlobalBounds().Width / 2, m_text.GetGlobalBounds().Height / 2);

            m_size = new Vector2f(m_text.GetGlobalBounds().Width * 1.5f, m_text.GetGlobalBounds().Height * 1.5f);
            m_buttonShape = new RectangleShape(m_size);


        }
        public void setStyle(Style style)
        {
            m_style = style;

            switch (m_style)
            {
                case Style.none:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(255, 255, 255, 100);
                        m_bgHover = new Color(200, 200, 200, 100);
                        m_bgClicked = new Color(150, 150, 150);
                        m_border = new Color(255, 255, 255, 100);
                    }
                    break;

                case Style.save:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(0, 255, 0, 100);
                        m_bgHover = new Color(0, 200, 0, 100);
                        m_bgClicked = new Color(0, 150, 0);
                        m_border = new Color(0, 0, 0, 100);
                    }
                    break;

                case Style.cancel:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(255, 0, 0, 100);
                        m_bgHover = new Color(200, 0, 0, 100);
                        m_bgClicked = new Color(150, 0, 0);
                        m_border = new Color(255, 255, 255, 100);
                    }
                    break;

                case Style.clean:
                    {
                        m_textNormal = new Color(255, 255, 255);
                        m_textHover = new Color(255, 255, 255);
                        m_textClicked = new Color(255, 255, 255);
                        m_bgNormal = new Color(0, 255, 255, 100);
                        m_bgHover = new Color(0, 200, 200, 100);
                        m_bgClicked = new Color(0, 150, 150);
                        m_border = new Color(255, 255, 255, 100);
                    }
                    break;

                default:
                    break;
            }

        }

        public void update(Event e, RenderWindow window)
        {
            Vector2f mousePosition = new Vector2f( Mouse.GetPosition(window).X,Mouse.GetPosition().Y);
            bool mouseInButton = mousePosition.X >= m_buttonShape.Position.X - m_buttonShape.GetGlobalBounds().Width/2
                            && mousePosition.X <= m_buttonShape.Position.X + m_buttonShape.GetGlobalBounds().Width/2
                            && mousePosition.Y >= m_buttonShape.Position.Y - m_buttonShape.GetGlobalBounds().Height / 2
                            && mousePosition.Y <= m_buttonShape.Position.Y + m_buttonShape.GetGlobalBounds().Height / 2;

            if(e.Type == EventType.MouseMoved)
            {
                if(mouseInButton)
                {
                    m_btnstate = State.hovered;
                }
                else
                {
                    m_btnstate = State.normal;
                }
            }
            else if(e.Type == EventType.MouseButtonReleased)
            {
                if(mouseInButton)
                {
                    m_btnstate = State.clicked;
                }
                else
                {
                    m_btnstate = State.normal;
                }
            }

        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }
}
