namespace KidsService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Kid kid = new Kid();
            kid.Name = "Test kiddo";

            Console.WriteLine(kid.Name);
        }
    }
}