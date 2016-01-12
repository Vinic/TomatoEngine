using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.TimedFireworks
{
    public class TimedCannon
    {
        private List<int> _times = new List<int>();
        private byte[] _color;
        private Random r = TomatoMainEngine.GameRandom;
        private PointFloat _pos;
        private Particle.ParticleSystem _firePar = new Particle.ParticleSystem("light");
        public TimedCannon(float posx, float posy, int[] times)
        {
            _times = times.ToList();
            _color = new byte[3] { (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255) };
            _pos = new PointFloat(posx, posy);
            _firePar.SetLifeTime(3, 6);
            _firePar.SetPhysics(false);
            _firePar.SetSpread(0.5f);
            _firePar.SetSize(0.1f,0.1f);
            _firePar.SetRandomSpeed(true,0.5f);
            _firePar.SetColor(255, 255, 100);
        }
        public void Update(int time)
        {
            for ( int i=0; i <_times.Count;i++ )
            {
                int t = _times[i];
                if ( t <= time )
                {
                    _times.Remove(t);
                    Fire();
                    _color[0] = (byte)r.Next(255);
                    _color[1] = (byte)r.Next(255);
                    _color[2] = (byte)r.Next(255);
                    i--;
                }
            }
        }
        public void Fire()
        {
            TomatoMainEngine.AddGameObject(new FireWork(_pos, _color));
            _firePar.SetPos(_pos);
            _firePar.Blow(0.5f,3,false);
        }
    }
}
