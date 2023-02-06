using Event_Calendar_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Calendar_WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ScheduleEvent> ScheduleEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            List<User> usersInit = new List<User>();
            usersInit.Add(new User() { UserId = 1, FirstName = "Marco", LastName = "Aguilar", Email = "marco@gmail.com", UserName = "marco", Password = "marco", RoleId = 1 });
            usersInit.Add(new User() { UserId = 2, FirstName = "Jose", LastName = "Ramos", Email = "jose@gmail.com", UserName = "jose", Password = "jose", RoleId = 2 });
            usersInit.Add(new User() { UserId = 3, FirstName = "Maria", LastName = "Quiroz", Email = "maria@gmail.com", UserName = "maria", Password = "maria", RoleId = 2 });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(p => p.UserId);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Email).IsRequired(false);
                entity.Property(p => p.UserName).IsRequired().HasMaxLength(15);
                entity.Property(p => p.Password).IsRequired();
                entity.HasOne(p => p.Role).WithMany(p => p.Users).HasForeignKey(p => p.RoleId);
                entity.HasData(usersInit);
            });

            List<Schedule> schedulesInit = new List<Schedule>();
            schedulesInit.Add(new Schedule() { ScheduleId = 1, Name = "Schedule 2023 M ", UserId = 1 });
            schedulesInit.Add(new Schedule() { ScheduleId = 2, Name = "Schedule 2023 J ", UserId = 2 });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");
                entity.HasKey(p => p.ScheduleId);
                entity.HasOne(p => p.User).WithOne(p => p.Schedule).HasForeignKey<Schedule>(p => p.UserId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(20);
                entity.HasData(schedulesInit);
            });

            List<ScheduleEvent> scheduleEventsInit = new List<ScheduleEvent>();
            scheduleEventsInit.Add(new ScheduleEvent() { ScheduleEventId = 1, Name = "Event Visita", ScheduleId = 1, Description = "visitar a un colega", CreationDate = DateTime.Today, Place = "Brasil", Participants = 2, TypeEventEnum = (int)TypeEvent.Exclusive, ParentEventId = null });
            scheduleEventsInit.Add(new ScheduleEvent() { ScheduleEventId = 2, Name = "Event Familia", ScheduleId = 2, Description = "reunion familiar", CreationDate = DateTime.Today, Place = "Bolivia", Participants = 5, TypeEventEnum = (int)TypeEvent.Share, ParentEventId = null });
            modelBuilder.Entity<ScheduleEvent>(entity =>
            {
                entity.ToTable("ScheduleEvent");
                entity.HasKey(p => p.ScheduleEventId);
                entity.HasOne(p => p.Schedule).WithMany(p => p.ScheduleEvents).HasForeignKey(p => p.ScheduleId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Description).IsRequired(false);
                entity.Property(p => p.CreationDate).IsRequired();
                entity.Property(p => p.Participants).IsRequired();
                entity.Property(p => p.TypeEventEnum).IsRequired();
                entity.Property(p => p.ParentEventId).IsRequired(false);
                entity.HasData(scheduleEventsInit);
            });

        }
    }
}
