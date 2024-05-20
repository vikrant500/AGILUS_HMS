using HMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Migrations;
namespace HMS.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("HMS") { }
        static AppIdentityDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppIdentityDbContext, IdentityDbInit>());
        }
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Center> Centers { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<HMS.VeiwModels.ProfitLoss> ProfitLosses { get; set; }
        public DbSet<HMS.VeiwModels.Dashboard> Dashboards { get; set; }

        public DbSet<HMS.VeiwModels.InvoiceReport> InvoiceReports { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<PatientFeedback> PatientFeedbacks { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Loyality> Loyalities { get; set; }

        public DbSet<Taxes> Taxes { get; set; }

        public DbSet<LoyalityProcedure> LoyalityProcedures { get; set; }

        public DbSet<LoyalityDependents> LoyalityDependents { get; set; }


        public DbSet<StaffSalary> StaffSalaries { get; set; }

        public DbSet<DoctorSalary> DoctorSalaries { get; set; }

        public DbSet<Referal> Referals { get; set; }

        public DbSet<JustDial> JustDials { get; set; }

        public DbSet<InvoiceTransactions> InvoiceTransactions { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<DepartmentType> DepartmentTypes { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<UserPatientMapping> UserPatientMappings { get; set; }

        public System.Data.Entity.DbSet<HMS.Models.Biometric> Biometrics { get; set; }
    }
    public class IdentityDbInit : DbMigrationsConfiguration<Infrastructure.AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            //context.Database.ExecuteSqlCommand("ALTER DATABASE SCOPED CONFIGURATION SET [IDENTITY_CACHE]=[OFF]");
            //context.SaveChanges();
            base.Seed(context);
        }
        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));
            string roleName = "Administrators";
            string userName = "Admin";
            string password = "MySecret";
            string email = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email },
                password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
        }
    }
}