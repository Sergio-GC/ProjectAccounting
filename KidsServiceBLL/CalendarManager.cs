using KidsService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsServiceBLL
{
    public class CalendarManager
    {
        private KidsContext context;

        public CalendarManager(KidsContext context)
        {
            this.context = context;
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
            return context.Calendars.Where(c => c.Kid == kid).ToList();
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

        public void ToggleDefinitive(Calendar calendar)
        {
            Calendar myCal = context.Calendars.Where(c => c.Id == calendar.Id).Single();
            myCal.IsDefinitive = !myCal.IsDefinitive;
        }

        public List<Calendar> GetNextDaysEntries()
        {
            // TODO Add possibility to change the days (3 - 5 - 7 - custom ...)
            return context.Calendars.Where(c => c.Date >= DateTime.Today && c.Date <= DateTime.Today.AddDays(5)).ToList();
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
