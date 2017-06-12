using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using System.Windows.Forms;

using SFML.Graphics;
using SFML.System;

using SpaceInvaders.Classes.Enemies;

// Klasa tworzy nowych przeciwników na podstawie pliku poziomu

namespace SpaceInvaders.Classes
{
    class EnemySpawner
    {
        // Konstruktor singletona
        #region Singleton
        private static EnemySpawner instance = null;
        public static EnemySpawner Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemySpawner();
                }
                return instance;
            }
            set
            {

            }
        }
        private EnemySpawner()
        {
            //enemyTextures = new List<Texture>();
            enemyPrefabs = new List<Enemy>();

            for (int i = 0; i < 1; i++)
            {
                //enemyTextures.Add(new Texture(string.Format("enemy{0}.png", i)));
                enemyPrefabs.Add(new Randomer(new Texture(string.Format("enemy{0}.png", i)), new int[1] { 0 }, 1f, new Vector2f(1200, -250), new Vector2f(0.3f, 0.3f), 100));
            }            
        }
        #endregion

        // Klasa przchowująca informacje o przciwnikach do ustawienia na planszy
        class EnemyData
        {
            public enum SpawnType
            {
                absolute,   // Ilość czasu od zrespienia poprzedniego przeciwnika
                relative    // Ilość czasu od śmierci ostatniego przeciwnika na planszy
            };
            public Enemy enemy;
            public float timeToSpawn;
            public SpawnType spawnType; 
        }

        //List<Texture> enemyTextures;
        List<Enemy> enemyPrefabs;

        List<EnemyData> enemiesToSpawn;

        List<Enemy> enemyList;

        public bool isEmpty = false;

        public bool initialize(int lvlNb, ref List<Enemy> _enemyList)
        {
            try
            {
                // Wczytanie pliku xml
                enemyList = _enemyList;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(string.Format("level{0}.xml", lvlNb));

                // Inicjalizacja pierwszego przeciwnika
                Randomer.value = Int32.Parse(xmldoc.SelectSingleNode("main").SelectSingleNode("enemy1mon").InnerText);

                // Trochę na około ale działa
                Image shotimg = new Image("enemyBullet.png");
                shotimg.FlipHorizontally();
                Texture shot = new Texture(shotimg);

                //Texture shot = new Texture("enemybullet.png");

                Randomer.bulletPrefab = new Bullet(ref shot, Int32.Parse(xmldoc.SelectSingleNode("main").SelectSingleNode("enemy1dmg").InnerText), new Vector2f(1f, 1f));


                XmlNodeList enemies = xmldoc.SelectSingleNode("main").SelectSingleNode("enemies").SelectNodes("enemy");
                enemiesToSpawn = new List<EnemyData>();

                foreach (XmlNode enemy in enemies)
                {
                    // Tworzenie kopii wzoru przeciwnika
                    EnemyData enemyData = new EnemyData();
                    int tmp;
                    Int32.TryParse(enemy.SelectSingleNode("enemyType").InnerText, out tmp);
                    if (tmp >= enemyPrefabs.Count) throw new Exception("Błędny numer przeciwnika"); // TODO throwuj błąd nieprawidłowy kod przeciwnika
                    enemyData.enemy = (Enemy)enemyPrefabs[tmp].Clone();

                    // Ustalanie pozycji startowej
                    Vector2f tmpVec = new Vector2f();
                    tmpVec.X = float.Parse(enemy.SelectSingleNode("x").InnerText);
                    tmpVec.Y = float.Parse(enemy.SelectSingleNode("y").InnerText);
                    enemyData.enemy.setColliderPosition(tmpVec);
                    //enemyData.enemy.animation.animationSprite.Position = tmpVec;               

                    // Ustalanie spawnu
                    enemyData.timeToSpawn = float.Parse(enemy.SelectSingleNode("spawnTimer").InnerText);
                    tmp = Int32.Parse(enemy.SelectSingleNode("spawnType").InnerText);
                    if (tmp > 1 || tmp < 0) throw new Exception("Błędny rodzaj tworzenia preciwnika");
                    enemyData.spawnType = (EnemyData.SpawnType)tmp;

                    enemiesToSpawn.Add(enemyData);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void update(float deltaTime)
        {
            if (enemiesToSpawn.Count > 0)
            {
                if (enemiesToSpawn.First().spawnType == EnemyData.SpawnType.absolute)
                {
                    enemiesToSpawn.First().timeToSpawn -= deltaTime;
                    if (enemiesToSpawn.First().timeToSpawn <= 0)
                    {
                        enemyList.Add(enemiesToSpawn.First().enemy);
                        enemiesToSpawn.RemoveAt(0);
                    }
                }
                else if(enemiesToSpawn.First().spawnType == EnemyData.SpawnType.relative)
                {
                    if(enemyList.Count == 0)
                    {
                        enemiesToSpawn.First().timeToSpawn -= deltaTime;
                        if (enemiesToSpawn.First().timeToSpawn <= 0)
                        {
                            enemyList.Add(enemiesToSpawn.First().enemy);
                            enemiesToSpawn.RemoveAt(0);
                        }
                    }
                }
            }
            else
            {
                isEmpty = true;
            }
        }
    }
}