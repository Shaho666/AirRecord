using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Data;

using AirRecordSystem.src.DAL;
using AirRecordSystem.src.Model;

namespace AirRecordSystem.src.BLL
{
    public class CheckDataBLO
    {

        private String dataPath;
        private AirRecordDAO dao;

        private ExcelParser parser;

        public CheckDataBLO() { }

        public CheckDataBLO(String dataPath)
        {
            this.dataPath = dataPath;
            dao = new AirRecordDAO(dataPath);
        }

        public bool CheckDatabase()
        {
            if(!File.Exists(dataPath))
            {
                Directory.CreateDirectory("res/");
                OdbcUtil.CreateDatabase(dataPath);
                OdbcUtil.CreateTable(dataPath, "AirRecord", typeof(AirRecord));
                return false;
            }

            return true;
        }

        public bool HasData()
        {
            return dao.numOfRecord() != 0;
        }

        public void SetParser(String excelName, String excelTableName, String dataTableName)
        {
            parser = new ExcelParser(excelName, excelTableName, dataTableName);
        }

        public bool InsertData(int begin, int end)
        {
            return dao.InsertDatatable(parser.ParseExcel(begin, end));
        }

        public int numOfRowsOfExcel()
        {
            return parser.numOfRows();
        }

        public DataTable GetAllRecords()
        {
            return dao.QueryAllToTable();
        }

        public List<String> GetOneFieldData(String fieldName)
        {
            return dao.QueryAllOneField(fieldName);
        }

        public List<String> GetStationWithCity(String cityName)
        {
            return dao.QueryStationWithCity(cityName);
        }

        public DataTable GetByCityAndStation(String cityName, String stationName)
        {
            return dao.QueryByCityAndStation(cityName, stationName);
        }

    }
}
