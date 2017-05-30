using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using AirRecordSystem.src.BLL;

namespace AirRecordSystem.src.UI
{
    public class DrawFactory
    {
        public int beginX;
        public int beginY;
        public int intervalX;
        public int intervalY;

        public String xUnit;
        public String yUnit;
        public float beginYValue;
        public float everyYValue;
        public float everyXValue;

        private String xName = "X";
        private String yName = "Y";
        private float beginXCoordinatesValues = 0;
        private float beginYCoordinatesValues = 0;
        public String title;
        public Font titleFont;

        public Pen linesPen;
        public Pen curvePen;
        public SolidBrush coordinatesValueBrush;
        public Pen coordinatesLinesPen;
        public Pen pointPen;

        public int numVerLine;
        public int numHorLine;
        public int width;
        public int height;

        public List<float> points;
        public List<String> timeDates;
        public List<int> aqiValues;

        #region initialize all args
        public void InitLinesValue(int beginx, int beginy, int intervalx, int intervaly)
        {
            this.beginX = beginx;
            this.beginY = beginy;
            this.intervalX = intervalx;
            this.intervalY = intervaly;
        }

        public void InitCoordinatesValue(string xUnit, string yUnit, float beginYValue, float everyYValue, float everyXValue)
        {
            this.xUnit = xUnit;
            this.yUnit = yUnit;
            this.beginYValue = beginYValue;
            this.everyYValue = everyYValue;
            this.everyXValue = everyXValue;
        }

        public void InitPen(Pen linesPen, Pen curvePen, SolidBrush coordinatesValueBrush, Pen coordinatesLinesPen, Pen pointpen)
        {
            this.linesPen = linesPen;
            this.curvePen = curvePen;
            this.coordinatesValueBrush = coordinatesValueBrush;
            this.coordinatesLinesPen = coordinatesLinesPen;
            this.pointPen = pointpen;
        }

        public void InitNumOfLines(int numVerLine, int numHorLine, int width, int height)
        {
            this.numVerLine = numVerLine;
            this.numHorLine = numHorLine;
            this.width = width;
            this.height = height;
        }

        public void InitPointList(List<float> points)
        {
            this.points = points;
        }

        public void FillDataArrays(List<String> timeDates, List<int> aqiValues)
        {
            this.timeDates = timeDates;
            this.aqiValues = aqiValues;
        }

        #endregion

        #region draw coordinates
        public void DrawCoordinateLine(Graphics g)
        {
            coordinatesLinesPen.Width = 2;
            coordinatesLinesPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            coordinatesLinesPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(coordinatesLinesPen, beginX, beginY + intervalY * numHorLine, beginX, beginY - intervalY);
            g.DrawLine(coordinatesLinesPen, beginX, beginY + intervalY * numHorLine, beginX + intervalX * numVerLine + 20, beginY + intervalY * numHorLine);
        }
        #endregion

        #region draw grids
        public void DrawLines(Graphics g)
        {
            linesPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            intervalX = (width - beginX * 2) / numVerLine;
            intervalY = (height - beginY) / (numHorLine + 1);

            for (int i = 0; i <= numHorLine; i++)
                g.DrawLine(linesPen, new Point(beginX, beginY + i * intervalY), new Point(beginX + numVerLine * intervalX, beginY + i * intervalY));
            for (int i = 0; i <= numVerLine; i++)
                g.DrawLine(linesPen, new Point(beginX + i * intervalX, beginY), new Point(beginX + i * intervalX, beginY + numHorLine * intervalY));
        }
        #endregion

        #region set value in projection of x and y
        public void DrawCoordinatesValues(Graphics g, int interval_xv, int interval_yv)
        {
            StringFormat drawFormat = new StringFormat();
            Font font = new Font("Comic Sans MS", 8);
            String showValue = "";
            /**
             * horizontal
             */
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Near;

            String xele = null;

            for (int i = 1; i < numVerLine; i++)
            {
                xele = timeDates[i];
                if (i % interval_xv == 0)
                {
                    showValue = xele + " ";
                    g.DrawString(showValue, font, coordinatesValueBrush, new PointF(beginX + (i) * intervalX, beginY + numHorLine * intervalY), drawFormat);
                }

            }

            drawFormat.Alignment = StringAlignment.Near;
            drawFormat.LineAlignment = StringAlignment.Far;
            g.DrawString(xName, font, coordinatesValueBrush, new PointF(beginX + numVerLine * intervalX + 10, beginY + numHorLine * intervalY), drawFormat);

            drawFormat.Alignment = StringAlignment.Far;
            drawFormat.LineAlignment = StringAlignment.Center;

            /**
             * vertical
             */
            HeapSort sortAqi = new HeapSort(new List<int>(aqiValues));
            List<int> sorted = sortAqi.Sort();

            int max = sorted[0];
            int min = sorted[sorted.Count - 1];

            float scale = (max - min) * 1.2f;
            everyYValue = (float)Math.Round(scale / numHorLine, 1);
            beginYCoordinatesValues = min - (max - min) * 0.1f;

            for (int j = 0; j < numHorLine; j++)
            {

                if (j % interval_yv == 0)
                {
                    showValue = everyYValue * j + beginYCoordinatesValues + "";
                    g.DrawString(showValue, font, coordinatesValueBrush, new PointF(beginX, beginY + (numHorLine - j) * intervalY), drawFormat);
                }

            }
            drawFormat.Alignment = StringAlignment.Near;
            drawFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(yName, font, coordinatesValueBrush, new PointF(beginX, beginY - 1 * intervalY), drawFormat);
        }
        #endregion

        #region set title
        public void DrawTitle(Graphics g)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(title, titleFont, new SolidBrush(Color.Black), new PointF(beginX + numVerLine * intervalX / 2, beginY - intervalY), drawFormat);
        }
        #endregion

        #region draw every point
        public void DrawCircle(Graphics g, float fx, float fy, int r, Pen pointPen, bool full = false)
        {
            if (!full)
                g.DrawEllipse(pointPen, fx - r, fy - r, 2 * r, 2 * r);
            else
            {
                SolidBrush pBrush = new SolidBrush(pointPen.Color);
                g.FillEllipse(pBrush, fx - r, fy - r, 2 * r, 2 * r);
            }
        }
        public void DrawEveryPoint(Graphics g, bool full = false, int pointR = 2)
        {
            float x, y;
            Point p;
            for (int i = 0; i < timeDates.Count; i++)
            {
                p = new Point();
                x = beginX + i * intervalX;
                y = beginY + (numHorLine - (aqiValues[i] - beginYCoordinatesValues) / everyYValue) * intervalY;
                p.X = Convert.ToInt32(x);
                p.Y = Convert.ToInt32(y);
                DrawCircle(g, x, y, pointR, pointPen, full);
            }
        }
        #endregion

        #region fill lines with every two points
        public void DrawLinkPoint(Graphics g)
        {

            curvePen.Width = 2;

            for (int i = 0; i + 1 < timeDates.Count; i++)
            {
                g.DrawLine(curvePen, beginX + i * intervalX, beginY + (numHorLine - (aqiValues[i] - beginYCoordinatesValues) / everyYValue) * intervalY,
                    beginX + (i + 1) * intervalX, beginY + (numHorLine - (aqiValues[i + 1] - beginYCoordinatesValues) / everyYValue) * intervalY);
            }
        }
        #endregion

    }
}
