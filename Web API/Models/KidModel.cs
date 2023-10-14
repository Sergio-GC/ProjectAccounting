public class KidModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public virtual List<KidModel>? Siblings { get; set; }
}