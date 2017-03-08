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
    enum btnStyle
    {
        blueish = 0,
    }
    class ButtonStyle
    {
        #region Props
        public Color bgNormal { get; set; }
        public Color bgHover { get; set; }
        public Color bgClicked { get; set; }
        public Color textNormal { get; set; }
        public Color textHover { get; set; }
        public Color textClicked { get; set; }
        public Color border { get; set; }

        public float borderThickness { get; set; }

        public Shape buttonShape { get; set; }

        public Font font { get; set; }
        public uint fontSize { get; set; }
        #endregion

        public ButtonStyle() {}
        public ButtonStyle(btnStyle style)
        {
            switch (style)
            {
                case btnStyle.blueish:
                    {
                        textNormal = new Color(224, 224, 224);
                        textHover = new Color(117, 165, 255);
                        textClicked = new Color(255, 220, 117);
                        bgNormal = new Color(46, 6, 122);
                        bgHover = new Color(46, 6, 122);
                        bgClicked = new Color(46, 6, 122);
                        borderThickness = 0;
                        font = new Font("comic.ttf");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
