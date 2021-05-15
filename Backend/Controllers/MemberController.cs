using System;
using System.Linq;
using Backend.Database;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class MemberController : Controller
    {
        public MemberController(DB db)
        {
            _db = db;
        }

        private readonly DB _db;

        [HttpGet]
        public Member[] List()
        {
            var membersDb = _db.Members.ToArray();

            var membersModel = membersDb.Select(m => new Member
            {
                Id = m.Id,
                FirstName = m.FirstName,
                MiddleName = m.MiddleName,
                LastName = m.LastName,
                Role = m.Role.ToString(),
                FamilyId = m.FamilyId,
            })
            .ToArray();

            return membersModel;
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            var memberDb = _db.Members.Find(id);
            if (memberDb == null)
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, "Member not found");

            var memberModel = new Member
            {
                Id = memberDb.Id,
                FirstName = memberDb.FirstName,
                MiddleName = memberDb.MiddleName,
                LastName = memberDb.LastName,
                Role = memberDb.Role.ToString(),
                FamilyId = memberDb.FamilyId,
            };

            return Ok(memberModel);
        }

        [HttpPost]
        public ActionResult Update([FromBody] Member member)
        {
            var memberDb = _db.Members.Find(member.Id);

            if (memberDb == null)
            {
                memberDb = new Database.Entities.Member
                {
                    Id = member.Id,
                };
                _db.Add(memberDb);
            }

            memberDb.FirstName = member.FirstName;
            memberDb.MiddleName = member.MiddleName;
            memberDb.LastName = member.LastName;
            memberDb.Role = Enum.Parse<Database.Entities.Member.ERole>(member.Role, true);
            memberDb.FamilyId = member.FamilyId;

            _db.SaveChanges();

            return Ok(memberDb.Id);
        }

        [HttpDelete]
        public void Delete(Guid id)
        {
            var member = _db.Members.Find(id);
            _db.Remove(member);
            _db.SaveChanges();
        }
    }
}