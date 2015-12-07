using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.FrontView2D.Levels
{
    public class DefaultLevel : Level
    {
        private Random _r = new Random();
        public override void Build(TomatoMainEngine engine)
        {
            base.Build(engine);
            for ( int x = 0; x < 100; x++)
            {
                for ( int y = 0; y < 100; y++ )
                {
                    if(y == 0){
                        var b = new FrontView2D.World.GroundTile((float)x*2 - 50, (float)-y*2, 1f, 1f, "grass");
                        //b.SetRotation((float)(Math.PI / 2 * _r.Next(0, 4)));
                        TomatoMainEngine.AddGameObject(b);
                    }
                    else
                    {
                        var b = new FrontView2D.World.GroundTile((float)x*2 - 50, (float)-y*2, 1f, 1f, "ground");
                        b.SetRotation((float)(Math.PI / 2 * _r.Next(0, 4)));
                        TomatoMainEngine.AddGameObject(b);
                    }
                    
                }
            }
        }
    }
}
