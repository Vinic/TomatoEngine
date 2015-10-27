﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    static class Levels
    {
        public static void SpaceTest(TomatoMainEngine engine){
            //adds the background
            RenderObject background = new RenderObject(0,0,30,30);
            background.SetTexture(ResourceManager.GetTexture("background"));
            //TomatoMainEngine.GameObjects.Add(background);
            //add a space ship
            TomatoMainEngine.GameObjects.Add(new SpaceGame.SpaceShip());
            Random r = new Random();
            for ( int i = 0; i < 200; i++)
            {
                var a = new SpaceGame.Asteroid();
                a.SetPos(r.Next(-200, 200), r.Next(-200, 200));
                TomatoMainEngine.AddGameObject(a);
            }
            
        }
    }
}
