﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.HasKey("Guid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("tb_m_accounts");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_guid");

                    b.Property<Guid>("RoleGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_guid");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.HasIndex("RoleGuid");

                    b.ToTable("tb_tr_account_roles");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int")
                        .HasColumnName("manager_id");

                    b.Property<int>("NIK")
                        .HasColumnType("int")
                        .HasColumnName("nik");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone_number");

                    b.HasKey("Guid");

                    b.HasIndex("ManagerId");

                    b.HasIndex("NIK", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("tb_m_employees");
                });

            modelBuilder.Entity("API.Models.LeaveHistory", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<Guid>("LeaveRequestGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("leave_request_guid");

                    b.HasKey("Guid");

                    b.HasIndex("LeaveRequestGuid");

                    b.ToTable("tb_tr_leave_histories");
                });

            modelBuilder.Entity("API.Models.LeaveRequest", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<byte[]>("Attachment")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("attachment");

                    b.Property<int>("EligibleLeave")
                        .HasColumnType("int")
                        .HasColumnName("eligible_leave");

                    b.Property<Guid>("EmployeesGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("employees_guid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<Guid>("LeaveTypesGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("leave_types_guid");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("remarks");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("SubmitDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("submit_date");

                    b.Property<int>("TotalLeave")
                        .HasColumnType("int")
                        .HasColumnName("total_leave");

                    b.HasKey("Guid");

                    b.HasIndex("EmployeesGuid");

                    b.HasIndex("LeaveTypesGuid")
                        .IsUnique();

                    b.ToTable("tb_tr_leave_request");
                });

            modelBuilder.Entity("API.Models.LeaveType", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<string>("LeaveDescription")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("leave_description");

                    b.Property<string>("LeaveName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("leave_name");

                    b.HasKey("Guid");

                    b.ToTable("tr_m_leave_types");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_roles");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("API.Models.Account", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.HasOne("API.Models.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Employee", "Manager")
                        .WithMany("Employees")
                        .HasForeignKey("ManagerId")
                        .HasPrincipalKey("NIK");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("API.Models.LeaveHistory", b =>
                {
                    b.HasOne("API.Models.LeaveRequest", "LeaveRequest")
                        .WithMany("LeaveHistories")
                        .HasForeignKey("LeaveRequestGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaveRequest");
                });

            modelBuilder.Entity("API.Models.LeaveRequest", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("EmployeesGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.LeaveType", "LeaveType")
                        .WithOne("LeaveRequest")
                        .HasForeignKey("API.Models.LeaveRequest", "LeaveTypesGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("LeaveType");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Employees");

                    b.Navigation("LeaveRequests");
                });

            modelBuilder.Entity("API.Models.LeaveRequest", b =>
                {
                    b.Navigation("LeaveHistories");
                });

            modelBuilder.Entity("API.Models.LeaveType", b =>
                {
                    b.Navigation("LeaveRequest");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
