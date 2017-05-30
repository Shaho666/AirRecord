using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirRecordSystem.src.UI
{
    public partial class CurveControl : UserControl
    {

        private DrawFactory df = new DrawFactory();

        public CurveControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            df.InitLinesValue(50, 30, 0, 0);
            df.InitCoordinatesValue("", "", 5, 1, 1);
            df.InitPen(new Pen(Color.Gray), new Pen(Color.Blue),
                new SolidBrush(Color.Gray), new Pen(Color.Gray), new Pen(Color.Red));
            df.InitNumOfLines(0, 0, 1077, 522);
        }

        private void CurveControl_Load(object sender, EventArgs e)
        {

        }

        public void Load_Figures(List<String> arg0, List<int> arg1)
        {
            df.FillDataArrays(arg0, arg1);
            df.numHorLine = df.numVerLine = arg0.Count;
        }

        private void CurveControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            df.DrawLines(g);
            df.DrawCoordinatesValues(g, 3, 1);
            df.DrawCoordinateLine(g);
            df.DrawTitle(g);
            df.DrawEveryPoint(g, true, 3);
            df.DrawLinkPoint(g);
        }

        private void CurveControl_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height == 0 || this.Width == 0)
                return;

            this.Refresh();
        }

    }
}
