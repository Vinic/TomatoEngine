using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public interface ILevel
    {
        //Build the level / init map / add player etc
        void Build(TomatoMainEngine engine);

        //Update the level so objects can be added or modifyed
        void Update(GameSettings settings);

        //Draw the level(score or a hud)
        void Draw(OpenGL gl);

    }
}
