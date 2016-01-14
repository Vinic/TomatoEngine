using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.MasterMindGame
{
    public class Pawn : GameObject
    {
        public PawnColor _pawnColor {get; private set;}
        public PawnType _pawnType { get; private set; }

        public Particle.ParticleSystem _effect;

        public bool Selected;
        public Pawn(PawnColor color, PawnType type): base("MasterMindPawn")
        {
            _effect = new Particle.ParticleSystem("light");
            _effect.SetLifeTime(10, 20);
            _effect.SetParent(this.EntityId);
            _effect.SetSize(0.2f, 0.2f);
            _effect.SetSpread(Helpers.PhysicsAndPositions.PI * 2);
            _effect.SetRandomSpeed(true, 0.05f);
            SetPawnColor(color);
            SetPawnType(type);
            SetTexture("pawn");
            Selected = false;
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            if(Selected){
                _effect.SetPos(GetPosition());
                _effect.Blow(0.05f, false);
            }            
        }

        public void SetPawnColor(PawnColor color)
        {
            _pawnColor = color;
            SetColor(ConvertPawnColor(color));
            _effect.SetColor(ConvertPawnColor(color));
        }
        public PawnColor GetPawnColor()
        {
            return _pawnColor;
        }

        public void SetPawnType(PawnType pawnType)
        {
            _pawnType = pawnType;
            if(pawnType == PawnType.Normal){
                SetSize(0.55f,0.55f);
            }
            else if( pawnType == PawnType.Small)
            {
                SetSize(0.25f,0.25f);
            }
        }

        public override void Draw(SharpGL.OpenGL gl)
        {
            if(_pawnColor != PawnColor.None){
                base.Draw(gl);
            }
        }

        public void NextColor()
        {
            int newColor = (int)_pawnColor + 1;
            if ( newColor >= 8 )
            {
                newColor = 1;
            }
            SetPawnColor((PawnColor)newColor);
            _effect.Blow(0.2f, 20, false);
        }
        public void PrevColor()
        {
            int newColor = (int)_pawnColor - 1;
            if ( newColor <= 0 )
            {
                newColor = 7;
            }
            SetPawnColor((PawnColor)newColor);
            _effect.Blow(0.2f, 20, false);
        }

        public static byte[] ConvertPawnColor(PawnColor color)
        {
            switch(color){
                case PawnColor.None:
                    return new byte[3] { 0, 0, 0 };
                    break;
                case PawnColor.Red:
                    return new byte[3] { 255, 0, 0 };
                    break;
                case PawnColor.Green:
                    return new byte[3] { 0, 255, 0 };
                    break;
                case PawnColor.Blue:
                    return new byte[3] { 0, 0 ,255};
                    break;
                case PawnColor.Orange:
                    return new byte[3] { 255, 100, 0 };
                    break;
                case PawnColor.Yellow:
                    return new byte[3] { 255, 255, 0 };
                    break;
                case PawnColor.Purple:
                    return new byte[3] { 255, 0, 255 };
                    break;
                case PawnColor.Brown:
                    return new byte[3] { 200, 110, 110 };
                    break;
                case PawnColor.Black:
                    return new byte[3] { 0, 0, 0 };
                    break;
                case PawnColor.White:
                    return new byte[3] { 255, 255, 255 };
                    break;
                default:
                    return new byte[3] { 0, 0, 0 };
                    break;

            }
        }
    }

    public enum PawnColor
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Orange = 4,
        Yellow = 5,
        Purple = 6,
        Brown = 7,
        Black = 8,
        White = 9
    }
    public enum PawnType
    {
        Normal = 0,
        Small = 1
    }
}
