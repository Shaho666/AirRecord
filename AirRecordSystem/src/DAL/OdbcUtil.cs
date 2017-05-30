using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

using System.Data;
using System.Data.OleDb;

using ADOX;

namespace AirRecordSystem.src.DAL
{
    public class OdbcUtil
    {
        private static String formatString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}";
        public static bool CreateDatabase(String fileName)
        {
            try
            {

                String strConnectionString = String.Format(formatString, fileName);
                Catalog catalog = new Catalog();
                catalog.Create(strConnectionString);

                catalog = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CreateTable(String fileName, String tableName, String[] fieldNames, String[] fieldTypes)
        {

            OleDbConnection conn = Connecting(fileName);

            try
            {
                StringBuilder strbuf = new StringBuilder();
                strbuf.Append("Create Table [");
                strbuf.Append(tableName);
                strbuf.Append("](");

                int n = fieldNames.GetLength(0);

                for (int i = 0; i < n - 1; i++)
                {
                    strbuf.Append(fieldNames[i] + " ");
                    strbuf.Append(fieldTypes[i] + ",");
                }

                strbuf.Append(fieldNames[n - 1] + " ");
                strbuf.Append(fieldTypes[n - 1] + ")");

                OleDbCommand dbc = new OleDbCommand(strbuf.ToString(), conn);

                dbc.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool CreateTable(String fileName, String tableName, Type type)
        {

            try
            {

                PropertyInfo[] property = type.GetProperties();

                String[] fieldNames = new String[property.Length];
                String[] fieldTypes = new String[property.Length];

                for (int i = 0; i < property.Length; i++)
                {
                    fieldNames[i] = property[i].Name;

                    if (!property[i].PropertyType.Name.Equals("String"))
                        fieldTypes[i] = "int";

                    else fieldTypes[i] = "String";
                }

                CreateTable(fileName, tableName, fieldNames, fieldTypes);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static OleDbConnection Connecting(String fileName)
        {

            OleDbConnection conn = null;

            try
            {
                conn = new OleDbConnection(String.Format(formatString, fileName));
                conn.Open();
            }
            catch (Exception e)
            {
                return null;
            }

            return conn;

        }

        public static bool CloseConnection(ref OleDbConnection conn)
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Dispose();
                conn = null;
            }

            return true;
        }

    }
}
