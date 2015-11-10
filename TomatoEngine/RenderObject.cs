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
        private float _rot, _rotV, _maxVel = 1f;
        private PointFloat _pos = new PointFloat(0,0), _size = new PointFloat(1,1), _vel = new PointFloat(0.0f,0.0f);
        private ImageTexture _texture = ResourceManager.GetTexture("test");
        public int EntityId;
        public string Type = "DefaultObject";
        private bool _physics = false;
        private bool _staticPosition = true;
        private bool _airResistance = false;
        private float _physSize = 1f;
        private bool _hasMass = true;
        private bool _isParticle = false;
        public int Z_Index = 0;
        private byte[] _color = new byte[3];
        public RenderObject()
        {
            EntityId = TomatoMainEngine.GetNewEntityId();
            _color[0] = 255;
            _color[1] = 255;
            _color[2] = 255;
        }

        public RenderObject(float x, float y, float sx, float sy)
        {
            EntityId = TomatoMainEngine.GetNewEntityId();
            _pos.x = x;
            _pos.y = y;
            _size.x = sx;
            _size.y = sy;
            _physSize = ( _size.x + _size.y ) / 2;
            _color[0] = 255;
            _color[1] = 255;
            _color[2] = 255;
        }

        public void SetTexture(string name, OpenGL gl)
        {
            var t = ResourceManager.GetTexture(name);
            t.InitTexture(gl);
            if(t != null){
                _texture = t;
            }

        }
        public ImageTexture GetTexture()
        {
            return _texture;
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
        public float GetPhysSize()
        {
            return _physSize;
        }
        public void SetPhysSize(float size)
        {
            _physSize = size;
        }
        public bool HasMass()
        {
            return _hasMass;
        }
        public void HasMass(bool yes)
        {
           _hasMass = yes;
        }
        public void SetTexture(ImageTexture texture)
        {
            if(texture != null){
                _texture = texture;
            }
        }
        public void SetRotation(float rot)
        {
            _rot = rot;
        }
        public void SetRotationAdd(float add)
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
        public PointFloat GetSize()
        {
            return _size;
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
        public PointFloat GetVelocity(){
            return _vel;
        }
        public float GetRotationVelocity()
        {
            return _rotV;
        }
        public void SetVelocity(PointFloat velocity)
        {
            _vel.x = velocity.x;
            _vel.y = velocity.y;
        }
        public void SetVelocity(float velX, float velY)
        {
            _vel.x = velX;
            _vel.y = velY;
        }
        public void SetVelocityAdd(PointFloat velocity)
        {
            _vel.x = _vel.x + velocity.x;
            _vel.y = _vel.y + velocity.y;
        }
        public void SetVelocityAdd(float velX, float velY)
        {
            _vel.x = _vel.x + velX;
            _vel.y = _vel.y + velY;
        }
        public void SetRotationVelocity(float rotVelocity)
        {
            _rotV = rotVelocity;
        }
        public void EnablePhysics(bool on)
        {
            _physics = on;
        }
        public void SetStaticObject(bool on)
        {
            _staticPosition = on;
        }
        public bool IsStaticObject()
        {
            return _staticPosition;
        }
        public bool HasPhysics()
        {
            return _physics;
        }
        public void EnableAirResistance(bool on)
        {
            _airResistance = on;
        }
        public void SetIsParticle(bool on)
        {
            _isParticle = on;
        }
        public bool IsParticle()
        {
            return _isParticle;
        }
        public void SetColor(byte[] color)
        {
            _color[0] = color[0];
            _color[1] = color[1];
            _color[2] = color[2];
        }
        public void SetColor(byte r, byte g, byte b)
        {
            _color[0] = r;
            _color[1] = g;
            _color[2] = b;
        }
        public byte[] GetColor()
        {
            return _color;
        }
        public void SetMaxVelocity(float max)
        {
            _maxVel = max;
        }
        public float GetMaxVelocity()
        {
            return _maxVel;
        }
        public virtual void Update(GameSettings settings){
            if(!_staticPosition && (_vel.HasValue() || _rotV != 0)){
                if(_airResistance){
                    _vel.x = _vel.x - (_vel.x / 40);
                    _vel.y = _vel.y - (_vel.y / 40);
                    _rotV = _rotV - (_rotV / 40);
                }
                _vel.Max(_maxVel);
                SetPosAdd(_vel);
                SetRotationAdd(_rotV);
            }
            if ( _physics && _vel.HasValue() && PhysEngine.IsOverlappingInCircle(this) )
            {
                List<RenderObject> collisions = PhysEngine.GetAllOverlappingInCircle(this);
                PhysEngine.HandleAllObjects(this, collisions);
            }
            
        }

        public virtual bool OnColision(RenderObject col, float inpact)
        {
            return true;
        }

        public virtual void Draw(OpenGL gl)
        {
            PointFloat[] pointData = RenderLogics.RectPoint(_pos, _size,_rot);
            _texture.UseTexure(gl);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(_color[0],_color[1],_color[2]);
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
        public virtual void DrawWireFrame(OpenGL gl)
        {
            PointFloat[] pointData = RenderLogics.RectPoint(_pos, _size, _rot);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 1f);
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
        public virtual void DrawVelocity(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0f, 1f, 1f);
            gl.Vertex(_pos.x, _pos.y);
            gl.Vertex(_pos.x + _vel.x * 10f, _pos.y + _vel.y * 10f);
            gl.End();
        }
    }
}
