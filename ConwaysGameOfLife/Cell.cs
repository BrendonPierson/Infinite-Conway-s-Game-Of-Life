using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool MarkForChange { get; set; }

        public Cell() { }
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
            MarkForChange = false;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }
            // If parameter cannot be cast to Point return false.
            Cell comparison = obj as Cell;
            if ((System.Object)comparison == null)
            {
                return false;
            }
            // Return true if the fields match:
            return (Row == comparison.Row) && (Col == comparison.Col);
        }

        public bool Equals(Cell comparison)
        {
            // If parameter is null return false:
            if ((object)comparison == null)
            {
                return false;
            }
            // Return true if the fields match:
            return (Row == comparison.Row) && (Col == comparison.Row);
        }
    }
}
