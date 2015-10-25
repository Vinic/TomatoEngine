using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    static class Levels
    {
        public static void SpaceTest(TomatoMainEngine engine){
            //adds the background
            //RenderObject background = new RenderObject(0,0,30,30);
            //background.SetTexture(ResourceManager.GetTexture("background"));
            //TomatoMainEngine.GameObjects.Add(background);
            //add a space ship
            TomatoMainEngine.GameObjects.Add(new SpaceGame.SpaceShip());

        }
    }
}
