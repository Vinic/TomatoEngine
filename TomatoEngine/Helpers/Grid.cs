using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Helpers
{
    public class Grid : GameObject
    {
        int _rows, _cols;
        float _stepX, _stepY, _beginX, _beginY;
        GameObject[,] _objects;
        public bool DisableSizing = false;
        public Grid(int rows, int cols)
            : base("Grid")
        {
            _rows = rows;
            _cols = cols;
            _objects = new GameObject[_rows, _cols];
            Z_Index = 100000;
        }

        public void Add(GameObject obj, int row, int col)
        {
            _objects[row, col] = obj;
            TomatoMainEngine.AddGameObject(obj);
        }

        public GameObject ObjectAt(int row, int col)
        {
            return _objects[row, col];
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            _stepX = GetSize().x * 2 / ( _cols + 1 );
            _stepY = GetSize().y * 2 / ( _rows + 1 );
            _beginX = GetPosition().x - ( GetSize().x / 2.0f );
            _beginY = GetPosition().y - ( GetSize().y / 2.0f );
            for ( int row = 0; row < _rows; row++ )
            {
                for ( int col = 0; col < _cols; col++ )
                {
                    if ( _objects[row, col] != null )
                    {
                        if ( !DisableSizing )
                        {
                            _objects[row, col].SetSize(_stepX, _stepY);
                        }
                        _objects[row, col].SetPos(_beginX + _stepX * col - 0.30f, _beginY + _stepY * ( row - 1f ));
                    }

                }
            }
        }

        public override void Draw(SharpGL.OpenGL gl)
        {

        }


    }
}
