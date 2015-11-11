using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    class CellEqualityComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell cell1, Cell cell2)
        {
            if (cell2 == null && cell1 == null)
                return true;
            else if (cell1 == null | cell2 == null)
                return false;
            else if (cell1.Row == cell2.Row && cell1.Col == cell2.Col)
                return true;
            else
                return false;
        }

        public int GetHashCode(Cell bx)
        {
            int hCode = bx.Col ^ bx.Row;
            return hCode.GetHashCode();
        }
    }
}

