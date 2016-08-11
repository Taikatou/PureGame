using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureGame.DesktopGl
{
    public class FileReader : IFileReader
    {
        public string ReadAllText(string jsonPath)
        {
            return File.ReadAllText(jsonPath);
        }
    }
}
