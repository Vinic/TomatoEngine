using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public class RenderObject
    {
        public bool RenderOuterScreen = false;
        private float _rot;
        private PointFloat _pos = new PointFloat(0,0), _size = new PointFloat(1,1);
        private ImageTexture _texture = ResourceManager.GetTexture("test");
        public int EntityId;
        public RenderObject()
        {
            EntityId = TomatoMainEngine.GetNewEntityId();
        }

        public RenderObject(float x, float y, float sx, float sy)
        {
            EntityId = TomatoMainEngine.GetNewEntityId();
            _pos.x = x;
            _pos.y = y;
            _size.x = sx;
            _size.y = sy;
        }

        public void SetTexture(string name, OpenGL gl)
        {
            var t = ResourceManager.GetTexture(name);
            t.InitTexture(gl);
            if(t != null){
                _texture = t;
            }
        }
        public void SetSize(PointFloat size)
        {
            _size.x = size.x;
            _size.y = size.y;
        }
        public void SetSize(float sx, float sy)
        {
            _size.x = sx;
            _size.y = sy;
        }
        public void SetTexture(ImageTexture texture)
        {
            if(texture != null){
                _texture = texture;
            }
        }
        public void SetRot(float rot)
        {
            _rot = rot;
        }
        public void SetRotAdd(float add)
        {
            _rot = _rot + add;
        }
        public void SetPos(float x, float y)
        {
            _pos.x = x;
            _pos.y = y;
        }
        public void SetPos(PointFloat pos)
        {
            _pos.x = pos.x;
            _pos.y = pos.y;
        }
        public PointFloat GetPosition()
        {
            return _pos;
        }
        public void SetPosAdd(PointFloat add)
        {
            _pos.x = _pos.x + add.x;
            _pos.y = _pos.y + add.y;
        }
        public void SetPosAdd(float x, float y)
        {
            _pos.x = _pos.x + x;
            _pos.y = _pos.y + y;
        }
        public float GetRotation(){
            return _rot;
        }
        public virtual void Update(GameSettings settings){
            
        }


        public void Draw(OpenGL gl)
        {
            PointFloat[] pointData = RenderLogics.RectPoint(_pos, _size,_rot);
            _texture.UseTexure(gl);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(1f, 1f, 1f);
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
        public void DrawWireFrame(OpenGL gl)
        {
            PointFloat[] pointData = RenderLogics.RectPoint(_pos, _size, _rot);
            gl.Begin(OpenGL.GL_LINES);
            
            gl.Color(1f, 1f, 0f);
            gl.Vertex(pointData[1].x, pointData[1].y);
            gl.Vertex(pointData[0].x, pointData[0].y);
            gl.Vertex(pointData[0].x, pointData[0].y);
            gl.Vertex(pointData[3].x, pointData[3].y);
            gl.Vertex(pointData[3].x, pointData[3].y);
            gl.Vertex(pointData[2].x, pointData[2].y);
            gl.Vertex(pointData[2].x, pointData[2].y);
            gl.Vertex(pointData[1].x, pointData[1].y);
            gl.End();
        }
    }
}
