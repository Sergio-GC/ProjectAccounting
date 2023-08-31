namespace KidsService
{
    internal class Program
    {
        static KidsContext ctx;
        static void Main(string[] args)
        {
            ctx = new KidsContext();
            bool e = ctx.Database.EnsureCreated();

            if(e)
            {
                Console.WriteLine("Database has been created!");

                initDB();
                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("Database already exists :3");
            }
        }

        private static void initDB()
        {
            Console.WriteLine("Inserting demo data...");

            ctx.Kids.Add(new Kid()
            {
                Name = "Gabriel",
                LastName = "Dos Santos",
                Birthdate = new DateTime(2021, 10, 9)
            });

            ctx.Kids.Add(new Kid()
            {
                Name = "Carolina",
                LastName = "Do Pico",
                Birthdate = new DateTime(2018, 03, 23)
            });

            ctx.SaveChanges();
        }
    }
}