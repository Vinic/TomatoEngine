using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.TimedFireworks
{
    public class TimedLevel : Level
    {
        private List<TimedCannon> _cannons = new List<TimedCannon>();
        private int _time = 0;
        private Random r = TomatoMainEngine.GameRandom;
        private bool _f = false;
        public  TimedLevel(): base()
        {

        }
        //Build the level / init map / add player etc
        public override void Build(TomatoMainEngine engine)
        {
            //SoundPool.PlayBackgroundMusic("ComeandFindMe");
            //SoundPool.PlayBackgroundMusic("Arpanauts");
            _cannons.Add(new TimedCannon(-10f,-15f,new int[5]{1000,2000,3000,4000,5000}));
            _cannons.Add(new TimedCannon(10f, -15f, new int[5] { 1000, 2000, 3000, 4000, 5000 }));
            PhysEngine.Mode = PhysMode.CircleBox;
        }
        //Update the level so objects can be added or modifyed
        public override void Update(GameSettings settings)
        {
            int time = SoundPool.GetBackgroundMusicPos();
            _time = time;
            foreach(TimedCannon c in _cannons){
                c.Update(time);
            }
            if(ControlKeys.IsKeyDown("f") != _f){
                _f = ControlKeys.IsKeyDown("f");
                //var fire = new FireWork(new PointFloat((float)r.Next(-150,150)/10f, -15), new byte[3] { (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255) });
                //fire.Explode = true;
                //TomatoMainEngine.AddGameObject(fire);
            }
        }
        //Draw the level(score or a hud)
        public override void Draw(OpenGL gl)
        {
            gl.DrawText(100, 100, 1f, 1f, 1f, "verdana", 20, _time.ToString());
        }
    }
}
