using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.MasterMindGame
{
    public class MasterMindBoard : RenderObject
    {
        private RenderObject[,] pins = new RenderObject[4,7];
        public MasterMindBoard()
            :base()
        {
            SetStaticObject(true);
            SetTexture("mm_board");
            SetSize(10.0f,10.0f);
            Z_Index = 1;
            
        }


        public override void Update(GameSettings settings)
        {
            base.Update(settings);
        }
        public override void Draw(SharpGL.OpenGL gl)
        {
            base.Draw(gl);
        }
    }
}
