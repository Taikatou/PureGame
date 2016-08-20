using System.IO;

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
