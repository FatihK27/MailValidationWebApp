using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Huawei.RabbitMqSubscriberService.Models;

public partial class MailValidationContext : DbContext
{
    public MailValidationContext()
    {
    }

    public MailValidationContext(DbContextOptions<MailValidationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Validation> Validations { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseNpgsql("Server=localhost;Database=MailValidation;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Validation>(entity =>
        {
            entity.HasKey(e => e.recID);

            entity.ToTable("Validation");

            entity.Property(e => e.recID).HasColumnName("recID");
            entity.Property(e => e.mailAddress).HasColumnName("mailAddress");
            entity.Property(e => e.userID).HasColumnName("userID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
