using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SajhaSabal.Models;

namespace SajhaSabal;

public class SsdbContext : IdentityDbContext
{
    public SsdbContext(DbContextOptions<SsdbContext> options) : base(options)
    {

    }

    public DbSet<ActionModel> Actions { get; set; }
    public DbSet<ComplaintModel> Complaints { get; set; }
    public DbSet<DepartmentModel> Departments { get; set; }
    public DbSet<NoticeModel> Notices { get; set; }
    public DbSet<UserDetailModel> UserDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ActionModel>().ToTable("Action");
         builder.Entity<ComplaintModel>().ToTable("Complaint");
         builder.Entity<DepartmentModel>().ToTable("Department");
         builder.Entity<NoticeModel>().ToTable("Notice");
         builder.Entity<UserDetailModel>().ToTable("UserDetail");
        base.OnModelCreating(builder);
    }
}