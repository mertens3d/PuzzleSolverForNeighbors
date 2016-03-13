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

            buildAllBoxes();
        }

        #endregion

        #region Methods





        public void FindSolutionInit()
        {
            initCurrentAllowedValues();

        }



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
                .Where(x => x.RowIdx == oneGridPanel.RowIdx)
                .FirstOrDefault(x => x.ColIdx == oneGridPanel.ColIdx + 1);
        }

        public OneGridPanel GetBoxUnder(OneGridPanel oneGridPanel)
        {
            return AllBoxList
                .Where(x => x.ColIdx == oneGridPanel.ColIdx)
                .FirstOrDefault(x => x.RowIdx == oneGridPanel.RowIdx + 1);
        }

        public void PopulateTestData(List<OneTestPrimerData> testDataList)
        {
            FindSolutionInit();
            AllBoxList.ForEach(x => x.SetToDefault());
            foreach (OneTestPrimerData oneTestPrimerData in testDataList)
            {
                OneGridPanel oneGridPanel = AllBoxList
                    .Where(x => x.ColIdx == oneTestPrimerData.ColIdx)
                    .FirstOrDefault(x => x.RowIdx == oneTestPrimerData.RowIdx);

                if (oneGridPanel != null)
                {
                    if (oneTestPrimerData.Value != 0)
                    {
                        oneGridPanel.AllowedValues.Clear();
                        oneGridPanel.AllowedValues.Add(oneTestPrimerData.Value);
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
            redrawAll();
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
                    OneGridPanel oneGridPanel = new OneGridPanel(rowIdx, colIdx, this, MaxColAndRowIdx);

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

        public void BAnalyzeRunsOfThreeOrMore()
        {
            //we want to look for groups of three that are neigbors
            //if found, then the middle one must be
        }


        public void AAnalyzeAdjacent()
        {
            AllBoxList.ForEach(x => x.FindRightAndBelow());
            AllBoxList.ForEach(x => x.RelationshipCompare());
        }

        private void initCurrentAllowedValues()
        {
            AllBoxList.ForEach(x => x.InitAllowedValues());
        }

        private void redrawAll()
        {
            AllBoxList.ForEach(x => x.RefreshEntireBox());
        }
        public void MAnalyzeEntireRows()
        {
            for (int rowIdx = 0; rowIdx < MaxColAndRowIdx; rowIdx++)
            {
                List<int> knownValuesInThisRow = AllBoxList
                    .Where(x => x.RowIdx == rowIdx)
                    .Where(x => x.ValueIsKnown)
                    .Select(x => x.KnownValue)
                    .ToList();

                AllBoxList
                    .Where(x => x.RowIdx == rowIdx)
                    .ToList()
                    .ForEach(x => x.RemoveValues(knownValuesInThisRow));
            }
        }
        public void NAnalyzeEntireColumns()
        {
            for (int colIdx = 0; colIdx < MaxColAndRowIdx; colIdx++)
            {
                List<int> knownValuesInThisColumn = AllBoxList
                    .Where(x => x.ColIdx == colIdx)
                    .Where(x => x.ValueIsKnown)
                    .Select(x => x.KnownValue)
                    .ToList();

                AllBoxList
                    .Where(x => x.ColIdx == colIdx)
                    .ToList()
                    .ForEach(x => x.RemoveValues(knownValuesInThisColumn));
            }
        }
        #endregion

        public void FindSolutionAll()
        {
            AAnalyzeAdjacent();
            BAnalyzeRunsOfThreeOrMore();


            MAnalyzeEntireRows();
            NAnalyzeEntireColumns();
        }
    }
}