using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KidsService;
using KidsServiceBLL;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase
    {
        private readonly KidsContext _context;
        private readonly CalendarManager _calendarManager;
        private readonly KidsManager _kidsManager;

        public CalendarsController(KidsContext context)
        {
            _context = context;
            _calendarManager = new CalendarManager(context);
            _kidsManager = new KidsManager(context);
        }

        [HttpGet("GetNextCalendars")]
        public List<CalendarModel> GetNextCalendars()
        {
            List<CalendarModel> calendarModels = new List<CalendarModel>();
            List<Calendar> calendars = _calendarManager.GetNextDaysEntries();

            foreach(Calendar c in calendars)
            {
                calendarModels.Add(c.CalendarToModel());
            }

            return calendarModels;
        }

        // GET: api/Calendars/kidId
        [HttpGet("GetCalendarForKid/{kidId}")]
        public List<CalendarModel> GetCalendarsForKid(int kidId)
        {
            List<CalendarModel> result = new();

            Kid kid = _kidsManager.GetKidById(kidId);
            List<Calendar> calendars = _calendarManager.GetCalendarsByKid(kid);

            foreach(Calendar calendar in calendars)
            {
                result.Add(calendar.CalendarToModel());
            }

            return result;
        }

        // GET: api/Calendars/5
        [HttpGet("GetCalendar/{id}")]
        public CalendarModel GetCalendar(int id)
        {
            return _calendarManager.GetCalendar(id).CalendarToModel();
        }

        // GET: api/Calendars/GetAllEntriesByKid/1
        [HttpGet("GetAllEntriesByKid/{id}")]
        public List<CalendarModel> GetAllEntriesByKid(int id, bool siblings)
        {
            List<CalendarModel> result = new();

            var calendars = _calendarManager.GetAllEntriesByKid(id, siblings);
            foreach( Calendar calendar in calendars)
            {
                result.Add(calendar.CalendarToModel());
            }

            return result;
        }

        [HttpGet("GetCalendarsByMonth/{month}")]
        public List<CalendarModel> GetCalendarsByMonth(DateTime month)
        {
            List<CalendarModel> result = new();
            List<Calendar> calendars = _calendarManager.GetCalendarsByMonth(month);

            foreach(Calendar c in calendars)
            {
                result.Add(c.CalendarToModel());
            }

            return result;
        }

        [HttpGet("GetCalendarsByKidAndMonth/{month}&{kid}&{siblings}")]
        public List<CalendarModel> GetCalendarsByKidAndMonth(DateTime month, int kid, bool siblings)
        {
            List<CalendarModel> result = new();
            List<Calendar> calendars = _calendarManager.GetCalendarsByKidAndMonth(month, kid, siblings);

            foreach(Calendar c in calendars)
            {
                result.Add(c.CalendarToModel());
            }

            return result;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutCalendar(int id, CalendarModel calendar)
        {
            Calendar ori = _calendarManager.GetCalendar(id);
            ori.Hours = calendar.Hours;
            ori.IsDefinitive = calendar.IsDefinitive;
            ori.Price = calendar.Price;
            ori.Date = calendar.Date;
            ori.Kid = calendar.Kid.KidModelToKid(true);

            _context.SaveChanges();
        }

        [HttpPut("ValidateEntry/{id}")]
        public void ValidateEntry(int id)
        {
            Calendar calendar = _calendarManager.GetCalendar(id);
            _calendarManager.ToggleDefinitive(calendar);

            _context.SaveChanges();
        }

        // POST: api/Calendars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostCalendar(CalendarModel calendar)
        {
            Kid kid = _kidsManager.GetKidById(calendar.Kid.Id);
            Calendar cal = calendar.CalendarModelToCalendar();
            cal.Kid = kid;

            _calendarManager.AddEntry(cal);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public void DeleteCalendar(int id)
        {
            Calendar calendar = _calendarManager.GetCalendar(id);
            _calendarManager.RemoveEntry(calendar);
        }
    }
}
