
namespace PuzzleSolverForNeighbors
{
    public class OneTestPrimerData
    {
        public int ColIdx;
        public int RowIdx;
        public int Value;
        public PrimeRelation PrimeRelation;
        public OneTestPrimerData(int colIdx, int rowIdx, int value = 0, PrimeRelation primeRelation = PrimeRelation.none)
        {
            ColIdx = colIdx;
            RowIdx = rowIdx;
            Value = value;
            PrimeRelation = primeRelation;
        }
    }
}
