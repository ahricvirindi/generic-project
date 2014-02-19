using System;
using System.Data.Entity;
using GenericProject.Core.Data.EntityFramework;

namespace GenericProject.Database
{
    public class DatabaseSeeder
    {
        private readonly DbContext _context;

        public DatabaseSeeder(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public void SeedDatabase()
        {
            var unitOfWork = new EntityFrameworkUnitOfWork(_context);

            var factory = new EntityFrameworkRepositoryFactory(unitOfWork);
            var generator = new DataGenerator(factory);

            if (generator.IsSeeded) return;

            generator.GenerateRoles();
            generator.GenerateUsers();
            generator.GenerateRelations();
            generator.GenerateTitles();
            generator.GenerateStates();
            generator.GenerateAddressTypes();
            generator.GeneratePeeps();

            unitOfWork.SubmitChanges();
        }
    }
}