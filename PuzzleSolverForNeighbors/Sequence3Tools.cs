
using System.Collections.Generic;

namespace PuzzleSolverForNeighbors
{
    static class Sequence3Tools
    {

       
       
        public static bool VerifyOneListOfLists(ref List<List<int>> candidatesSequenceSet)
        {
            List<List<int>> validNumbers = new List<List<int>>();


            foreach (int baseInt in candidatesSequenceSet[0])
            {
                ValidSequenceAscDesc validSeqAscDesc = new ValidSequenceAscDesc(baseInt, candidatesSequenceSet.Count);

                validSeqAscDesc.TestAgainst(candidatesSequenceSet, baseInt);

                if (validSeqAscDesc.WorksAscending)
                {
                    addNumbersToList(ref validNumbers, validSeqAscDesc.ValidSeqAscending);
                }
            }

            return validNumbers;
        }

        private static void addNumbersToList(ref List<List<int>> validNumbers, List<int> validSeq)
        {
            for (int idx = 0; idx < validNumbers.Count; idx++)
            {
                if (!validNumbers[idx].Contains(validSeq[idx]))
                {
                    validNumbers[idx].Add(validSeq[idx]);
                }
            }
        }
    }
}
