using System;
using System.Windows.Forms;

namespace PuzzleSolverForNeighbors
{
    public partial class Form1 : Form
    {

        public AllBoxesManager AllBoxesMan;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buildBoxes();
        }

        private void buildBoxes()
        {
            int gridWidth = (int)numericUpDown1.Value;

            AllBoxesMan = new AllBoxesManager(gridWidth);
            GamePanelTarget.Controls.Add(AllBoxesMan.MasterPanel);
            // AllBoxesMan.PutBoxesOnScreen(this.GamePanelTarget);

        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            buildBoxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllBoxesMan.FindSolutionInit();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            AllBoxesMan.AAnalyzeAdjacent();
            //   this.Invalidate();
            //   this.Update();
            // this.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            AllBoxesMan.NAnalyzeEntireColumns();
        }

        private void testDataA(object sender, EventArgs e)
        {
            AllBoxesMan.PopulateTestData(TestPuzzles.TestDataA);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AllBoxesMan.AAnalyzeAdjacent();

            AllBoxesMan.BAnalyzeRunsOfThreeOrMore();
            AllBoxesMan.MAnalyzeEntireRows();
            AllBoxesMan.NAnalyzeEntireColumns();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllBoxesMan.MAnalyzeEntireRows();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AllBoxesMan.BAnalyzeRunsOfThreeOrMore();
        }
    }
}
