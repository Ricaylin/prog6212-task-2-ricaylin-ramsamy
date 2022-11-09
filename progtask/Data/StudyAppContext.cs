using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Exceptions.SqlServer;

namespace progtask.Data;

public partial class StudyAppContext : DbContext
{
    public DbSet<User> users { get; set; } = null!;
    public DbSet<Module> modules { get; set; } = null!;

    public StudyAppContext()
    {
    }

    public StudyAppContext(DbContextOptions<StudyAppContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:dbs-cldv-st10083902.database.windows.net,1433;Initial Catalog=StudyApp;Persist Security Info=False;User ID=st10083902;Password=Cardsome77#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
