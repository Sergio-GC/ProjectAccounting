using KidsService;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class ExtensionConverter
{
    public static KidModel KidToModel(this Kid kid)
    {
        KidModel result = new KidModel();

        result.Birthdate = kid.Birthdate;
        result.Name = kid.Name;
        result.LastName = kid.LastName;
        
        if(kid.Siblings != null)
        {
            List<KidModel> siblings = new List<KidModel>();

            foreach(Kid sibling in kid.Siblings)
            {
                KidModel km = new KidModel();

                km.Name = sibling.Name;
                km.LastName = sibling.LastName;
                km.Birthdate = sibling.Birthdate;

                siblings.Add(km);
            }
            result.Siblings = siblings;
        }
        else
        {
            result.Siblings = null;
        }

        return result;
    }

    public static CalendarModel CalendarToModel(this Calendar calendar)
    {
        CalendarModel result = new CalendarModel();

        result.Price = calendar.Price;
        result.IsDefinitive = calendar.IsDefinitive;
        result.Date = calendar.Date;
        result.Hours = calendar.Hours;
        result.Kid = calendar.Kid.KidToModel();

        return result;
    }
}