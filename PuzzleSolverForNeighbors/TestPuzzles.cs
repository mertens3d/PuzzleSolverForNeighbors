using System.Collections.Generic;

namespace PuzzleSolverForNeighbors
{
    internal class TestPuzzles
    {
        public static List<OneTestPrimerData> TestDataA = new List<OneTestPrimerData>()
        {
            new OneTestPrimerData(1, 0, 6),
            new OneTestPrimerData(5, 0, 5),
            new OneTestPrimerData(2, 0, 0, PrimeRelation.bottom),
            new OneTestPrimerData(3, 0, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 0, 0, PrimeRelation.bottom),
            new OneTestPrimerData(0, 1, 0, PrimeRelation.right),
            new OneTestPrimerData(2, 1, 0, PrimeRelation.bottom),
            new OneTestPrimerData(4, 1, 0, PrimeRelation.bottom),
            new OneTestPrimerData(5, 1, 0, PrimeRelation.bottom),
            new OneTestPrimerData(0, 2, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 2, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 2, 0, PrimeRelation.bottom),
            new OneTestPrimerData(4, 2, 0, PrimeRelation.bottom),
            new OneTestPrimerData(0, 3, 0, PrimeRelation.right),
            new OneTestPrimerData(1, 3, 0, PrimeRelation.bottom),
            new OneTestPrimerData(2, 3, 0, PrimeRelation.bottom),
            new OneTestPrimerData(2, 3, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 3, 0, PrimeRelation.right),
            new OneTestPrimerData(0, 4, 0, PrimeRelation.right),
            new OneTestPrimerData(3, 4, 0, PrimeRelation.bottom),
            new OneTestPrimerData(4, 4, 0, PrimeRelation.bottom),
            new OneTestPrimerData(1, 5, 0, PrimeRelation.right),
        };
    }
}