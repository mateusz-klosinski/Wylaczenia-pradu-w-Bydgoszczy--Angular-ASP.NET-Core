using System;

namespace Enea.Services
{
    public class Disconnection
    {
        public string Area { get; private set; }
        public DateTime Date { get; private set; }
        public string Time { get; private set; }
        public string Details { get; private set; }

        public Disconnection(string area, string date, string details)
        {
            Area = area;
            Date = ConvertDate(date);
            Details = details;
            Time = date.Substring(11, date.Length - 11);
        }

        public override string ToString()
        {
            return Area;
        }

        public DateTime ConvertDate(string date)
        {
            DateTime convertedDate = DateTime.MinValue;
            DateTime.TryParse(date.Substring(0, 10), out convertedDate);
            return convertedDate;
        }



    }
}