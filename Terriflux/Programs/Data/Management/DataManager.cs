using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Data.Management
{
    public static partial class DataManager
    {
        public static StreamReader Load(string fileName)
        {
            string filePath = (Paths.DATA + fileName + Paths.TEXTEXT);
            if (File.Exists(filePath))
            {
                return new StreamReader(filePath);
            }
            else
            {
                throw new FileNotFoundException($"Unable to find the specified file at '{filePath}'");
            }
        }

        public static StreamReader LoadBuildingData()
        {
            return Load("Buildings");
        }
    }
}
