using Backend.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        { }
        public DB() : this(new DbContextOptions<DB>())
        { }

        public DbSet<Family> Families { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne<Family>(m => m.Family)
                .WithMany(f => f.Members)
                .HasForeignKey(m => m.FamilyId);
        }
    }
}