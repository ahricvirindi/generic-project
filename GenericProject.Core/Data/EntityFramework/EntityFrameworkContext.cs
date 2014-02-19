using System;
using System.Data.Entity;
using System.Reflection;
using GenericProject.Core.Injection;
using GenericProject.Core.Model;
using log4net;
using System.Data.Entity.Validation;

namespace GenericProject.Core.Data.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public EntityFrameworkContext()
            : base("GenericProject") { }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Title> Titles { get; set; }

        public DbSet<Relation> Relations { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Peep> Peeps { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }

        public DbSet<State> States { get; set; }

        public static EntityFrameworkContext Instance { get { return (EntityFrameworkContext)Injector.Get<DbContext>(); } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users).Map(m => m.MapLeftKey("UserId").MapRightKey("RoleId").ToTable("UserRoles"));
            modelBuilder.Entity<Peep>()
                        .HasMany(u => u.Relations)
                        .WithMany(r => r.Peeps)
                        .Map(m => m.MapLeftKey("PeepId").MapRightKey("RelationId").ToTable("PeepRelations"));

            modelBuilder.Entity<Peep>()
                        .HasMany(u => u.Tags)
                        .WithMany(r => r.Peeps)
                        .Map(m => m.MapLeftKey("PeepId").MapRightKey("TagId").ToTable("PeepTags"));
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                        log.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage), dbEx);
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error("A database error has occurred.", ex);
                throw;
            }
        }
    }
}