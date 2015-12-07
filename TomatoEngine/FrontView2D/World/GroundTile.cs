using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.FrontView2D.World
{
    public class GroundTile : RenderObject
    {
        PointFloat _size;
        public GroundTile(float x, float y, float width, float heigth, string texureName)
            : base("Tile", x, y, width, heigth)
        {
            SetTexture(ResourceManager.GetTexture(texureName));
        }

        public override void Draw(OpenGL gl)
        {
            PointFloat _size = GetSize();
            PointFloat[] pointData = RenderLogics.RectPoint(GetPosition(), _size, GetRotation());
            byte[] color = GetColor();
            GetTexture().UseTexure(gl);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(color[0], color[1], color[2]);
            gl.TexCoord(0, 0);
            gl.Vertex(pointData[1].x, pointData[1].y);
            gl.TexCoord(0, 1);
            gl.Vertex(pointData[0].x, pointData[0].y);
            gl.TexCoord(1, 1);
            gl.Vertex(pointData[3].x, pointData[3].y);
            gl.TexCoord(1, 0);
            gl.Vertex(pointData[2].x, pointData[2].y);
            gl.End();
        }
    }
}
