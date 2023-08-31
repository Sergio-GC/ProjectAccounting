using KidsService;

namespace KidsServiceBLL
{
    public class KidsManager
    {
        private KidsContext context;

        public KidsManager(KidsContext context)
        {
            this.context = context;
        }

        
        public List<Kid> GetKids()
        {
            return context.Kids.ToList();
        }

        public void AddKid(Kid kid)
        {
            context.Kids.Add(kid);
            context.SaveChanges();
        }

        public void RemoveKid(Kid kid)
        {
            context.Kids.Remove(kid);
            context.SaveChanges();
        }

        public void UpdateKid(Kid kid)
        {
            Kid dbKid = context.Kids.Where(k => k.Id == kid.Id).Single();
            if (dbKid != null)
            {
                dbKid = kid;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Unexpected error!");
            }
        }

        public void AddSibling(Kid kid, Kid sibling)
        {
            if (kid.Siblings == null)
                kid.Siblings = new List<Kid>();

            kid.Siblings.Add(sibling);

            
            if(sibling.Siblings == null)
                sibling.Siblings = new List<Kid>();

            sibling.Siblings.Add(kid);


            context.SaveChanges();
        }

        public void DeleteSibling(Kid kid, Kid sibling)
        {
            if (kid.Siblings == null)
                throw new Exception("The given kid has no siblings!");

            if (sibling.Siblings == null)
                throw new Exception("The given sibling has no siblings! So he cannot be a sibling of the given kid");

            Boolean isOk = false;
            foreach(Kid k in kid.Siblings){
                if(k.Id == sibling.Id)
                {
                    isOk = true;
                    break;
                }
            }

            if (!isOk)
                throw new Exception("The given sibling is not a sibling of the given kid!");

            kid.Siblings.Remove(sibling);
            sibling.Siblings.Remove(kid);

            context.SaveChanges();
        }

        public Kid GetKidById(int id)
        {
            Kid result = context.Kids.Where(k => k.Id == id).Single();
            
            if (result != null)
                return result;
            else
                throw new Exception("There is no kid for the given id");
        }
    }
}