using System;
using SFML.Graphics;
using System.Runtime.Serialization;
using System.IO;

namespace SpaceInvaders.Classes.GUI
{
    public enum stats
    {
        shipSpeed = 0,
        armor = 1,
        accuracy = 2,
        fireSpeed = 3,
        weaponDmg = 4,
    }

    [Serializable()]
    public class PlayerManager: ISerializable
    {
        private string RESNAME = string.Format("{0}\\Resources\\", Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        //właściwości
        public int MissionProgress { get; set; }
        public Ship ShipInfo { get; private set; }
        public int PlayerMoney { get; private set; }
        public string PlayerName { get; set; }

     
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
            MissionProgress = 1;
            PlayerMoney = 500;
            PlayerName = _playerName;
            ShipInfo = new Ship(new Texture(RESNAME +"blank.png"), 1000, new int[5] { 3, 4, 2, 1, 2 });
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

        public void donatePlayer(int value)
        {
            PlayerMoney += value;
        }
        
        public bool upgradeShip(stats _stat)
        {
            int i = (int)_stat;
            int cost = upgradeCost(_stat);
            if (cost<=PlayerMoney)
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
            return (int)(0.1 * ShipInfo.ShipPrice * Math.Exp(ShipInfo.upgrades[i]));
        }
        public void repairShip(float toRepair)
        {
            ShipInfo.ShipHealth -= toRepair;
        }
        



       

    }
}
