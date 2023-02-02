using Event_Calendar_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Calendar_WebApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ScheduleEvent> ScheduleEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<User> usersInit = new List<User>();
            usersInit.Add(new User() { UserId = 1, FirstName = "Test 1", LastName = "Test LastName", Email = "test1@gmail.com" , Username = "test1", Password = "12345" });
            usersInit.Add(new User() { UserId = 2, FirstName = "Test 2", LastName = "Test LastName", Email = "test2@gmail.com", Username = "test2", Password = "12345" });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(p => p.UserId);                
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Email).IsRequired(false);
                entity.Property(p => p.Username).IsRequired().HasMaxLength(15);
                entity.Property(p => p.Password).IsRequired();
                entity.HasData(usersInit);
            });

            List<Role> rolesInit = new List<Role>();
            rolesInit.Add(new Role() { RoleId = 1, Name = "Admin" });
            rolesInit.Add(new Role() { RoleId = 2, Name = "UserLocal" });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(p => p.RoleId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(20);
                entity.HasData(rolesInit);
            });

            List<UserRole> userRolesInit = new List<UserRole>();
            userRolesInit.Add(new UserRole() { UserRoleId = 1, UserId = 1, RoleId = 1 });
            userRolesInit.Add(new UserRole() { UserRoleId = 2, UserId = 2, RoleId = 2 });
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                entity.HasKey(p => p.UserRoleId);
                entity.HasOne(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p => p.RoleId);
                entity.HasOne(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId);
                entity.Property(p => p.UserId).IsRequired();
                entity.HasData(userRolesInit);
            });

            List<Schedule> schedulesInit = new List<Schedule>();
            schedulesInit.Add(new Schedule() { ScheduleId = 1, Name = "Schedule 1", UserId = 1 });
            schedulesInit.Add(new Schedule() { ScheduleId = 2, Name = "Schedule 2", UserId = 2 });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");
                entity.HasKey(p => p.ScheduleId);
                entity.HasOne(p => p.User).WithOne(p => p.Schedule).HasForeignKey<Schedule>(p => p.UserId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(20);
                entity.HasData(schedulesInit);
            });

            List<ScheduleEvent> scheduleEventsInit = new List<ScheduleEvent>();
            scheduleEventsInit.Add(new ScheduleEvent() { ScheduleEventId = 1, Name = "Event 1", ScheduleId = 1, Description = "description 1 test", CreationDate = DateTime.Today, Place =  "Brasil", TypeEventEnum = (int)TypeEvent.Exclusive });
            scheduleEventsInit.Add(new ScheduleEvent() { ScheduleEventId = 2, Name = "Event 2", ScheduleId = 2, Description = "description 2 test", CreationDate = DateTime.Today, Place = "Bolivia", TypeEventEnum = (int)TypeEvent.Share });
            modelBuilder.Entity<ScheduleEvent>(entity =>
            {
                entity.ToTable("ScheduleEvent");
                entity.HasKey(p => p.ScheduleEventId);
                entity.HasOne(p => p.Schedule).WithMany(p => p.ScheduleEvents).HasForeignKey(p=> p.ScheduleId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Description).IsRequired(false);
                entity.Property(p => p.CreationDate).IsRequired();
                entity.Property(p => p.TypeEventEnum).IsRequired();
                entity.HasData(scheduleEventsInit);
            });

        }
    }
}
