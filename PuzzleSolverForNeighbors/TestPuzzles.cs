using System.Collections.Generic;

namespace PuzzleSolverForNeighbors
{
    internal class TestPuzzles
    {
        #region Fields

        public static List<OneTestPrimerData> TestDataA = new List<OneTestPrimerData>()
        {
            new OneTestPrimerData(0, 2, 6),
            new OneTestPrimerData(0, 5, 5),
            new OneTestPrimerData(0, 2, 0, PrimeRelation.bottom),
            new OneTestPrimerData(0, 3, 0, PrimeRelation.right),
            new OneTestPrimerData(0, 3, 0, PrimeRelation.bottom),
            new OneTestPrimerData(1, 0, 0, PrimeRelation.right),
            new OneTestPrimerData(1, 2, 0, PrimeRelation.bottom),
            new OneTestPrimerData(1, 4, 0, PrimeRelation.bottom),
            new OneTestPrimerData(1, 5, 0, PrimeRelation.bottom),
            new OneTestPrimerData(2, 0, 0, PrimeRelation.right),
            new OneTestPrimerData(2, 3, 0, PrimeRelation.right),
            new OneTestPrimerData(2, 3, 0, PrimeRelation.bottom),
            new OneTestPrimerData(2, 4, 0, PrimeRelation.bottom),
            new OneTestPrimerData(3, 0, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 1, 0, PrimeRelation.bottom),
            new OneTestPrimerData(3, 2, 0, PrimeRelation.bottom),
            new OneTestPrimerData(3, 2, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 3, 0, PrimeRelation.right),
            new OneTestPrimerData(4, 0, 0, PrimeRelation.right),
            new OneTestPrimerData(4, 3, 0, PrimeRelation.bottom),
            new OneTestPrimerData(4, 4, 0, PrimeRelation.bottom),
            new OneTestPrimerData(5, 1, 0, PrimeRelation.right),
        };

        #endregion


    }
}