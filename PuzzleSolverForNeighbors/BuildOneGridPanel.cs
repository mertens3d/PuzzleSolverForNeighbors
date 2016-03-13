
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PuzzleSolverForNeighbors
{
    public partial class OneGridPanel
    {
        public void BuildOneGridPanelControls()
        {
            ControlAssociatedGridPanel = new Panel()
            {
                Width = Params.SpacingHorizontal,
                Height = Params.SpacingVertical,
                //   BackColor = Color.LightGray,
                // BorderStyle = BorderStyle.FixedSingle,

            };

            buildChildControls();
            addChildControlsToPanel();
        }
        private void addChildControlsToPanel()
        {
            ControlAssociatedGridPanel.Controls.Add(controlAssociatedTextBox);

            ControlAssociatedGridPanel.Controls.Add(RelationshipToUnder.ControlAssociatedRelationshipLabel);
            ControlAssociatedGridPanel.Controls.Add(RelationshipToRight.ControlAssociatedRelationshipLabel);
            ControlAssociatedGridPanel.Controls.Add(controlElligibleValuesLabel);
        }
        private void buildChildrenRelationships()
        {
            RelationshipToUnder = new OneVerticalRelationship(RelationshipType.vertical);
            RelationshipToRight = new OneHorizontalRelationship(RelationshipType.horizontal);
        }


        private void buildChildEligibleValuesLabel()
        {
            controlElligibleValuesLabel = new TextBox
            {
                Text = "?",
                // TextAlign = ContentAlignment.MiddleCenter
            };
            // ElligibleValuesLabel.BackColor = Color.Red;
            controlElligibleValuesLabel.SetBounds(0, Params.BoxHeight, Params.EligibleValsWidth, Params.EligibleValsHeight);
        }
        private void buildChildControls()
        {
            buildChildrenRelationships();
            buildChildEligibleValuesLabel();
            buildAssociatedNumeric();
        }
        private void buildAssociatedNumeric()
        {
            controlAssociatedTextBox = new NumericUpDown
            {
                Text = string.Empty,
                Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                // BackColor = Color.Green
            };
            controlAssociatedTextBox.TextChanged += eventTextChanged;


            controlAssociatedTextBox.SetBounds(0, 0, Params.BoxHeight, Params.BoxHeight);

            //  AssociatedTextBox.BackColor = Color.Transparent;
            ControlAssociatedGridPanel.Controls.Add(controlAssociatedTextBox);

            controlAssociatedTextBox.ValueChanged += BoxValueChanged;
        }

        private void BoxValueChanged(object sender, EventArgs e)
        {

        }

       
    }
}
