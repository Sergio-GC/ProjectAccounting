using KidsService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace KidsServiceBLL
{
    public class CalendarManager
    {
        private KidsContext context;
        private ConfigManager _configManager = ConfigManager.Instance;

        public CalendarManager(KidsContext context)
        {
            this.context = context;
        }

        public List<Calendar> GetCalendarsByMonth(DateTime month)
        {
            int monthNo = month.Month;
            List<Calendar> calendars = new List<Calendar>();

            calendars.AddRange(context.Calendars.Where(c => c.Date.Month == monthNo).ToList());
            return calendars;
        }

        public List<Calendar> GetCalendarsByKidAndMonth(DateTime month, int kidId, bool siblings)
        {
            int monthNo = month.Month;
            List<Calendar> calendars = new List<Calendar>();
            Kid kid = context.Kids.Where(k => k.Id == kidId).Single();

            calendars.AddRange(context.Calendars.Where(c => c.Date.Month == monthNo && c.Kid.Id == kidId).ToList());

            if(siblings)
            {
                foreach(Kid k in kid.Siblings)
                {
                    calendars.AddRange(context.Calendars.Where(c => c.Date.Month == monthNo && c.Kid.Id == k.Id).ToList());
                }
            }

            return calendars;
        }

        public List<Calendar> GetAllEntriesByKid(int kidId, bool siblings)
        {
            List<Calendar> result = new();

            Kid kid = context.Kids.Where(k => k.Id == kidId).Single();
            result.AddRange(context.Calendars.Where(c => c.Kid.Id == kidId).ToList());

            if (siblings)
            {
                foreach(Kid k in kid.Siblings)
                {
                    result.AddRange(context.Calendars.Where(c => c.Kid.Id == k.Id).ToList());
                }
            }

            return result;
        }

        public void AddEntry(Calendar calendar)
        {
            context.Calendars.Add(calendar);
            context.SaveChanges();
        }

        public void EditEntry(Calendar calendar)
        {
            Calendar dbCalendar = context.Calendars.Where(c => c.Id == calendar.Id).Single();
            dbCalendar = calendar;

            context.SaveChanges();
        }

        public void RemoveEntry(Calendar calendar)
        {
            context.Calendars.Remove(calendar);
            context.SaveChanges();
        }

        public List<Calendar> GetCalendarsByKid(Kid kid)
        {
            int days = _configManager.defaultDays;
            return context.Calendars.Where(c => c.Kid == kid && c.Date >= DateTime.Now && c.Date <= DateTime.Now.AddDays(days)).ToList();
        }

        public List<Calendar> GetCalendarsByKidAndSiblings(Kid kid)
        {
            // First add the entries for the given kid
            List<Calendar> result = (GetCalendarsByKid(kid));


            // Get list of siblings
            List<Kid> siblings = kid.Siblings;

            if(siblings != null)
            {
                // Get all their entries
                foreach (Kid k in siblings)
                {
                    List<Calendar> entries = GetCalendarsByKid(k);
                    result.AddRange(entries);
                }
            }

            return result;
        }

        public Calendar GetCalendar(int calendarId)
        {
            return context.Calendars.Where(c => c.Id == calendarId).Single();
        }

        public void ToggleDefinitive(Calendar calendar)
        {
            Calendar myCal = context.Calendars.Where(c => c.Id == calendar.Id).Single();
            myCal.IsDefinitive = !myCal.IsDefinitive;
        }

        public List<Calendar> GetNextDaysEntries()
        {
            int days = _configManager.defaultDays;
            return context.Calendars.Where(c => c.Date >= DateTime.Now && c.Date <= DateTime.Now.AddDays(days)).ToList();
        }

        public void CalculatePrice(Calendar calendar)
        {
            double result = 0.0;
            Calendar myCal = context.Calendars.Where(c => c.Id == calendar.Id).Single();
            String hours = myCal.Hours;

            // Parse the hours from the JSON String to a list of doubles
            List<String> parsedHours = JsonConvert.DeserializeObject<List<String>>(hours);
            List<double> rightHours = new List<double>();
            foreach(String h in parsedHours)
                rightHours.Add(double.Parse(h));

            // Calculate the hours
            for (int i = 0; i <  rightHours.Count; i += 2)
                result += (rightHours[i + 1] - rightHours[i]);

            // Multiply the hours by the price
            result *= 5;

            // Store the price rounded to the 0.1
            myCal.Price = Math.Round(result, 1);
        }
    }
}
