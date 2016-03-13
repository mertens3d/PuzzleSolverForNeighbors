using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PuzzleSolverForNeighbors
{
    public partial class OneGridPanel
    {
        #region Fields

        public List<int> AllowedValues;
        public int RowIdx;
        private OneGridPanel gridPanelRight;
        private OneGridPanel gridPanelUnder;


        #endregion

        #region Properties

        public AllBoxesManager PtrToAllBoxesManager { get; private set; }
        private NumericUpDown controlAssociatedTextBox { get; set; }
        private TextBox controlElligibleValuesLabel { get; set; }
        public Panel ControlAssociatedGridPanel { get; set; }
        public int ColIdx { get; set; }
        public int IdxOverall { get; set; }
        public OneHorizontalRelationship RelationshipToRight { get; set; }
        public OneVerticalRelationship RelationshipToUnder { get; set; }

        private int maxColRowIdx { get; set; }
        //     public OneVerticalRelationship RelationshipToUnder { get; set; }

        //public int CoordX
        //{
        //    get { return (ColIdx * Params.SpacingHorizontal); }
        //}

        //public int CoordY
        //{
        //    get { return (RowIdx * Params.SpacingVertical); }
        //}

        public OneGridPanel GridPanelToTheRight
        {
            get { return PtrToAllBoxesManager.GetBoxToTheRight(this); }
        }

        public bool ValueIsKnown
        {
            get { return AllowedValues.Count == 1; }
        }

        public int KnownValue
        {
            get { return AllowedValues.First(); }
        }

        #endregion

        #region Constructors

        public OneGridPanel(int colIdx, int rowIdx, AllBoxesManager ptrToAllBoxesManager, int maxColRowIdx)
        {
            ColIdx = colIdx;
            RowIdx = rowIdx;
            PtrToAllBoxesManager = ptrToAllBoxesManager;
            this.maxColRowIdx = maxColRowIdx;
            BuildOneGridPanelControls();
        }

        #endregion

        #region Methods

        public void DownIsYesNeighbor()
        {
            //if they are neighbors then any numbers that are not within +/- 1 of each other must be removed
            //and they are neighbors in this case

            List<int> numsToRemove = AllowedValues
                .Where(x => gridPanelUnder != null)
                .Where(x => !gridPanelUnder.HasNeighborValueFor(x))
                .ToList();

            if (numsToRemove.Any())
            {
                numsToRemove.ForEach(removeSingleValue);
            }

            if (gridPanelUnder != null)
            {
                numsToRemove = gridPanelUnder.AllowedValues
                .Where(x => !HasNeighborValueFor(x))
                .ToList();

                if (numsToRemove.Any())
                {
                    numsToRemove.ForEach(gridPanelUnder.removeSingleValue);
                }
            }



        }
        public void RightIsYesNeighbor()
        {
            //if they are neighbors then any numbers that are not within +/- 1 of each other must be removed
            //and they are neighbors in this case

            if (GridPanelToTheRight != null)
            {

                List<int> numsToRemove = AllowedValues
                    .Where(x => !GridPanelToTheRight.HasNeighborValueFor(x))
                    .ToList();

                if (numsToRemove.Any())
                {
                    numsToRemove.ForEach(removeSingleValue);
                }


                numsToRemove = GridPanelToTheRight.AllowedValues
                    .Where(x => !HasNeighborValueFor(x))
                    .ToList();

                if (numsToRemove.Any())
                {
                    numsToRemove.ForEach(GridPanelToTheRight.removeSingleValue);
                }
            }
        }
        public void FindRightAndBelow()
        {
            gridPanelUnder = PtrToAllBoxesManager.GetBoxUnder(this);
            gridPanelRight = PtrToAllBoxesManager.GetBoxToTheRight(this);
        }

        public bool HasNeighborValueFor(int oneAllowedValue)
        {
            bool hasPlusOne = AllowedValues.Contains(oneAllowedValue + 1);
            bool hasMinusOne = AllowedValues.Contains(oneAllowedValue - 1);
            return hasPlusOne || hasMinusOne;
        }

        public void InitAllowedValues()
        {
            AllowedValues = new List<int>();


            if (!string.IsNullOrEmpty(controlAssociatedTextBox.Text))
            {
                AllowedValues.Add(Convert.ToInt32(controlAssociatedTextBox.Text));
            }
            else
            {
                setAllowedValuesToAll();
            }
            UpdateAllowedValuesLabel();
        }

        private void setAllowedValuesToAll()
        {
            AllowedValues.Clear();
            for (int val = 1; val <= maxColRowIdx; val++)
            {
                AllowedValues.Add(val);
            }
        }
        public void RefreshEntireBox()
        {
            UpdateAllowedValuesLabel();
            RelationshipToRight.UpdateLabel();
            RelationshipToUnder.UpdateLabel();
            setDisplayOfKnownValue();
        }

        public void SetToDefault()
        {
            setAllowedValuesToAll();
            RelationshipToUnder.IsNeighbor = false;
            RelationshipToRight.IsNeighbor = false;

            controlAssociatedTextBox.Text = string.Empty;
            RefreshEntireBox();

        }
        public void RelationshipCompare()
        {
            relationshipCompareDown();
            relationshipCompareToRight();
        }

        //private OneBox _relationshipUnder;
        //public OneBox RelationshipUnder
        //{
        //    get { return _boxUnder ?? (_boxUnder = allBoxesManager.GetBoxUnder(this)); }
        //}

        public void RemoveNonNeighborValue(int intoToRemove)
        {
            removeSingleValue(intoToRemove);
            removeSingleValue(intoToRemove + 1);
            removeSingleValue(intoToRemove - 1);
        }

        public void RemoveValues(List<int> knownValuesInThisColumn)
        {
            knownValuesInThisColumn
                .ForEach(removeSingleValue);
        }

        public void UpdateAllowedValuesLabel()
        {
            string text = string.Empty;
            AllowedValues.ForEach(x => text += x);
            controlElligibleValuesLabel.Text = text;

            controlElligibleValuesLabel.Invalidate();
            controlElligibleValuesLabel.Update();
            controlElligibleValuesLabel.Refresh();
        }

        private void downIsNotNeighbor()
        {
            if (gridPanelUnder != null && gridPanelUnder.AllowedValues.Count == 1 && AllowedValues.Count != 1)
            {
                RemoveNonNeighborValue(gridPanelUnder.AllowedValues.First());
                UpdateAllowedValuesLabel();
            }


            if (gridPanelUnder != null && AllowedValues.Count == 1 && gridPanelUnder.AllowedValues.Count != 1)
            {
                gridPanelUnder.RemoveNonNeighborValue(AllowedValues.First());
                gridPanelUnder.UpdateAllowedValuesLabel();
            }
        }

        private void eventTextChanged(object sender, EventArgs e)
        {
            if (controlAssociatedTextBox.Text == 0.ToString())
            {
                controlAssociatedTextBox.Text = string.Empty;
            }
        }

        private void relationshipCompareDown()
        {
            //if they are neighbors then this one cannot have eligible values that are less then or greater than this value

            if (!RelationshipToUnder.IsNeighbor)
            {
                downIsNotNeighbor();
            }
            else
            {
                DownIsYesNeighbor();
            }
        }

        private void relationshipCompareToRight()
        {
            if (!RelationshipToRight.IsNeighbor)
            {
                if (gridPanelRight != null && gridPanelRight.ValueIsKnown && !ValueIsKnown)
                {
                    RemoveNonNeighborValue(gridPanelRight.AllowedValues.First());
                    UpdateAllowedValuesLabel();
                }


                if (ValueIsKnown && gridPanelRight != null && !gridPanelRight.ValueIsKnown)
                {
                    gridPanelRight.RemoveNonNeighborValue(AllowedValues.First());
                    gridPanelRight.UpdateAllowedValuesLabel();
                }
            }
            else
            {
                RightIsYesNeighbor();
            }
        }

        private void setDisplayOfKnownValue()
        {
            if (ValueIsKnown)
            {
                controlAssociatedTextBox.Text = KnownValue.ToString();
                controlAssociatedTextBox.Refresh();
                controlAssociatedTextBox.Update();
            }
        }
        private void removeSingleValue(int numToRemove)
        {
            if (!ValueIsKnown)
            {
                AllowedValues.Remove(numToRemove);
                UpdateAllowedValuesLabel();
                setDisplayOfKnownValue();

            }
        }

        #endregion
    }
}