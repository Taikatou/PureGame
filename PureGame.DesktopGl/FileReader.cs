using System.IO;

namespace PureGame.DesktopGl
{
    public class FileReader : IFileReader
    {
        private string HomePath;
        public string ReadAllText(string jsonPath)
        {
            return File.ReadAllText(HomePath + "/" + jsonPath);
        }

        public FileReader(string HomePath="")
        {
            this.HomePath = HomePath;
        }
    }
}
