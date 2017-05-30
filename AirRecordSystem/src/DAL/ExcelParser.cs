using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

using ADOX;

namespace AirRecordSystem.src.DAL
{
    public class ExcelParser
    {

        private String strConn = @"Provider=Microsoft.Ace.OleDb.12.0;Data Source='{0}';
                    Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

        private String excelFileName;
        private String excelTableName;
        private String dataTableName;

        #region constructor

        public ExcelParser() { }

        public ExcelParser(String excelFileName, String excelTableName, String dataTableName)
        {
            this.excelFileName = excelFileName;
            this.excelTableName = excelTableName;
            this.dataTableName = dataTableName;
        }

        #endregion

        public int numOfRows()
        {

            String connection = String.Format(strConn, excelFileName);

            OleDbConnection conn = new OleDbConnection(connection);
            conn.Open();

            OleDbCommand odc = new OleDbCommand("SELECT * FROM [" + excelTableName + "$]", conn);
            OleDbDataReader odr = odc.ExecuteReader();

            int count = 0;

            while(odr.Read())
            {
                if (odr.HasRows)
                    count++;
            }

            return count;
        }

        public DataTable ParseExcel(int beginId, int endId)
        {

            DataTable excelDataTable = new DataTable();
            String connection = String.Format(strConn, excelFileName);

            OleDbConnection conn = new OleDbConnection(connection);
            conn.Open();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM [");
            sql.Append(excelTableName);
            sql.Append("$] WHERE Id >= ");
            sql.Append(beginId + "");
            sql.Append("AND Id <= ");
            sql.Append(endId + "");

            OleDbDataAdapter oada = new OleDbDataAdapter(sql.ToString(), connection);
            excelDataTable.TableName = dataTableName;

            oada.Fill(excelDataTable);
            conn.Close();

            return excelDataTable;

        }

    }
}
