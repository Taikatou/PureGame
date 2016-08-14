using Microsoft.Xna.Framework;
using SmallGame;
using System;

namespace PureGame.Engine.Scripts
{
    public interface IScriptService
    {
        void RegisterCollection(ScriptCollection collection);
        void RegisterParameterHandler<P>(Func<P> fetcher);
        void Run(string scriptName, IGameObject gob);
        void Update(GameTime time);
        void Load(string scriptPath);
    }
}
