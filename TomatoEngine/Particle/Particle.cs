using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Particle
{
    class Particle : GameObject
    {
        private int _lifeTime = 10;
        private float _speed = 1;
        private int _parent = 0;
        private int _trail = 1;
        public Particle(ImageTexture texture, float x, float y, float _rotation, float speed, int lifetime, float sizex, float sizey, byte[] color, int parent, bool physics, bool mass) : base()
        {
            Type = "Particle.Particle";
            SetSize(sizex,sizey);
            SetTexture(texture);
            _lifeTime = lifetime;
            SetRotation(_rotation);
            SetPos(x,y);
            _speed = speed;
            EnablePhysics(physics);
            SetStaticObject(false);
            EnableAirResistance(false);
            SetIsParticle(true);
            HasMass(mass);
            float xAdd = (float)Math.Sin((double)GetRotation());
            float yAdd = (float)Math.Cos((double)GetRotation());
            SetVelocity(xAdd * _speed, yAdd * _speed);
            SetPhysSize(0.1f);
            SetColor(color);
            _parent = parent;
            if ( parent  != 0 )
            {
                Z_Index = TomatoMainEngine.GetRenderObject(_parent).Z_Index - 1;
            }
            else
            {
                Z_Index = 999999999;
            }
            
        }

        public override void Update(GameSettings settings)
        {
            
            
            _lifeTime = _lifeTime - 1;
            if(_lifeTime < 0){
                _lifeTime = 0;
                TomatoMainEngine.RemoveRenderObject(EntityId);
            }
            SetRotation(GetVelocity().GetDirection());
            base.Update(settings);
        }
        public override void Draw(SharpGL.OpenGL gl)
        {

            if ( _trail > 1)
            {
                for ( int t = 0; t<_trail;t++ )
                {
                    PointFloat trailPos = GetPosition();
                    PointFloat vel = GetVelocity();
                    PointFloat dit = new PointFloat(trailPos.x - vel.x*t, trailPos.y - vel.y*t);
                    PointFloat[] pointData = RenderLogics.RectPoint(dit, GetSize(), GetRotation());
                    
                    GetTexture().UseTexure(gl);
                    gl.Begin(OpenGL.GL_QUADS);
                    byte[] col = GetColor();
                    gl.Color(col[0], col[1], col[2]);
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
            base.Draw(gl);
        }
        public override void DrawVelocity(SharpGL.OpenGL gl)
        {
            base.DrawVelocity(gl);
        }
        public override void DrawWireFrame(SharpGL.OpenGL gl)
        {
            base.DrawWireFrame(gl);
        }
        public override bool OnColision(GameObject col, float inpact)
        {
            return col.EntityId != _parent && EntityId != col.EntityId;
        }
    }
}
