using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public class GameSettings
    {
        private float _fSpeed;
        public bool SizeLocked { get; set; }

		public GameSettings(float fSpeed)
        {
            _fSpeed = fSpeed;
        }

        public float GetSpeed()
        {
            return _fSpeed;
        }

		public void GamePause(bool Pause)
        {
            if (Pause)
            {
				// Iets zoals Game.Pause();
            }
        }
    }
}
