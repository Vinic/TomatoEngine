using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.MasterMindGame
{
    public class MasterMind : TomatoEngine.Level
    {
        public override void Build(TomatoMainEngine engine)
        {
            base.Build(engine);
            engine.SetSize(600,600);
            engine.LockSize(true);
            SoundPool.PlayBackgroundMusic("Arpanauts");
            var firstBackground = new GameObject();
            firstBackground.SetTexture("sunset");
            firstBackground.SetSize(30,20);
            firstBackground.Z_Index = -1;
            TomatoMainEngine.AddGameObject(firstBackground);
            var background = new SoundBoard.Waves();
            background.SetPos(0f,-15f);
            background.SetHeight(30);
            background.SetSize(60,0);
            background.Z_Index = 1;
            TomatoMainEngine.AddGameObject(background);
            TomatoMainEngine.AddGameObject(new MasterMindBoard());
        }
    }
}
