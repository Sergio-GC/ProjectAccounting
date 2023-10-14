using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KidsService;
using KidsServiceBLL;
using NuGet.Protocol;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsController : ControllerBase
    {
        private readonly KidsManager _kidsManager;
        private readonly KidsContext _context;

        public KidsController(KidsContext context)
        {
            _context = context;
            _kidsManager = new KidsManager(context);
        }

        // GET: api/Kids
        [HttpGet]
        public List<KidModel> GetKids()
        {
            List<KidModel> kidModels = new List<KidModel>();
            List<Kid> kids = _kidsManager.GetKids();

            foreach(Kid k in kids)
            {
                kidModels.Add(k.KidToModel(true));
            }

            return kidModels;
        }

        // GET: api/Kids/5
        [HttpGet("{id}")]
        public KidModel GetKid(int id)
        {
            return _kidsManager.GetKidById(id).KidToModel(true);
        }

        // PUT: api/Kids/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void UpdateKid(int id, KidModel kidModel)
        {
            Kid kid = _kidsManager.GetKidById(id);
            Kid newKid = kidModel.KidModelToKid(true);

            kid.Name = newKid.Name;
            kid.LastName = newKid.LastName;
            kid.Birthdate = newKid.Birthdate;
            kid.Siblings = newKid.Siblings;

            _context.SaveChanges();
        }

        // POST: api/Kids
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostKid(KidModel kid)
        {
            _kidsManager.AddKid(kid.KidModelToKid(true));
            _context.SaveChanges();
        }

        // DELETE: api/Kids/5
        [HttpDelete("{id}")]
        public void DeleteKid(int id)
        {
            Kid kid = _kidsManager.GetKidById(id);
            _kidsManager.RemoveKid(kid);

            _context.SaveChanges();
        }
    }
}
