using System;

namespace Backend.Database.Entities
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public ERole Role { get; set; }

        public Guid FamilyId { get; set; }
        public Family Family { get; set; }

        public enum ERole
        {
            Child,
            Parent,
        }
    }
}