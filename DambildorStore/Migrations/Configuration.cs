namespace DambildorStore.Migrations
{
    using DambildorStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DambildorStore.Models.DambildorModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DambildorStore.Models.DambildorModel context)
        {
            context.ManagerTypes.AddOrUpdate(s => s.ID, new ManagerType() { ID = 1, Name = "Admin" });
            context.ManagerTypes.AddOrUpdate(s => s.ID, new ManagerType() { ID = 2, Name = "Moderatör" });
            context.Managers.AddOrUpdate(s => s.ID, new Manager() { ID = 1, Name = "John", Surname = "Doe", Mail = "johndoe26@hotmail.com", Password = "1234", ManagerType_ID = 1, ProfileImage = "none.png", Status = true, CreationDate = Convert.ToDateTime("02/07/2022") });
        }
    }
}
