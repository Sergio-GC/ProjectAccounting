using KidsService;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class ExtensionConverter
{
    public static KidModel KidToModel(this Kid kid, bool firstKid)
    {
        KidModel result = new KidModel();

        result.Id = kid.Id;
        result.Birthdate = kid.Birthdate;
        result.Name = kid.Name;
        result.LastName = kid.LastName;

        if (kid.Siblings != null && firstKid)
        {
            foreach (Kid km in kid.Siblings)
            {
                result.Siblings.Add(km.KidToModel(false));
            }
        }
        else
        {
            result.Siblings = new List<KidModel>();
        }

        return result;
    }

    public static Kid KidModelToKid(this KidModel kid, bool firstKid)
    {
        Kid result = new Kid();

        result.Id = kid.Id;
        result.LastName = kid.LastName;
        result.Name = kid.Name;
        result.Birthdate = kid.Birthdate;

        if(kid.Siblings != null && firstKid)
        {
            foreach(KidModel km in kid.Siblings)
            {
                result.Siblings.Add(km.KidModelToKid(false));
            }
        }
        else
        {
            result.Siblings = new List<Kid>();
        }

        return result;
    }

    public static CalendarModel CalendarToModel(this Calendar calendar)
    {
        CalendarModel result = new CalendarModel();

        result.Id = calendar.Id;
        result.Price = calendar.Price;
        result.IsDefinitive = calendar.IsDefinitive;
        result.Date = calendar.Date;
        result.Hours = calendar.Hours;
        result.Kid = calendar.Kid.KidToModel(true);

        return result;
    }

    public static Calendar CalendarModelToCalendar(this CalendarModel calendarModel)
    {
        Calendar result = new Calendar();

        result.Price = calendarModel.Price;
        result.Hours = calendarModel.Hours;
        result.Id = calendarModel.Id;
        result.IsDefinitive = calendarModel.IsDefinitive;
        result.Date = calendarModel.Date;
        result.Kid = calendarModel.Kid.KidModelToKid(true);

        return result;
    }
}