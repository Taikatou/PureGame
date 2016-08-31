using System.IO;

namespace PureGame.DesktopGl
{
    public class FileReader : IFileReader
    {
        private readonly string _homePath;
        public string ReadAllText(string jsonPath)
        {
            return File.ReadAllText(_homePath + "/" + jsonPath);
        }

        public FileReader(string homePath="")
        {
            _homePath = homePath;
        }
    }
}
