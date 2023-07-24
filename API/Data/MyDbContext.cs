using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        //tables
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }

        // Other Configuration or Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasIndex(a => new {
                    a.Email
                }).IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => new {
                    e.NIK,
                    e.PhoneNumber
                }).IsUnique();

            // Role - AccountRole (One to Many)
            modelBuilder.Entity<Role>()
                        .HasMany(role => role.AccountRoles)
                        .WithOne(accountrole => accountrole.Role)
                        .HasForeignKey(accountrole => accountrole.RoleGuid);

            // Account - AccountRole (One to Many)
            modelBuilder.Entity<Account>()
                        .HasMany(account => account.AccountRoles)
                        .WithOne(accountrole => accountrole.Account)
                        .HasForeignKey(accountrole => accountrole.AccountGuid);

            // Account - Employee (One to One)
            modelBuilder.Entity<Account>()
                       .HasOne(account => account.Employee)
                       .WithOne(employee => employee.Account)
                       .HasForeignKey<Account>(account => account.Guid);


            // LeaveRequest - LeaveHistory (One to Many)
            modelBuilder.Entity<LeaveRequest>()
                        .HasMany(leaverequest => leaverequest.LeaveHistories)
                        .WithOne(leavehistory => leavehistory.LeaveRequest)
                        .HasForeignKey(leavehistory => leavehistory.LeaveRequestGuid);

            // LeaveRequest - LeaveType (One to One)
            modelBuilder.Entity<LeaveRequest>()
                        .HasOne(leaverequest => leaverequest.LeaveType)
                        .WithOne(leavetype => leavetype.LeaveRequest)
                        .HasForeignKey<LeaveRequest>(leaverequest => leaverequest.LeaveTypesGuid);

            // Employee - LeaveRequest (One to Many)
            modelBuilder.Entity<Employee>()
                        .HasMany(employee => employee.LeaveRequests)
                        .WithOne(leaverequest => leaverequest.Employee)
                        .HasForeignKey(leaverequest => leaverequest.EmployeesGuid);

            // Employee - Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.ManagerGuid);
        }
    }
}
