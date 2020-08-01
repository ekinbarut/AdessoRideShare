using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Models
{
    public class ARSDBContext : DbContext
    {
        public ARSDBContext() : base("MAIN")
        {
           
        }

        public static ARSDBContext Create()
        {
            return new ARSDBContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Travels>()
                .HasRequired(c => c.ArrivingCountry)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Travels>()
                .HasRequired(c => c.DepartingCountry)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TravelUsers>()
                        .HasKey(c => new { c.TravelId, c.UserId })
                        .HasRequired(c=> c.Travel)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            Database.SetInitializer<ARSDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Countries> Countries { get; set; }

        public DbSet<Travels> Travels { get; set; }

        public DbSet<TravelUsers> TravelUsers { get; set; }

    }
}
