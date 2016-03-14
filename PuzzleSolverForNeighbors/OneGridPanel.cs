using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PuzzleSolverForNeighbors
{
    public partial class OneGridPanel
    {
        #region Fields

        public List<int> AllowedValues = new List<int>();
        public OneGridPanel GridPanelRight { get; private set; }
        public OneGridPanel GridPanelUnder { get; private set; }

        #endregion

        #region Properties

        public AllBoxesManager PtrToAllBoxesManager { get; private set; }
        public NumericUpDown ControlAssociatedTextBox { get; set; }
        private TextBox controlElligibleValuesLabel { get; set; }
        public Panel ControlAssociatedGridPanel { get; set; }
        public LocIdx LocIdx { get; set; }
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
            set
            {
                AllowedValues.Clear();
                AllowedValues.Add(value);
                ControlAssociatedTextBox.Text = value.ToString();

            }
        }



        #endregion

        #region Constructors

        public OneGridPanel(LocIdx locIdx, AllBoxesManager ptrToAllBoxesManager, int maxColRowIdx)
        {
            this.LocIdx = locIdx;
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
                .Where(x => GridPanelUnder != null)
                .Where(x => !GridPanelUnder.HasNeighborValueFor(x))
                .ToList();

            if (numsToRemove.Any())
            {
                numsToRemove.ForEach(removeSingleValue);
            }

            if (GridPanelUnder != null)
            {
                numsToRemove = GridPanelUnder.AllowedValues
                .Where(x => !HasNeighborValueFor(x))
                .ToList();

                if (numsToRemove.Any())
                {
                    numsToRemove.ForEach(GridPanelUnder.removeSingleValue);
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
            GridPanelUnder = PtrToAllBoxesManager.GetBoxUnder(this);
            GridPanelRight = PtrToAllBoxesManager.GetBoxToTheRight(this);
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


            if (!string.IsNullOrEmpty(ControlAssociatedTextBox.Text))
            {
                AllowedValues.Add(Convert.ToInt32(ControlAssociatedTextBox.Text));
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
            UpdateTextBoxForKnownValues();

            UpdateAllowedValuesLabel();

            RelationshipToRight.UpdateLabel();
            RelationshipToUnder.UpdateLabel();


            ControlAssociatedTextBox.Refresh();
            ControlAssociatedTextBox.Update();


            controlElligibleValuesLabel.Invalidate();
            controlElligibleValuesLabel.Update();
            controlElligibleValuesLabel.Refresh();


        }

        public void SetToDefault()
        {
            setAllowedValuesToAll();
            RelationshipToUnder.IsNeighbor = false;
            RelationshipToRight.IsNeighbor = false;

            ControlAssociatedTextBox.Text = string.Empty;

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


            // RefreshEntireBox();
        }

        private void downIsNotNeighbor()
        {
            if (GridPanelUnder != null && GridPanelUnder.AllowedValues.Count == 1 && AllowedValues.Count != 1)
            {
                RemoveNonNeighborValue(GridPanelUnder.AllowedValues.First());
                UpdateAllowedValuesLabel();
            }


            if (GridPanelUnder != null && AllowedValues.Count == 1 && GridPanelUnder.AllowedValues.Count != 1)
            {
                GridPanelUnder.RemoveNonNeighborValue(AllowedValues.First());
                GridPanelUnder.UpdateAllowedValuesLabel();
            }
        }

        private void eventTextChanged(object sender, EventArgs e)
        {
            if (ControlAssociatedTextBox.Text == 0.ToString())
            {
                ControlAssociatedTextBox.Text = string.Empty;
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
                if (GridPanelRight != null && GridPanelRight.ValueIsKnown && !ValueIsKnown)
                {
                    RemoveNonNeighborValue(GridPanelRight.AllowedValues.First());
                    UpdateAllowedValuesLabel();
                }


                if (ValueIsKnown && GridPanelRight != null && !GridPanelRight.ValueIsKnown)
                {
                    GridPanelRight.RemoveNonNeighborValue(AllowedValues.First());
                    GridPanelRight.UpdateAllowedValuesLabel();
                }
            }
            else
            {
                RightIsYesNeighbor();
            }
        }

        public void UpdateTextBoxForKnownValues()
        {
            if (ValueIsKnown)
            {
                ControlAssociatedTextBox.Text = KnownValue.ToString();

            }
        }


        private void removeSingleValue(int numToRemove)
        {
            if (!ValueIsKnown)
            {
                AllowedValues.Remove(numToRemove);



            }
        }

        #endregion
    }
}