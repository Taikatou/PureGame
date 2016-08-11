using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureGame
{
    public interface IFileReader
    {
        string ReadAllText(string jsonPath);
    }
}
