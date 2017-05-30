using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AirRecordSystem.src.BLL;

namespace AirRecordSystem.src.UI
{
    public partial class FormAction : Form
    {

        private CheckDataBLO cbd = new CheckDataBLO("res/AirRecord.accdb");
        private DataTable allRecords;
        private DataTable byCityAndStation;

        public FormAction()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (cbd.CheckDatabase())
            {
                dataGridView1.DataSource = (allRecords = cbd.GetAllRecords());
                toolStripStatusLabel1.Text = "数据加载完毕，共" + dataGridView1.RowCount + "条记录";
            }
            else
            {
                toolStripStatusLabel1.Text = "请选择要导入的excel文件";
            }

            foreach (String aqi in aqis)
            {
                this.toolStripComboBox4.Items.Add(aqi);
            }

            SetGridWidth();
        }

        private void SetGridWidth()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void chooseExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            String excelName = null;
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                excelName = ofd.FileName;
                cbd.SetParser(excelName, "Sheet1", "tableName");

                if (!cbd.HasData())
                {
                    int numOfRows = cbd.numOfRowsOfExcel();
                    int step = 100;
                    int nSteps = numOfRows / step;

                    this.dataGridView1.Enabled = false;
                    for (int i = 0; i < nSteps; i++)
                    {
                        progressBar1.Value += step * 100 / numOfRows;
                        cbd.InsertData(i * step, i * step + 99);
                    }

                    progressBar1.Value = 100;
                    cbd.InsertData(nSteps * 100, numOfRows);
                    
                }
            }

            dataGridView1.DataSource = cbd.GetAllRecords();
            SetGridWidth();
            toolStripStatusLabel1.Text = "数据加载完毕，共" + dataGridView1.RowCount + "条记录";
            this.Controls.Remove(progressBar1);
            this.dataGridView1.Enabled = true;
        }

        private void otherFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void closeCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            toolStripStatusLabel1.Text = "就绪";
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Clear();
            byCityAndStation = null;

            List<String> cityNames = cbd.GetOneFieldData("City");

            foreach (String str in cityNames)
            {
                toolStripComboBox1.Items.Add(str);
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox5_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            toolStripComboBox2.Items.Clear();
            byCityAndStation = null;

            List<String> stationNames = null;

            if (toolStripComboBox1.SelectedItem == null)
            {
                stationNames = cbd.GetOneFieldData("Station");

                foreach (String str in stationNames)
                {
                    toolStripComboBox2.Items.Add(str);
                }
            }
            else
            {
                String cityName = toolStripComboBox1.SelectedItem.ToString();

                stationNames = cbd.GetStationWithCity(cityName);

                foreach (String str in stationNames)
                {
                    toolStripComboBox2.Items.Add(str);
                }
            }

        }

        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(userControl);
            this.Controls.Add(dataGridView1);

            dataGridView1.DataSource = allRecords;
            SetGridWidth();
            toolStripStatusLabel1.Text = "数据加载完毕，共" + dataGridView1.RowCount + "条记录";
        }

        private void viewByCityStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(userControl);
            this.Controls.Add(dataGridView1);

            SetGridWidth();

            if (toolStripComboBox1.SelectedItem == null)
            {
                MessageBox.Show("城市名称不能为空");
                return;
            }

            if (toolStripComboBox2.SelectedItem == null)
            {
                MessageBox.Show("站点名称不能为空");
                return;
            }

            String cityName = toolStripComboBox1.SelectedItem.ToString();
            String stationName = toolStripComboBox2.SelectedItem.ToString();

            byCityAndStation = cbd.GetByCityAndStation(cityName, stationName);
            dataGridView1.DataSource = byCityAndStation;
            toolStripStatusLabel1.Text = "数据加载完毕，共" + dataGridView1.RowCount + "条记录";
        }

        private void drawByArgsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(userControl);

            if (toolStripComboBox4.SelectedItem == null)
            {
                MessageBox.Show("请选择绘图参数");
                return;
            }

            String aqiFieldName = toolStripComboBox4.SelectedItem.ToString();

            List<String> timeDates = new List<String>();
            List<int> aqiValues = new List<int>();

            if (byCityAndStation == null)
            {
                MessageBox.Show("请确认按城市和站点名称查询的结果");
                return;
            }

            for (int i = 0; i < byCityAndStation.Rows.Count; i++)
            {
                String date = byCityAndStation.Rows[i]["TimeDate"].ToString();

                String[] tokens = date.Split(new char[3] { '/', ' ', ':' });
                StringBuilder buf = new StringBuilder();

                buf.Append(Convert.ToInt32(tokens[0]) % 2000);
                buf.Append("-");
                buf.Append(tokens[1]);
                buf.Append("-");
                buf.Append(tokens[2]);
                buf.Append(" ");
                buf.Append(tokens[3]);

                timeDates.Add(buf.ToString());
                aqiValues.Add(Convert.ToInt32(byCityAndStation.Rows[i][aqiFieldName].ToString()));
            }

            userControl.Load_Figures(timeDates, aqiValues);

            this.Controls.Remove(dataGridView1);
            this.Controls.Add(userControl);

        }

        private void toolStripComboBox4_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

    }
}
