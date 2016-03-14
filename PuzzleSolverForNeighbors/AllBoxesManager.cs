using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PuzzleSolverForNeighbors
{
    public class AllBoxesManager
    {
        #region Fields

        public readonly List<OneGridPanel> AllBoxList;
        //public readonly List<OneVerticalRelationship> AllVerticalRelationships;
        //public readonly List<OneHorizontalRelationship> AllHorizontalRelationships;

        public int MaxColAndRowIdx;

        #endregion

        #region Properties

        public Panel MasterPanel { get; set; }

        #endregion

        #region Constructors

        public AllBoxesManager(int maxColAndRowIdx)
        {
            AllBoxList = new List<OneGridPanel>();
            //AllVerticalRelationships = new List<OneVerticalRelationship>();
            //AllHorizontalRelationships = new List<OneHorizontalRelationship>();
            MaxColAndRowIdx = maxColAndRowIdx;
            populateAllNeighborHoods();

            buildAllBoxes();
        }

        #endregion

        #region Methods

        public void Analyze_A_Adjacent()
        {
            AllBoxList.ForEach(x => x.FindRightAndBelow());
            AllBoxList.ForEach(x => x.RelationshipCompare());
            redrawAll();
        }

        public void Analyze_B_CleanKnownsFromEntireColumn()
        {
            for (int colIdx = 0; colIdx < MaxColAndRowIdx; colIdx++)
            {
                cleanOutKnownValuesFromRun(runForCol(colIdx));
            }
            redrawAll();
        }

        public void Analyze_C_CleanKnownsFromEntireRows()
        {
            for (int rowIdx = 0; rowIdx < MaxColAndRowIdx; rowIdx++)
            {
                cleanOutKnownValuesFromRun(runForRow(rowIdx));
            }
            redrawAll();
        }

        public void Analyze_D_Solos()
        {
            for (int idx = 0; idx < MaxColAndRowIdx; idx++)
            {
                //col
                analyzeSoloInRun(runForRow(idx));
                //row
                analyzeSoloInRun(runForCol(idx));
            }

            redrawAll();
        }

        private readonly List<NeighborHood> allNeighborHoods;

        private void populateAllNeighborHoods()
        {
            for (int idx = 0; idx < MaxColAndRowIdx; idx++)
            {
                List<OneGridPanel> candidateRunRow = runForRow(idx);

                //horizontal
                int horIdx = 0;
                while (horIdx < MaxColAndRowIdx - Params.MinNeighborhoodCount)
                {
                    NeighborHood candidateHood = new NeighborHood(horIdx, candidateRunRow, RelationshipType.horizontal);
                    if (candidateHood.NeighborhoodCount >= Params.MinNeighborhoodCount)
                    {
                        allNeighborHoods.Add(candidateHood);
                        horIdx = horIdx + candidateHood.NeighborhoodCount;
                    }
                }

                //vertical
                int vertIdx = 0;
                while (vertIdx < MaxColAndRowIdx - Params.MinNeighborhoodCount)
                {
                    NeighborHood candidateHood = new NeighborHood(vertIdx, candidateRunRow, RelationshipType.vertical);
                    if (candidateHood.NeighborhoodCount >= Params.MinNeighborhoodCount)
                    {
                        allNeighborHoods.Add(candidateHood);
                        vertIdx = vertIdx + candidateHood.NeighborhoodCount;
                    }
                }



                //int seqLength = getIdxOfSequenceEnd(candidateRunRow, vertIdx, relationshipIsDown);
                //if (seqLength > seqLength)
                //{
                //    List<OneGridPanel> trimmedCandidteRun = candidateRunRow.Skip(vertIdx).Take(seqLength);
                //    fixAllowedValuesInSequence(trimmedCandidteRun, relationshipIsDown);
                //}

            }
        }


        public void Analyze_E_Neighborhoods()
        {

            allNeighborHoods.ForEach(x => x.AnalyzeHood());

        }

        private void fixAllowedValuesInSequence(List<OneGridPanel> candidateRun, bool relationshipIsDown)
        {
            //we are here because we have a sequence the is 3 or more
            //that means that the interior elements of the sequence must be one less than the max of neighbor
            //and one more than the min of the neighbor

       


        }



        //public void FindSolutionAll()
        //{
        //    Analyze_A_Adjacent();
        //    Analyze_E_RunsOfThreeOrMore();


        //    Analyze_C_CleanKnownsFromEntireRows();
        //    Analyze_B_CleanKnownsFromEntireColumn();
        //}


        //public void PutBoxesOnScreen(Panel form1)
        //{
        //    foreach (OneGridPanel oneGridPanel in AllBoxList)
        //    {
        //        form1.Controls.Add(oneGridPanel.ControlAssociatedGridPanel);
        //    }
        //}


        public OneGridPanel GetBoxToTheRight(OneGridPanel oneGridPanel)
        {
            return AllBoxList
                .Where(x => x.LocIdx.RowIdx == oneGridPanel.LocIdx.RowIdx)
                .FirstOrDefault(x => x.LocIdx.ColIdx == oneGridPanel.LocIdx.ColIdx + 1);
        }

        public OneGridPanel GetBoxUnder(OneGridPanel oneGridPanel)
        {
            return AllBoxList
                .Where(x => x.LocIdx.ColIdx == oneGridPanel.LocIdx.ColIdx)
                .FirstOrDefault(x => x.LocIdx.RowIdx == oneGridPanel.LocIdx.RowIdx + 1);
        }

        public void InitGameBoard()
        {
            initCurrentAllowedValues();
        }

        public void PopulateTestData(List<OneTestPrimerData> testDataList)
        {
            AllBoxList.ForEach(x => x.SetToDefault());
            foreach (OneTestPrimerData oneTestPrimerData in testDataList)
            {
                OneGridPanel oneGridPanel = AllBoxList
                    .Where(x => x.LocIdx.ColIdx == oneTestPrimerData.ColIdx)
                    .FirstOrDefault(x => x.LocIdx.RowIdx == oneTestPrimerData.RowIdx);

                if (oneGridPanel != null)
                {
                    if (oneTestPrimerData.Value != 0)
                    {
                        oneGridPanel.AllowedValues.Clear();
                        oneGridPanel.AllowedValues.Add(oneTestPrimerData.Value);
                        oneGridPanel.ControlAssociatedTextBox.Text = oneTestPrimerData.Value.ToString();
                    }

                    if (oneTestPrimerData.PrimeRelation == PrimeRelation.right)
                    {
                        oneGridPanel.RelationshipToRight.IsNeighbor = true;
                    }

                    if (oneTestPrimerData.PrimeRelation == PrimeRelation.bottom)
                    {
                        oneGridPanel.RelationshipToUnder.IsNeighbor = true;
                    }
                }
            }
            InitGameBoard();
            redrawAll();
        }

        private void analyzeSoloInRun(List<OneGridPanel> targetList)
        {
            //count up all of the numbers that appear only once
            //  List<int> solos = new List<int>();
            for (int needle = 0; needle < MaxColAndRowIdx; needle++)
            {

                List<OneGridPanel> foundInList = targetList
                    .Where(x => x.AllowedValues.Contains(needle))
                    .ToList();


                if (foundInList.Count() == 1)
                {
                    // solos.Add(needle);
                    foundInList.First().KnownValue = needle;
                }
            }
        }

        private void buildAllBoxes()
        {
            MasterPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
                //  BackColor = Color.LightBlue
            };

            for (int colIdx = 0; colIdx < MaxColAndRowIdx; colIdx++)
            {
                for (int rowIdx = 0; rowIdx < MaxColAndRowIdx; rowIdx++)
                {
                    LocIdx locIdx = new LocIdx(colIdx, rowIdx);
                    OneGridPanel oneGridPanel = new OneGridPanel(locIdx, this, MaxColAndRowIdx);

                    AllBoxList.Add(oneGridPanel);

                    int coordX = colIdx * Params.SpacingHorizontal;
                    int coordY = rowIdx * Params.SpacingVertical;

                    Point point = new Point(coordX, coordY);

                    oneGridPanel.ControlAssociatedGridPanel.BringToFront();
                    oneGridPanel.ControlAssociatedGridPanel.Location = point;
                    MasterPanel.Controls.Add(oneGridPanel.ControlAssociatedGridPanel);


                    MasterPanel.Controls.Add(new Label()
                    {
                        Text = coordX + "," + coordY + "{" + MaxColAndRowIdx + "}",
                        Location = point,
                        Width = 20,
                        Height = 20,

                        // BackColor = Color.Pink
                    });
                }
            }
        }

        private void cleanOutKnownValuesFromRun(List<OneGridPanel> runList)
        {
            List<int> knownValuesInThisRun = runList
                .Where(x => x.ValueIsKnown)
                .Select(x => x.KnownValue)
                .ToList();

            runList
                .Where(x => !x.ValueIsKnown)
                .ToList()
                .ForEach(x => x.RemoveValues(knownValuesInThisRun));
        }

        private void initCurrentAllowedValues()
        {
            AllBoxList.ForEach(x => x.InitAllowedValues());
        }

        private void redrawAll()
        {
            AllBoxList.ForEach(x => x.RefreshEntireBox());
        }

        private List<OneGridPanel> runForCol(int idx)
        {
            return AllBoxList.Where(x => x.LocIdx.ColIdx == idx).ToList();
        }

        private List<OneGridPanel> runForRow(int idx)
        {
            return AllBoxList.Where(x => x.LocIdx.RowIdx == idx).ToList();
        }

        #endregion
    }
}