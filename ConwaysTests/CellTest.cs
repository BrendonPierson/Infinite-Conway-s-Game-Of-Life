using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConwaysGameOfLife;

namespace ConwaysTests
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void CanCreateCell()
        {
            Cell cell = new Cell();
            Assert.IsNotNull(cell);
        }

        [TestMethod]
        public void CanCreateCellWithInitialValues()
        {
            Cell cell = new Cell(1, 2);
            Assert.AreEqual(1, cell.Row);
            Assert.AreEqual(2, cell.Col);
        }

        [TestMethod]
        public void CanAssertCellsAreEqual()
        {
            Cell cell1 = new Cell(1, 2);
            Cell cell2 = new Cell(1, 2);
            Assert.AreEqual(cell1, cell2);
        }
    }
}
