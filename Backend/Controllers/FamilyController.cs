using System;
using System.Linq;
using Backend.Database;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class FamilyController : Controller
    {
        public FamilyController(DB db)
        {
            _db = db;
        }

        private readonly DB _db;

        [HttpGet]
        public Family[] List()
        {
            var familiesDb = _db.Families.ToArray();

            var familiesModel = familiesDb.Select(m => new Family
            {
                Id = m.Id,
                Street = m.Street,
                HouseNumber = m.HouseNumber,
                City = m.City,
                Zip = m.Zip,
            })
            .ToArray();

            return familiesModel;
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            var familyDb = _db.Families.Find(id);
            if (familyDb == null)
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, "Family not found");

            var familyModel = new Family
            {
                Id = familyDb.Id,
                Street = familyDb.Street,
                HouseNumber = familyDb.HouseNumber,
                City = familyDb.City,
                Zip = familyDb.Zip,
            };

            return Ok(familyModel);
        }

        [HttpPost]
        public ActionResult Update([FromBody] Family family)
        {
            var familyDb = family.Id != null
                ? _db.Families.Find(family.Id)
                : new Database.Entities.Family();

            if (familyDb == null)
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, "Family not found");

            if (familyDb.Id == default)
            {
                familyDb.Id = Guid.NewGuid();
                _db.Add(familyDb);
            }

            familyDb.Street = family.Street;
            familyDb.HouseNumber = family.HouseNumber;
            familyDb.City = family.City;
            familyDb.Zip = family.Zip;

            _db.SaveChanges();

            return Ok(familyDb.Id);
        }

        [HttpDelete]
        public void Delete(Guid id)
        {
            var family = _db.Families.Find(id);
            _db.Remove(family);
            _db.SaveChanges();
        }
    }
}