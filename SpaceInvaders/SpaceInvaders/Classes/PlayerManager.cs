using System;
using SFML.Graphics;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace SpaceInvaders.Classes.GUI
{
    public enum stats
    {
        repair = -1,
        shipSpeed = 0,
        armor = 1,
        accuracy = 2,
        fireSpeed = 3,
        weaponDmg = 4,
    }
    public enum powerups
    {
        rocket = 0,
        laser = 1,
        bomb = 2,
        wave = 3,
        barrier = 4
    }

    [Serializable()]
    public class PlayerManager : ISerializable
    {
        
        private int currentShip = 0;
        //właściwości
        public ShipPrefab[] ShipPrefabs { get; private set; }
        public int MissionProgress { get; set; }
        public Ship ShipInfo { get; private set; }
        public int PlayerMoney { get; private set; }
        public string PlayerName { get; set; }

        public int[] Powerups { get; private set; }

        #region Singleton
        private static PlayerManager instance = null;

        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager("");
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private PlayerManager(string _playerName)
        {
            Powerups = new int[5];
            MissionProgress = 1;
            PlayerMoney = 1000000;
            PlayerName = _playerName;
            readPrefabs();
            ShipInfo = new Ship(new Texture(ResourcesManager.resourcesPath + ShipPrefabs[currentShip].TexturePath), ShipPrefabs[currentShip].Price,ShipPrefabs[currentShip].DefaultHealth,ShipPrefabs[currentShip].DefaultSpeed, ShipPrefabs[currentShip].MaxUpgrades);
        }
        #endregion

        #region Serialization

        public PlayerManager(SerializationInfo info, StreamingContext context)
        {
            MissionProgress = (int)info.GetValue("MissionProgress", typeof(int));
            ShipInfo = (Ship)info.GetValue("ShipInfo", typeof(Ship));
            PlayerMoney = (int)info.GetValue("PlayerMoney", typeof(int));
            PlayerName = (string)info.GetValue("PlayerName", typeof(string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MissionProgress", MissionProgress);
            info.AddValue("ShipInfo", ShipInfo, typeof(Ship));
            info.AddValue("PlayerMoney", PlayerMoney);
            info.AddValue("PlayerName", PlayerName);
        }

        #endregion

        private void readPrefabs()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("rocketInfo.xml");

            XmlNode rocketsNode = xmldoc.SelectSingleNode("/rockets");
            int count = rocketsNode.ChildNodes.Count;
            XmlNode currRocketNode;
            XmlNode currStatNode;
            ShipPrefabs = new ShipPrefab[count];
            string name;
            string path;
            int defaultHealth;
            int defaultSpeed;
            int price;
            for (int i = 0; i < count; i++)
            {
                int[] maxUpgrades = new int[5];
                currRocketNode = rocketsNode.SelectSingleNode(string.Format("r{0}", i));
                name = currRocketNode.SelectSingleNode("name").InnerText;
                path = currRocketNode.SelectSingleNode("texture").InnerText;
                for (int j = 0; j < 5; j++)
                {
                    currStatNode = currRocketNode.SelectSingleNode(string.Format("s{0}", j));
                    Int32.TryParse(currStatNode.InnerText, out maxUpgrades[j]);
                }
                Int32.TryParse(currRocketNode.SelectSingleNode("price").InnerText, out price);
                Int32.TryParse(currRocketNode.SelectSingleNode("health").InnerText, out defaultHealth);
                Int32.TryParse(currRocketNode.SelectSingleNode("speed").InnerText, out defaultSpeed);
                ShipPrefabs[i] = new ShipPrefab
                {
                    ShipName = name,
                    Price = price,
                    TexturePath = path,
                    MaxUpgrades = maxUpgrades,
                    DefaultHealth = defaultHealth,
                    DefaultSpeed = defaultSpeed
                };
            }
        }

        public void changeShip(int ship)
        {
            int cost = ShipPrefabs[ship].Price;
            int moneyAfterSelling = PlayerMoney += ShipInfo.ShipValue;
            if (cost <= moneyAfterSelling)
            {
                PlayerMoney = moneyAfterSelling;
                PlayerMoney -= cost;
                currentShip = ship;
                ShipInfo = new Ship(new Texture(ResourcesManager.resourcesPath + ShipPrefabs[currentShip].TexturePath), ShipPrefabs[currentShip].Price, ShipPrefabs[currentShip].DefaultHealth, ShipPrefabs[currentShip].DefaultSpeed, ShipPrefabs[currentShip].MaxUpgrades);
            }
        }
        public void donatePlayer(int value)
        {
            PlayerMoney += value;
        }
        public bool upgradeShip(stats _stat)
        {
            int i = (int)_stat;
            int cost = upgradeCost(_stat);
            if (cost <= PlayerMoney)
            {
                if (ShipInfo.upgrades[i] < ShipInfo.maxUpgrades[i])
                {
                    ShipInfo.ShipValue += cost;
                    PlayerMoney -= cost;
                    ShipInfo.upgrades[i]++;
                    return true;
                }
            }
            return false;
        }
        public int upgradeCost(stats _stat)
        {
            int i = (int)(_stat);
            if (i < 0)
                return (int)(0.05 * ShipInfo.ShipValue);
            return (int)(0.2 * ShipInfo.ShipPrice * Math.Exp(ShipInfo.upgrades[i]));
        }

        public bool addPowerup(powerups _powerup)
        {
            int cost = powerupCost(_powerup);
            if(cost<=PlayerMoney)
            {
                PlayerMoney -= cost;
                Powerups[(int)_powerup]++;
                return true;
            }
            return false;
        }

        public int powerupCost(powerups _powerup)
        {
            return (int)(0.2 * ShipInfo.ShipValue);
        }
        public void repairShip(float toRepair)
        {
            int cost = upgradeCost(stats.repair);
            if (PlayerMoney >= cost)
            {
                if (ShipInfo.ShipHealth > 0)
                {
                    ShipInfo.ShipHealth -= toRepair;
                    PlayerMoney -= cost;
                }
            }
        }

        public void damageShip(float toDamage)
        {
            ShipInfo.ShipHealth += toDamage;
        }

        public class ShipPrefab
        {
            public string ShipName { get; set; }
            public string TexturePath { get; set; }
            public int[] MaxUpgrades { get; set; }
            public int Price { get; set; }
            public int DefaultHealth { get; set; }
            public int DefaultSpeed { get; set; }
        }
    }
}
