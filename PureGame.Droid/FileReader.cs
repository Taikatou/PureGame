using System.IO;

namespace PureGame.Droid
{
    public class FileReader : IFileReader
    {
        public string ReadAllText(string jsonPath)
        {
            return File.ReadAllText(jsonPath);
        }
    }
}
