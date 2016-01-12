using SharpGL;
using SharpGL.SceneGraph.Lighting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TomatoEngine.Lights
{
    public class Light : GameObject
    {
        private int _parentId;
        private byte _r, _g, _b;
        private bool _drawSprite = true;
        private SharpGL.SceneGraph.Lighting.Light _light;
        private SharpGL.SceneGraph.Vertex _vertexPos = new SharpGL.SceneGraph.Vertex();
        private bool _startup = false;
        public Light() 
            : base()
        {
            _light = new SharpGL.SceneGraph.Lighting.Light();
            SetTexture(ResourceManager.GetTexture("light"));
            SetStaticObject(true);
            EnableAirResistance(false);
            EnablePhysics(false);
            Z_Index = 100;
        }
        public Light(int r, int g, int b, int parentId) 
            : base()
        {
            _light = new SharpGL.SceneGraph.Lighting.Light();
            _light.On = true;
            Color col = Color.FromArgb(255,r, g, b);
            _light.Ambient = col;
            _light.Diffuse = col;
            _light.Specular = col;
            SetTexture(ResourceManager.GetTexture("light"));
            SetStaticObject(true);
            EnableAirResistance(false);
            EnablePhysics(false);
            //_r = r;
            //_g = g;
            //_b = b;
            _parentId = parentId;
        }

        public void SetColor()
        {
            
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            if ( _parentId != 0 )
            {
                GameObject parent = TomatoMainEngine.GetRenderObject(_parentId);
                SetPos(parent.GetPosition());
                SetRotation(parent.GetRotation());
                _vertexPos.X = GetPosition().x;
                _vertexPos.Y = GetPosition().y;
                _vertexPos.Z = 1;
                _light.Position = _vertexPos;
            }
        }

        public override void Draw(OpenGL gl)
        {
            base.Draw(gl);

            if ( !_startup )
            {
                _startup = true;
                //_light.Push(gl);
                _light.Bind(gl);
                
            }
        }
    }
}
