using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConwaysGameOfLife;
using System.Collections.Generic;

namespace ConwaysTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NewBoardIsNotNull()
        {
            GameBoard board = new GameBoard();
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void NewBoardAcceptsAliveArgument()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell(1, 2));
            GameBoard board = new GameBoard(alive);
            Assert.AreEqual(1, board.AliveCells.Count);
            CollectionAssert.AreEqual(alive, board.AliveCells);
        }

        [TestMethod]
        public void GetAliveCellRange()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell(1, 2));
            alive.Add(new Cell(1, 4));
            alive.Add(new Cell(3, 2));
            alive.Add(new Cell(0, 1));
            GameBoard board = new GameBoard(alive);
            Assert.AreEqual(0, board.Range.LowRow);
            Assert.AreEqual(1, board.Range.LowCol);
            Assert.AreEqual(3, board.Range.HighRow);
            Assert.AreEqual(4, board.Range.HighCol);
        }

        [TestMethod]
        public void CanGenerateNeighborsList()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell (1, 2));
            alive.Add(new Cell(0, 1));
            alive.Add(new Cell(1, 4));
            alive.Add(new Cell(3, 2));
            GameBoard board = new GameBoard(alive);
            List<Cell> expected = new List<Cell>();
            expected.Add(new Cell(1, 1));
            expected.Add(new Cell(1, 2));
            expected.Add(new Cell(1, 3));
            expected.Add(new Cell(2, 1));
            expected.Add(new Cell(2, 3));
            expected.Add(new Cell(3, 1));
            expected.Add(new Cell(3, 2));
            expected.Add(new Cell(3, 3));
            List<Cell> neighbors = board.GenerateNeighborsList(new Cell(2, 2));
            CollectionAssert.AreEqual(expected, neighbors);
        }

        [TestMethod]
        public void SumOfAliveNeighbors()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell ( 1, 2 ));
            alive.Add(new Cell ( 2, 2 ));
            alive.Add(new Cell ( 2, 3 ));
            alive.Add(new Cell ( 4, 3 ));
            GameBoard board = new GameBoard(alive);
            Assert.AreEqual(3, board.NeighborsAliveCount(new Cell(1, 3)));
        }

        [TestMethod]
        public void SumOfNeighborsFour()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell(0, 0));
            alive.Add(new Cell(1, 0));
            alive.Add(new Cell(2, 0));
            alive.Add(new Cell(2, 1));
            alive.Add(new Cell(1, 2));
            GameBoard board = new GameBoard(alive);
            Assert.AreEqual(3, board.NeighborsAliveCount(board.AliveCells[3]));
        }

        [TestMethod]
        public void CanApplyKillRules()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell ( 1, 2 ));
            alive.Add(new Cell ( 2, 2 ));
            alive.Add(new Cell ( 2, 3 ));
            alive.Add(new Cell ( 4, 3 ));
            GameBoard board = new GameBoard(alive);
            Cell cellToKill = new Cell(1, 2);
            board.ApplyKillRules(cellToKill, 4);
            Assert.IsTrue(cellToKill.MarkForChange);
        }

        [TestMethod]
        public void EnsureTwoCellsAreEqualInList()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell(1, 2));
            alive.Add(new Cell(1, 2));
            GameBoard board = new GameBoard(alive);
            Assert.AreEqual(board.AliveCells[0], board.AliveCells[1]);
        }

        [TestMethod]
        public void CorrectDeadCellCanComeToLife()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell(1, 2));
            alive.Add(new Cell(2, 2));
            alive.Add(new Cell(2, 3));
            alive.Add(new Cell(4, 3));
            GameBoard board = new GameBoard(alive);
            List<Cell> liveCellCandidates = new List<Cell>();
            liveCellCandidates.Add(new Cell ( 1, 3 ));
            liveCellCandidates.Add(new Cell ( 1, 3 ));
            liveCellCandidates.Add(new Cell ( 4, 1 ));
            liveCellCandidates.Add(new Cell ( 1, 3 ));
            board.CheckBringCellToLife(liveCellCandidates);
            Assert.AreEqual(5, board.AliveCells.Count);
        }

        [TestMethod]
        public void CellMarkedForChangeIsRemoved()
        {
            List<Cell> alive = new List<Cell>();
            alive.Add(new Cell(1, 2));
            alive.Add(new Cell(2, 2));
            alive.Add(new Cell(2, 3));
            alive.Add(new Cell(4, 3));
            GameBoard board = new GameBoard(alive);
            board.ApplyKillRules(board.AliveCells[0], 4);
            board.RemoveCellsThatDied();
            Assert.AreEqual(3, board.AliveCells.Count);
        }

        // Next Steps:
            // 1. Apply kill rules to living cells
            // 2. Bring cells to life according to rule of 3
                // When getting the living neighbors count, all neighbor coordinates of the living cell should be pushed to a data structure
                // When the neighbors count is done for all living cells, if any coordinate pairs appear 3 times, they are pushed to the aliveCells list

    }
}
