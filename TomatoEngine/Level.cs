using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public class Level
    {
        public Level()
        {

        }
        //Build the level / init map / add player etc
        public virtual void Build(TomatoMainEngine engine)
        {

        }
        //Update the level so objects can be added or modifyed
        public virtual void Update(GameSettings settings)
        {

        }
        //Draw the level(score or a hud)
        public virtual void Draw(OpenGL gl)
        {

        }
    }
}
