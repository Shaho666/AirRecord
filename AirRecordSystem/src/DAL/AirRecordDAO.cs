using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

using ADOX;

namespace AirRecordSystem.src.DAL
{
    public class AirRecordDAO
    {
        private String fileName;

        private const String tableName = "AirRecord";

        public AirRecordDAO() { }

        public AirRecordDAO(String fileName)
        {
            this.fileName = fileName;
        }

        public bool InsertDatatable(DataTable table)
        {

            OleDbConnection conn = null;
            OleDbDataAdapter oada = null;
            OleDbCommandBuilder ocb = null;

            try
            {

                conn = OdbcUtil.Connecting(fileName);
                oada = new OleDbDataAdapter("SELECT * FROM [" + tableName + "]", conn);
                ocb = new OleDbCommandBuilder(oada);

                DataSet ds = new DataSet();
                oada.Fill(ds, "flag");

                DataTable dt = ds.Tables["flag"];

                String year, month, day, hour, minute, second;
                String timeDate, dateFormat;

                foreach (DataRow row in table.Rows)
                {
                    
                    DataRow dr = dt.NewRow();

                    timeDate = row[0].ToString();
                    year = timeDate.Substring(0, 4);
                    month = timeDate.Substring(4, 2);
                    day = timeDate.Substring(6, 2);
                    hour = timeDate.Substring(8, 2);
                    minute = timeDate.Substring(10, 2);
                    second = timeDate.Substring(12, 2);

                    dateFormat = String.Format("#{0}/{1}/{2} {3}:{4}:{5}#",
                            year, month, day, hour, minute, second);

                    dr[0] = Convert.ToDateTime(dateFormat);

                    for (int i = 1; i < dt.Columns.Count; i++)
                        dr[i] = row[i];

                    dt.Rows.Add(dr);
                }

                oada.Update(dt);

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                OdbcUtil.CloseConnection(ref conn);
            }

            return true;
        }

        public DataTable QueryAllToTable()
        {

            OleDbConnection conn = null;
            OleDbDataAdapter oada = null;
            DataTable dataTable = null;

            try
            {
                dataTable = new DataTable();
                conn = OdbcUtil.Connecting(fileName);
                oada = new OleDbDataAdapter("SELECT * FROM " + tableName, conn);
                oada.Fill(dataTable);
            }
            catch
            {
                return null;
            }
            finally
            {
                OdbcUtil.CloseConnection(ref conn);
            }

            return dataTable;

        }

        public List<String> QueryAllOneField(String fieldName)
        {

            OleDbConnection conn = null;
            OleDbCommand cmd = null;
            OleDbDataReader odr = null;

            List<String> fieldData = null;

            try
            {

                conn = OdbcUtil.Connecting(fileName);
                cmd = new OleDbCommand("SELECT DISTINCT(" + fieldName + ") FROM " + tableName, conn);

                odr = cmd.ExecuteReader();
                fieldData = new List<String>();

                while(odr.Read())
                {
                    if(odr.HasRows)
                    {
                        fieldData.Add(odr.GetString(0));
                    }
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                OdbcUtil.CloseConnection(ref conn);
            }

            return fieldData;

        }

        public List<String> QueryStationWithCity(String cityName)
        {

            OleDbConnection conn = null;
            OleDbCommand cmd = null;
            OleDbDataReader odr = null;

            List<String> stations = null;

            try
            {

                conn = OdbcUtil.Connecting(fileName);
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append("SELECT DISTINCT(Station) FROM ");
                sqlBuilder.Append(tableName);
                sqlBuilder.Append(" WHERE City=");
                sqlBuilder.Append("'");
                sqlBuilder.Append(cityName);
                sqlBuilder.Append("'");

                cmd = new OleDbCommand(sqlBuilder.ToString(), conn);

                odr = cmd.ExecuteReader();
                stations = new List<String>();

                while (odr.Read())
                {
                    if (odr.HasRows)
                    {
                        stations.Add(odr.GetString(0));
                    }
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                OdbcUtil.CloseConnection(ref conn);
            }

            return stations;

        }

        public DataTable QueryByCityAndStation(String cityName, String stationName)
        {

            OleDbConnection conn = null;
            OleDbDataAdapter oada = null;
            DataTable dataTable = null;

            try
            {
                dataTable = new DataTable();
                conn = OdbcUtil.Connecting(fileName);

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append("SELECT * FROM ");
                sqlBuilder.Append(tableName);
                sqlBuilder.Append(" WHERE City='");
                sqlBuilder.Append(cityName);
                sqlBuilder.Append("' AND Station='");
                sqlBuilder.Append(stationName);
                sqlBuilder.Append("'");

                oada = new OleDbDataAdapter(sqlBuilder.ToString(), conn);
                oada.Fill(dataTable);
            }
            catch
            {
                return null;
            }
            finally
            {
                OdbcUtil.CloseConnection(ref conn);
            }

            return dataTable;

        }

        public int numOfRecord()
        {

            OleDbConnection conn = null;
            OleDbCommand cmd = null;

            int num = 0;

            try
            {
                conn = OdbcUtil.Connecting(fileName);
                cmd = new OleDbCommand("SELECT * FROM " + tableName, conn);

                OleDbDataReader odr = cmd.ExecuteReader();

                while (odr.Read())
                {
                    if (odr.HasRows)
                        num++;
                }
                
            }
            catch
            {
                return -1;
            }
            finally
            {
                OdbcUtil.CloseConnection(ref conn);
            }

            return num;

        }

    }
}
