
namespace PuzzleSolverForNeighbors
{
    static class Params
    {
        //public static int GridTopLeftX = 0;
        //public static int GridTopLeftY = 0;

        // public static double Ratio = 0.65;

        public static int BoxDim = 40;


        public static int BoxHeight = BoxDim;
        public static int EligibleValsHeight = 20;
        public static int RelationshipLabelHeightWhenVertical = 20;
        public static int SpacingVertical = BoxHeight + EligibleValsHeight + RelationshipLabelHeightWhenVertical;


        public static int BoxWidth = BoxDim;
        public static int RelationshipLabelWidthWhenHorizontal = 20;
        public static int EligibleValsWidth = BoxWidth + RelationshipLabelWidthWhenHorizontal;
        public static int SpacingHorizontal = BoxWidth + RelationshipLabelWidthWhenHorizontal;

        // public static int TextBoxWidth = 40;


        public static int RelationshipLabelHeightWhenHorizontal = BoxHeight;
        public static int RelationshipLabelWidthWhenVertical = BoxWidth;
    }
}
