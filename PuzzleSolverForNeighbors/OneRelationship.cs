using System;
using System.Drawing;
using Label = System.Windows.Forms.Label;

namespace PuzzleSolverForNeighbors
{
    public abstract class OneRelationShip
    {

        public bool IsNeighbor;
        public Label ControlAssociatedRelationshipLabel;
        //public int RowIdx;
        //public int ColIdx;

        //public int CoordX
        //{
        //    get
        //    {
        //        if (relationshipType == RelationshipType.vertical)
        //        {
        //            return (ColIdx * Params.SpacingHorizontal);
        //        }
        //        else
        //        {
        //            return (ColIdx * Params.SpacingHorizontal) + Params.BoxWidth;
        //        }



        //    }
        //}
        //public int CoordY
        //{
        //    get
        //    {
        //        if (relationshipType == RelationshipType.vertical)
        //        {
        //            return (RowIdx * Params.SpacingVertical) + Params.BoxHeight + Params.EligibleValsHeight;
        //        }
        //        else
        //        {
        //            return (RowIdx * Params.SpacingVertical);
        //        }
        //    }
        //}


        private void setLabelText()
        {
            ControlAssociatedRelationshipLabel.Text = IsNeighbor ? "+" : string.Empty;
            ControlAssociatedRelationshipLabel.BackColor = IsNeighbor ? Color.LightGreen : Color.Transparent;
        }
        public void UpdateLabel()
        {
            setLabelText();
        }


        private readonly RelationshipType relationshipType;

        protected OneRelationShip(RelationshipType relationshipType)
        {

            this.relationshipType = relationshipType;
            ControlAssociatedRelationshipLabel = new System.Windows.Forms.Label
            {

                Text = " ",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
                ,
                // BackColor = Color.GreenYellow,
                Height = calcHeight(),
                Width = calcWidth(),
                // BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = calcLocation()


            };



            ControlAssociatedRelationshipLabel.Click += toggleRelationship;

        }

        private int calcWidth()
        {
            if (relationshipType == RelationshipType.vertical)
            {
                return Params.RelationshipLabelWidthWhenVertical;
            }
            else
            {
                return Params.RelationshipLabelWidthWhenHorizontal;
            }
        }

        private int calcHeight()
        {
            if (relationshipType == RelationshipType.vertical)
            {
                return Params.RelationshipLabelHeightWhenVertical;
            }
            else
            {
                return Params.RelationshipLabelHeightWhenHorizontal;
            }
        }

        private Point calcLocation()
        {
            if (relationshipType == RelationshipType.vertical)
            {
                return new Point(0, Params.BoxHeight + Params.EligibleValsHeight);
            }
            else
            {
                return new Point(Params.BoxWidth, 0);
            }
        }

        private void toggleRelationship(object sender, EventArgs e)
        {
            IsNeighbor = !IsNeighbor;
            setLabelText();
        }

        //public void PutInControl(Control control)
        //{

        //    // AssociatedLabel.Location = new Point();

        //    ControlAssociatedRelationshipLabel.SetBounds(
        //        CoordX,
        //        CoordY,
        //        relationshipType == RelationshipType.vertical ?
        //        Params.RelationshipLabelWidthWhenVertical
        //        : Params.RelationshipLabelWidthWhenHorizontal,

        //        relationshipType == RelationshipType.vertical ? Params.RelationshipLabelHeightWhenVertical : Params.RelationshipLabelHeightWhenHorizontal);


        //    control.Controls.Add(ControlAssociatedRelationshipLabel);
        //    // control.Update();
        //    // AssociatedLabel.Update();
        //}

    }
}
