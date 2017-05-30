using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRecordSystem.src.Model
{
    public class AirRecord
    {
        private String timeDate;

        public String TimeDate
        {
            get { return timeDate; }
            set { timeDate = value; }
        }
        private String id;

        public String Id
        {
            get { return id; }
            set { id = value; }
        }
        private String city;

        public String City
        {
            get { return city; }
            set { city = value; }
        }
        private String station;

        public String Station
        {
            get { return station; }
            set { station = value; }
        }
        private String sCode;

        public String SCode
        {
            get { return sCode; }
            set { sCode = value; }
        }
        private String ofValue;

        public String OfValue
        {
            get { return this.ofValue; }
            set { this.ofValue = value; }
        }
        private String status;

        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        private String levelOf;

        public String LevelOf
        {
            get { return levelOf; }
            set { levelOf = value; }
        }
        private String priPolut;

        public String PriPolut
        {
            get { return priPolut; }
            set { priPolut = value; }
        }
        private String stamp;

        public String Stamp
        {
            get { return stamp; }
            set { stamp = value; }
        }
        private int aqi;

        public int Aqi
        {
            get { return aqi; }
            set { aqi = value; }
        }
        private String aqiColor;

        public String AqiColor
        {
            get { return aqiColor; }
            set { aqiColor = value; }
        }
        private int coAqi;

        public int CoAqi
        {
            get { return coAqi; }
            set { coAqi = value; }
        }
        private int no2Aqi;

        public int No2Aqi
        {
            get { return no2Aqi; }
            set { no2Aqi = value; }
        }
        private int o3Aqi;

        public int O3Aqi
        {
            get { return o3Aqi; }
            set { o3Aqi = value; }
        }
        private int o38hAqi;

        public int O38hAqi
        {
            get { return o38hAqi; }
            set { o38hAqi = value; }
        }
        private int pm10_24hAqi;

        public int Pm10_24hAqi
        {
            get { return pm10_24hAqi; }
            set { pm10_24hAqi = value; }
        }
        private int pm25_24hAqi;

        public int Pm25_24hAqi
        {
            get { return pm25_24hAqi; }
            set { pm25_24hAqi = value; }
        }
        private int so2Aqi;

        public int So2Aqi
        {
            get { return so2Aqi; }
            set { so2Aqi = value; }
        }
        private int no2;

        public int No2
        {
            get { return no2; }
            set { no2 = value; }
        }
        private int no2_24h;

        public int No2_24h
        {
            get { return no2_24h; }
            set { no2_24h = value; }
        }
        private int o3;

        public int O3
        {
            get { return o3; }
            set { o3 = value; }
        }
        private int o3_24h;

        public int O3_24h
        {
            get { return o3_24h; }
            set { o3_24h = value; }
        }
        private int o3_8h;

        public int O3_8h
        {
            get { return o3_8h; }
            set { o3_8h = value; }
        }
        private int o3_8h_24h;

        public int O3_8h_24h
        {
            get { return o3_8h_24h; }
            set { o3_8h_24h = value; }
        }
        private int pm10;

        public int Pm10
        {
            get { return pm10; }
            set { pm10 = value; }
        }
        private int pm10_24h;

        public int Pm10_24h
        {
            get { return pm10_24h; }
            set { pm10_24h = value; }
        }
        private int pm25;

        public int Pm25
        {
            get { return pm25; }
            set { pm25 = value; }
        }
        private int pm25_24h;

        public int Pm25_24h
        {
            get { return pm25_24h; }
            set { pm25_24h = value; }
        }
        private int so2;

        public int So2
        {
            get { return so2; }
            set { so2 = value; }
        }
        private int so2_24h;

        public int So2_24h
        {
            get { return so2_24h; }
            set { so2_24h = value; }
        }
    }
}
