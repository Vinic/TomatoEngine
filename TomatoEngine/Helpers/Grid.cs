using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Helpers
{
    public class Grid
    {
        int _rows, _cols;
        GameObject[,] _objects;
        Grid(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _objects = new GameObject[_rows, _cols];
        }

        public void Add(GameObject obj, int row, int col)
        {

        }

        public void Update()
        {

        }

    }
}
