using System.Windows.Forms;

namespace MineSweeper
{
    class RafButton : Button
    {
        public byte I { get; set; }
        public byte J { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlag { get; set; }
        public bool IsQuestion { get; set; }
        public bool IsVisited { get; set; } 
        public bool LeftClickDisabled { get; set; }
        public bool IsClicked { get; set; }
    }
}