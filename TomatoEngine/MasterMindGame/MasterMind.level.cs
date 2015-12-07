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
            //TomatoMainEngine.AddGameObject(new MasterMindBoard());
            engine.SetSize(300,600);
            SoundPool.PlayBackgroundMusic("Arpanauts");
            //SoundPool.PlayBackgroundMusic("Bricks");
            var firstBackground = new RenderObject();
            firstBackground.SetTexture("sunset");
            firstBackground.SetSize(30,20);
            TomatoMainEngine.AddGameObject(firstBackground);
            var background = new SoundBoard.Waves();
            background.SetPos(0f,-15f);
            background.SetHeight(30);
            background.SetSize(60,0);
            TomatoMainEngine.AddGameObject(background);
        }
    }
}
