using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameBoard : Board
    {
        public CellsRange Range { get; set; }
        public List<Cell> AliveCells { get; set; }
        private List<Cell> ressurectionCellCandidates;

        public GameBoard()
        {
            ressurectionCellCandidates = new List<Cell>();
        }
        public GameBoard(List<Cell> aliveCells)
        {
            ressurectionCellCandidates = new List<Cell>();
            AliveCells = aliveCells;
            Range = new CellsRange(aliveCells);
        }

        // Return the Count of alive neighbors
        public int NeighborsAliveCount(Cell cell)
        {
            int aliveCount = 0;
            List<Cell> neighbors = GenerateNeighborsList(cell);

            foreach(var neighbor in neighbors)
            {
                if (AliveCells.Contains(neighbor))
                {
                    aliveCount++;
                }
            }
            return aliveCount;
        }

        // Returns lists of lists of all neighbor coordinates from the input list
        public List<Cell> GenerateNeighborsList(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();
            neighbors.Add(new Cell (cell.Row - 1, cell.Col - 1 ));
            neighbors.Add(new Cell (cell.Row - 1, cell.Col));
            neighbors.Add(new Cell (cell.Row - 1, cell.Col + 1));
            neighbors.Add(new Cell (cell.Row, cell.Col - 1 ));
            neighbors.Add(new Cell (cell.Row, cell.Col + 1 ));
            neighbors.Add(new Cell (cell.Row + 1, cell.Col - 1 ));
            neighbors.Add(new Cell (cell.Row + 1, cell.Col ));
            neighbors.Add(new Cell (cell.Row + 1, cell.Col + 1 ));
            ressurectionCellCandidates.AddRange(neighbors);
            return neighbors;
        }

        // Bring cells with three living neighbors to life
        public void CheckBringCellToLife(List<Cell> deadNeighbors)
        {
            CellEqualityComparer cellComparer = new CellEqualityComparer();
            Dictionary<Cell, int> neighborDict = new Dictionary<Cell, int>(cellComparer);

            foreach (var neighbor in deadNeighbors)
            {
                if (neighborDict.ContainsKey(neighbor))
                {
                    neighborDict[neighbor] += 1;
                }
                else
                {
                    neighborDict.Add(neighbor, 1);
                }
            }

            foreach (var cell in neighborDict)
            {
                if (cell.Value == 3)
                {
                    AliveCells.Add(cell.Key);
                }
            }
        }

        // Mark a cell to be removed if live neighborsCount < 2 || > 3
        public void ApplyKillRules(Cell cell, int numLiveNeighbors)
        {
            if(numLiveNeighbors > 3 || numLiveNeighbors < 2)
            {
                cell.MarkForChange = true;
            }
        }

        // kill cells that are marked using filter
        public void RemoveCellsThatDied()
        {
            AliveCells = AliveCells.FindAll(delegate (Cell cell) { return !cell.MarkForChange; });
        }

        // Apply the four rules
        public void ApplyAllRules()
        {
            for (int i = 0; i < AliveCells.Count; i++)
            {
                int liveNeighbors = NeighborsAliveCount(AliveCells[i]);
                ApplyKillRules(AliveCells[i], liveNeighbors);
            }
            RemoveCellsThatDied();
            CheckBringCellToLife(ressurectionCellCandidates);
            ressurectionCellCandidates.Clear();
        }

        // Generate List of Lists for visualizer
        public List<List<bool>> GenerateGrid()
        {
            Range = new CellsRange(AliveCells);
            List<List<bool>> cells = new List<List<bool>>();
            for(var row = 0; row < Range.RowRange; row++)
            {
                cells.Add(GenerateRow(Range.ColRange));
            }
            foreach(var cell in AliveCells)
            {
                cells[cell.Row - Range.LowRow][cell.Col - Range.LowCol] = true;
            }
            return cells;
        }
        // Helper function to help create each row in the grid
        public List<bool> GenerateRow(int size)
        {
            List<bool> row = new List<bool>();
            for (int i = 0; i < size; i++)
            {
                row.Add(false);
            }
            return row;
        }

        public void Tick()
        {
            ApplyAllRules();   
        }

        public List<List<bool>> ToList()
        {
            return GenerateGrid();
        }
    }
}
