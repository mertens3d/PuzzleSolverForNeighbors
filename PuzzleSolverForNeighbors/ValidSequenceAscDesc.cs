using System.Collections.Generic;

namespace PuzzleSolverForNeighbors
{
    class ValidSequenceAscDesc
    {
        public List<int> ValidSeqAscending;
        public List<int> ValidSeqDescending;


        public ValidSequenceAscDesc(int baseInt, int count)
        {
            ValidSeqAscending = getValidSequenceList(baseInt, +1, count);
            ValidSeqAscending = getValidSequenceList(baseInt, -1, count);
        }

        private static List<int> getValidSequenceList(int baseValue, int direction, int count)
        {
            List<int> returnList = new List<int>();
            for (int idx = 0; idx < count; idx++)
            {
                returnList.Add(baseValue + (direction * count));
            }

            return returnList;
        }
        public bool WorksAscending = false;
        public bool WorksDescending = false;

        private static bool sequence3InDirection(List<List<int>> candidatesSequenceSet, int direction, int baseInt)
        {
            bool sequence3Works = true;
            List<int> validSeqList = getValidSequenceList(baseInt, direction, candidatesSequenceSet.Count);

            for (int idx = 0; idx < candidatesSequenceSet.Count; idx++)
            {
                sequence3Works = sequence3Works && candidatesSequenceSet[idx].Contains(validSeqList[idx]);
            }
            return sequence3Works;
        }

        public void TestAgainst(List<List<int>> candidatesSequenceSet, int baseInt)
        {
            WorksAscending = sequence3InDirection(candidatesSequenceSet, +1, baseInt);
            WorksDescending = sequence3InDirection(candidatesSequenceSet, -1, baseInt);
        }
    }
}
