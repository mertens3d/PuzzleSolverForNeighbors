using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolverForNeighbors
{
    class NeighborHood
    {
        public int IdxOfLastNeighbor;
        public int NeighborhoodCount;
        public int IdxOfFirstNeighbor;
        public OneGridPanel BaseGridPanel { get { return owningRun[IdxOfFirstNeighbor]; } }
        private readonly List<OneGridPanel> owningRun;
        public RelationshipType RelationshipType;
        public NeighborHood(int idxOfFirstNeighbor, List<OneGridPanel> candidateRun,
            RelationshipType relationshipType)
        {
            this.IdxOfFirstNeighbor = idxOfFirstNeighbor;
            IdxOfLastNeighbor = GetIdxOfLastNeighbor();
            NeighborhoodCount = IdxOfLastNeighbor - IdxOfFirstNeighbor + 1;
            this.owningRun = candidateRun;
            this.RelationshipType = relationshipType;
        }


        public int GetIdxOfLastNeighbor()
        {
            int maxSeqIdx = IdxOfFirstNeighbor;
            int currCandidateSeqIdx = IdxOfFirstNeighbor + 1;

            while ((currCandidateSeqIdx <= owningRun.Count)
                   &&
                   RelationshipType == RelationshipType.vertical
                ? owningRun[currCandidateSeqIdx].RelationshipToUnder.IsNeighbor
                : owningRun[currCandidateSeqIdx].RelationshipToRight.IsNeighbor)
            {
                currCandidateSeqIdx++;
                maxSeqIdx = currCandidateSeqIdx;

            }

            return maxSeqIdx;

        }

        public void AnalyzeHood()
        {
            //here we know we have a valid neighborhood of 3 or more
            //we need to flush out the illegal values that don't form a valid sequence
            List<ValidSequenceAscDesc> allValidSequences =
                BaseGridPanel.AllowedValues
                .Select(oneBaseVal => new ValidSequenceAscDesc(oneBaseVal, NeighborhoodCount))
                .ToList();

           List< List<int>> allowedValsThatMesh = new List<List<int>>();

            //now we need to flush out any values that don't mesh
            for (int idx = IdxOfFirstNeighbor; idx <= IdxOfLastNeighbor; idx++)
            {
                OneGridPanel oneGridPanel = owningRun[idx];
                List<int> valuesToRemove = new List<int>();
                foreach (int allowedValue in oneGridPanel.AllowedValues)
                {
                    if (! allValidSequences)
                }
                oneGridPanel.AllowedValues
            }

        }
    }
}
