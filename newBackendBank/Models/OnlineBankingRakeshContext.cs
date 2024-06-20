using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace newBackendBank.Models;

public partial class OnlineBankingRakeshContext : DbContext
{
    public OnlineBankingRakeshContext()
    {
    }

    public OnlineBankingRakeshContext(DbContextOptions<OnlineBankingRakeshContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminDeposit> AdminDeposits { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<DepositRequest> DepositRequests { get; set; }

    public virtual DbSet<GeneratedDetail> GeneratedDetails { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<DailyTransactionCount> DailyTrans { get; set; }


    public virtual DbSet<UserDetail> UserDetails { get; set; }

    
    public virtual DbSet<UserNetBankDetail> UserNetBankDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:rakeshkserver.database.windows.net,1433;Initial Catalog=OnlineBanking(Rakesh);Persist Security Info=False;User ID=rakesh;Password=Raki@3272;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {

            modelBuilder.Entity<DailyTransactionCount>()
           .HasNoKey();

            entity.HasKey(e => e.AdminId).HasName("PK__ADMIN__719FE48893367227");

            entity.ToTable("ADMIN");

            entity.Property(e => e.AdminEmail)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AdminName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AdminPassword)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AdminDeposit>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PK__AdminDep__AB60DF7101593737");

            entity.Property(e => e.DepositAmmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DepositDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Branch__A1682FA56682FDFF");

            entity.ToTable("Branch");

            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.BranchAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<DepositRequest>(entity =>
        {
            entity.HasKey(e => e.MessegeId).HasName("PK__DepositR__09FAF729D0616550");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DepositStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("pending");
            entity.Property(e => e.RecivedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<GeneratedDetail>(entity =>
        {
            entity.HasKey(e => e.GeneratedId).HasName("PK__Generate__B0D6615C12E8DF31");

            entity.HasIndex(e => e.UserId, "UQ__Generate__1788CCAD3A90B037").IsUnique();

            entity.Property(e => e.BranchName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IfseCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Pan)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.GeneratedDetail)
                .HasForeignKey<GeneratedDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Generated__Branc__6754599E");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B7851838F");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId).HasName("PK__UserDeta__7286828D07EA718E");

            entity.Property(e => e.UserDetailsId).HasColumnName("userDetailsID");
            entity.Property(e => e.Aadhar).HasMaxLength(12);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.AnnualIncome).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CountryName).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.DrivingLicense).HasMaxLength(20);
            entity.Property(e => e.EducationalQualification).HasMaxLength(100);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.OccupationType).HasMaxLength(50);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .HasColumnName("PAN");
            entity.Property(e => e.Pin).HasMaxLength(10);
            entity.Property(e => e.Place).HasMaxLength(50);
            entity.Property(e => e.ProfilePhoto).IsUnicode(false);
            entity.Property(e => e.SignaturePhoto)
                .IsUnicode(false)
                .HasColumnName("signaturePhoto");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.VoterId).HasMaxLength(20);
        });

        modelBuilder.Entity<UserNetBankDetail>(entity =>
        {
            entity.HasKey(e => e.UserNetBankId).HasName("PK__UserNetB__A9A524ED119C36C3");

            entity.Property(e => e.UserNetBankId)
                .ValueGeneratedNever()
                .HasColumnName("userNetBankID");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .HasColumnName("PAN");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
