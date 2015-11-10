using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{

    //THIS IS THE OLD WAY TO MAKE LEVELS
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
                a.SetPos(r.Next(-100, 100), r.Next(-100, 100));
                a.SetRotation((float)r.Next(100) / 50);
                TomatoMainEngine.AddGameObject(a);
            }
            
            
        }

        public static void BallDemo(TomatoMainEngine engine)
        {
            SoundPool.PlayBackgroundMusic("Bricks");
            Random r = new Random();
            for ( int i = 0; i < 2; i++ )
            {
                var a = new ParticleBallDemo.FlammingBall();
                a.SetPos(r.Next(-10, 10), r.Next(-10, 10));
                a.SetRotation((float)r.Next(100) / 50);
                TomatoMainEngine.AddGameObject(a);
            }
        }
    }
}
