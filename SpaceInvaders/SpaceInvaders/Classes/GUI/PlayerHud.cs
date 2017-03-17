using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders.Classes.GUI
{
    enum stats
    {
        shipSpeed,
        armor,
        accuracy,
        fireSpeed,
        weaponDmg,
    }
    class PlayerHud : Drawable
    {
        private int shipSpeed = 0;
        private int maxShipSpeed = 0;
        private int armor = 0;
        private int maxArmor = 0;
        private int accuracy = 0;
        private int maxAccuracy = 0;
        private int fireSpeed = 0;
        private int maxFireSpeed = 0;
        private int weaponDmg = 0;
        private int maxWeaponDmg = 0;

        private Sprite HudSprite;
        private Sprite eq;
        private Sprite neq;
        public Sprite ShipImg { get; set; }
        private OurLabel ShipNameLabel;
        private OurLabel PlayerMoneyLabel;

        private string shipName;
        private int playerMoney;
        private int[] shipValues;
        private int[] maxShipValues;


        public int ShipSpeed
        {
            get
            {
                return shipSpeed;
            }

            set
            {
                if (value < 0)
                    shipSpeed = 0;
                if (value > MaxShipSpeed)
                    shipSpeed = MaxShipSpeed;
                shipSpeed = value;
            }
        }

        public int MaxShipSpeed
        {
            get
            {
                return maxShipSpeed;
            }

            set
            {
                if (value > 5)
                    maxShipSpeed = 5;
                maxShipSpeed = value;
            }
        }

        public int Armor
        {
            get
            {
                return armor;
            }

            set
            {
                if (value < 0)
                    armor = 0;
                if (value > MaxArmor)
                    armor = MaxArmor;
                armor = value;
            }
        }

        public int MaxArmor
        {
            get
            {
                return maxArmor;
            }

            set
            {
                if (value > 5)
                    maxArmor = 5;
                maxArmor = value;
            }
        }

        public int Accuracy
        {
            get
            {
                return accuracy;
            }

            set
            {
                if (value < 0)
                    accuracy = 0;
                if (value > maxAccuracy)
                    accuracy = maxAccuracy;
                accuracy = value;
            }
        }

        public int MaxAccuracy
        {
            get
            {
                return maxAccuracy;
            }

            set
            {
                if (value > 5)
                    maxAccuracy = 5;
                maxAccuracy = value;
            }
        }

        public int FireSpeed
        {
            get
            {
                return fireSpeed;
            }

            set
            {
                if (value < 0)
                    fireSpeed = 0;
                if (value > MaxFireSpeed)
                    fireSpeed = MaxFireSpeed;
                fireSpeed = value;
            }
        }

        public int MaxFireSpeed
        {
            get
            {
                return maxFireSpeed;
            }

            set
            {
                if (value > 5)
                    maxFireSpeed = 5;
                maxFireSpeed = value;
            }
        }

        public int WeaponDmg
        {
            get
            {
                return weaponDmg;
            }

            set
            {
                if (value < 0)
                    weaponDmg = 0;
                if (value > maxWeaponDmg)
                    weaponDmg = maxWeaponDmg;
                weaponDmg = value;
            }
        }

        public int MaxWeaponDmg
        {
            get
            {
                return maxWeaponDmg;
            }

            set
            {
                if (value > 5)
                    maxWeaponDmg = 5;
                maxWeaponDmg = value;
            }
        }

        public string ShipName
        {
            get
            {
                return shipName;
            }

            set
            {
                shipName = value;
                ShipNameLabel.Text = shipName;
            }
        }

        public int ShipValue
        {
            get
            {
                return playerMoney;
            }

            set
            {
                playerMoney = value;
                PlayerMoneyLabel.Text = playerMoney + "$";
            }
        }

        //do zrobienia
        //public OurProgressbar ShipHealth { get; set; }

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
            HudSprite = new Sprite(new Texture("playerHud.png"));

            eq = new Sprite(new Texture("equipped.png"));
            neq = new Sprite(new Texture("notEquipped.png"));
            HudSprite.Position = new Vector2f(1279, 172);
            ShipNameLabel = new OurLabel(new Texture("blank.png"), ShipName, 30, new Vector2i(267, 27));
            ShipNameLabel.setPosition(new Vector2f(HudSprite.Position.X + 16, HudSprite.Position.Y + 9));
            PlayerMoneyLabel = new OurLabel(new Texture("blank.png"), ShipValue.ToString() + " $", 30, new Vector2i(267, 38));
            PlayerMoneyLabel.setPosition(new Vector2f(HudSprite.Position.X + 16, HudSprite.Position.Y + 224));
        }
        public void update()
        {
            shipValues = new int[5] { shipSpeed, armor, accuracy, fireSpeed, weaponDmg };
            maxShipValues = new int[5] { maxShipSpeed, maxArmor, maxAccuracy, maxFireSpeed, maxWeaponDmg };

        }
        public void add(stats stat)
        {
            switch (stat)
            {
                case stats.shipSpeed:
                    {
                        if(shipSpeed<MaxShipSpeed)
                        {
                            shipSpeed++;
                        }
                    }
                    break;
                case stats.armor:
                    {
                        if (armor < MaxArmor)
                        {
                            armor++;
                        }
                    }
                    break;
                case stats.accuracy:
                    {
                        if (accuracy < maxAccuracy)
                        {
                            accuracy++;
                        }
                    }
                    break;
                case stats.fireSpeed:
                    {
                        if (fireSpeed < maxFireSpeed)
                        {
                            fireSpeed++;
                        }
                    }
                    break;
                case stats.weaponDmg:
                    {
                        if (weaponDmg < maxWeaponDmg)
                        {
                            weaponDmg++;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(HudSprite, states);
            target.Draw(ShipNameLabel, states);
            target.Draw(PlayerMoneyLabel, states);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < maxShipValues[i]; j++)
                {
                    neq.Position = new Vector2f(HudSprite.Position.X + 21 + 52 * j, HudSprite.Position.Y + 314 + 84 * i);
                    target.Draw(neq);
                }
                for (int j = 0; j < shipValues[i]; j++)
                {
                    eq.Position = new Vector2f(HudSprite.Position.X + 21 + 52 * j, HudSprite.Position.Y + 314 + 84 * i);
                    target.Draw(eq);
                }
            }


        }


    }
}
