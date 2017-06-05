using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.IO;


namespace SpaceInvaders.Classes.GUI
{
    
    public class PlayerHud : Drawable
    {
        private Sprite HudSprite;
        private Sprite eq;
        private Sprite neq;
        public Sprite ShipImg { get; set; }
        private OurLabel PlayerNameLabel;
        private OurLabel PlayerMoneyLabel;
        private OurLabel HealthLabel;
        private OurProgressbar DmgProgressbar;

        public PlayerManager PlayerInfo { get; set; }

        private static PlayerHud instance;
        public static PlayerHud Instance()
        {
            if (instance == null)
            {
                instance = new PlayerHud();
            }
            return instance;
        }

        private PlayerHud()
        {
            HudSprite = new Sprite(new Texture(ResourcesManager.resourcesPath + "playerHud.png"));
            eq = new Sprite(new Texture(ResourcesManager.resourcesPath + "equipped.png"));
            neq = new Sprite(new Texture(ResourcesManager.resourcesPath + "notEquipped.png"));
            HudSprite.Position = new Vector2f(1279, 72);


            ShipImg = new Sprite();
            PlayerNameLabel = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), "", 30, new Vector2i(267, 27));
            PlayerNameLabel.setPosition(new Vector2f(HudSprite.Position.X + 16, HudSprite.Position.Y + 9));
            PlayerMoneyLabel = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"),"0 cr", 30, new Vector2i(267, 38));
            PlayerMoneyLabel.setPosition(new Vector2f(HudSprite.Position.X + 16, HudSprite.Position.Y + 224));
            HealthLabel = new OurLabel(new Texture(ResourcesManager.resourcesPath + "blank.png"), (0 * 100).ToString() + "%", 24, new Vector2i(64,25));
            HealthLabel.setPosition(new Vector2f(HudSprite.Position.X + 217, HudSprite.Position.Y + 732));
            DmgProgressbar = new OurProgressbar(new Texture(ResourcesManager.resourcesPath + "DamageProgressBarSprite.png"), new Vector2f(193, 23));
            DmgProgressbar.setPosition(new Vector2f(HudSprite.Position.X + 21, HudSprite.Position.Y + 733));


        }
        public void update()
        {
            DmgProgressbar.Progress = PlayerInfo.ShipInfo.ShipHealth;
            DmgProgressbar.update();
            PlayerNameLabel.Text = PlayerInfo.PlayerName;
            PlayerMoneyLabel.Text = PlayerInfo.PlayerMoney.ToString() + " cr";
            HealthLabel.Text = ((PlayerInfo.ShipInfo.ShipHealth * 100)).ToString() + "%";
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(HudSprite, states);
            target.Draw(ShipImg, states);
            target.Draw(PlayerNameLabel, states);
            target.Draw(PlayerMoneyLabel, states);
            target.Draw(DmgProgressbar, states);
            target.Draw(HealthLabel, states);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < PlayerInfo.ShipInfo.maxUpgrades[i]; j++)
                {
                    neq.Position = new Vector2f(HudSprite.Position.X + 21 + 52 * j, HudSprite.Position.Y + 314 + 84 * i);
                    target.Draw(neq);
                }
                for (int j = 0; j < PlayerInfo.ShipInfo.upgrades[i]; j++)
                {
                    eq.Position = new Vector2f(HudSprite.Position.X + 21 + 52 * j, HudSprite.Position.Y + 314 + 84 * i);
                    target.Draw(eq);
                }
            }


        }


    }
}
