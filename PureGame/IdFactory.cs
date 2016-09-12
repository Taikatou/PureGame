using System;

namespace PureGame
{
    public class IdFactory
    {
        public static string NewId => Guid.NewGuid().ToString();
    }
}
