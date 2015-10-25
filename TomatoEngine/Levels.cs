using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    static class Levels
    {
        public static void SpaceTest(TomatoMainEngine engine){
            TomatoMainEngine.GameObjects.Add(new SpaceGame.SpaceShip());
            TomatoMainEngine.AddGameObject(new SpaceGame.Asteroid());
        }
    }
}
