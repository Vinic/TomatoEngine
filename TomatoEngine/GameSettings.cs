using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    class GameSettings
    {
        private float _fSpeed;
        private bool[] _buttons;

		public GameSettings(float fSpeed, bool[] buttons)
        {
            _fSpeed = fSpeed;
            _buttons = buttons;
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
