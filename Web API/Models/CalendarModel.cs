public class CalendarModel
{
    public virtual KidModel Kid { get; set; }
    public DateTime Date { get; set; }
    public string Hours { get; set; }
    public double Price { get; set; }
    public Boolean IsDefinitive { get; set; }
}