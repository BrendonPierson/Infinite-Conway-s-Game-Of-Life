using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class CellsRange
    {
        public int LowCol { get; set; }
        public int LowRow { get; set; }
        public int HighCol { get; set; }
        public int HighRow { get; set; }
        public int RowRange { get; set; }
        public int ColRange { get; set; }

        public CellsRange(List<Cell> listOfAliveCells)
        {
            GetRange(listOfAliveCells);
        }
        
        public void GetRange(List<Cell> listOfAliveCells)
        {
            List<int> rows = new List<int>();
            List<int> cols = new List<int>();
            for (int i = 0; i < listOfAliveCells.Count; i++)
            {
                rows.Add(listOfAliveCells[i].Row);
                cols.Add(listOfAliveCells[i].Col);
            }
            LowRow = rows.Min();
            HighRow = rows.Max();
            LowCol = cols.Min();
            HighCol = cols.Max();
            RowRange = HighRow - LowRow + 1;
            ColRange = HighCol - LowCol + 1;
        }
    }
}
