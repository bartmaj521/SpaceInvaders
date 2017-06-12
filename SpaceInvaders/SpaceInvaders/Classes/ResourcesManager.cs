using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceInvaders.Classes.GUI
{
    public static class ResourcesManager
    {
        public static string resourcesPath =  string.Format("{0}\\Resources\\", Directory.GetCurrentDirectory());
        public static string xmlPath = string.Format("{0}\\Xmls\\", Directory.GetCurrentDirectory());
    }
}
