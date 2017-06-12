using System;
using System.Linq;
using SFML.Graphics;
using System.Runtime.Serialization;

namespace SpaceInvaders.Classes.GUI
{
    public class UpgradedEventArgs : EventArgs
    {
        public stats Stat { get; set; }
        public int Cost { get; set; }
    }

    [Serializable()]
    public class Ship:ISerializable
    {
        //pola
        private float shipHealth;

        //właściwości
        public Texture ShipTexture { get; set; }
        public float ShipHealth
        {
            get
            {
                return shipHealth;
            }

            set
            {
                shipHealth = value;
                if (shipHealth <= 0)
                    shipHealth = 0;
                if (shipHealth >= 1)
                    shipHealth = 1;

                shipHealth = (float)Math.Round(shipHealth, 2);
            }
        } //0 - sprawny 1 - zniszczony
        public int ShipPrice { get; set; }
        public int ShipValue { get; set; }
        public int DefaultHealth { get; set; }
        public int DefaultSpeed { get; set; }
        public int[] upgrades { get; set; }
        public int[] maxUpgrades { get; set; }


        public Ship(Texture _texture, int _shipPrice,int _defaultHealth, int _defaultSpeed ,int[] _maxUppgrades)
        {
            ShipPrice = ShipValue = _shipPrice;
            ShipTexture = _texture;
            DefaultHealth = _defaultHealth;
            DefaultSpeed = _defaultSpeed;
            maxUpgrades = _maxUppgrades;
            shipHealth = 0; 
            upgrades = new int[maxUpgrades.Count()];
        }

        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ShipPrice", ShipPrice);
            info.AddValue("ShipValue", ShipValue);
            info.AddValue("ShipHealth", ShipHealth);
            info.AddValue("upgrades", upgrades);
            info.AddValue("maxUpgrades", maxUpgrades);
        }

        public Ship(SerializationInfo info, StreamingContext context)
        {
            ShipPrice = (int)info.GetValue("ShipPrice", typeof(int));
            ShipValue = (int)info.GetValue("ShipValue", typeof(int));
            ShipHealth = (int)info.GetValue("ShipHealth", typeof(int));
            upgrades = (int[])info.GetValue("upgrades", typeof(int[]));
            maxUpgrades = (int[])info.GetValue("maxUpgrades", typeof(int[]));
        }
        #endregion
    }
}
