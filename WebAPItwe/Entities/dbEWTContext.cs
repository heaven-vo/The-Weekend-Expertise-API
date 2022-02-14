using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class dbEWTContext : DbContext
    {
        public dbEWTContext()
        {
        }

        public dbEWTContext(DbContextOptions<dbEWTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cafe> Cafes { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberSession> MemberSessions { get; set; }
        public virtual DbSet<Mentor> Mentors { get; set; }
        public virtual DbSet<MentorMajor> MentorMajors { get; set; }
        public virtual DbSet<MentorSession> MentorSessions { get; set; }
        public virtual DbSet<MentorSkill> MentorSkills { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:duongas.database.windows.net,1433;Initial Catalog=dbTWE;Persist Security Info=False;User ID=twe;Password=abcd123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<Cafe>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CloseTime).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(350);

                entity.Property(e => e.Distric)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.OpenTime).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(350);

                entity.Property(e => e.Birthday).HasPrecision(2);

                entity.Property(e => e.Fullname).HasMaxLength(150);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.MajorId).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Sex).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Members_Major");
            });

            modelBuilder.Entity<MemberSession>(entity =>
            {
                entity.ToTable("MemberSession");

                entity.HasIndex(e => e.MemberId, "IX_MemberSession_MemberId");

                entity.HasIndex(e => e.SessionId, "IX_MemberSession_SessionId");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Feedback).HasMaxLength(450);

                entity.Property(e => e.MemberId).HasMaxLength(50);

                entity.Property(e => e.SessionId).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberSessions)
                    .HasForeignKey(d => d.MemberId);

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.MemberSessions)
                    .HasForeignKey(d => d.SessionId);
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Birthday).HasPrecision(2);

                entity.Property(e => e.Fullname).HasMaxLength(150);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Sex).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<MentorMajor>(entity =>
            {
                entity.ToTable("MentorMajor");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.MajorId).HasMaxLength(50);

                entity.Property(e => e.MentorId).HasMaxLength(50);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.MentorMajors)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_MentorMajor_Major");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.MentorMajors)
                    .HasForeignKey(d => d.MentorId)
                    .HasConstraintName("FK_MentorMajor_Mentors");
            });

            modelBuilder.Entity<MentorSession>(entity =>
            {
                entity.ToTable("MentorSession");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AcceptDate).HasPrecision(2);

                entity.Property(e => e.MentorId).HasMaxLength(50);

                entity.Property(e => e.RequestDate).HasPrecision(2);

                entity.Property(e => e.SessionId).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.MentorSessions)
                    .HasForeignKey(d => d.MentorId)
                    .HasConstraintName("FK_MentorSession_Mentors");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.MentorSessions)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_MentorSession_Session");
            });

            modelBuilder.Entity<MentorSkill>(entity =>
            {
                entity.HasIndex(e => e.MentorId, "IX_MentorSkills_MentorId");

                entity.HasIndex(e => e.SkillId, "IX_MentorSkills_SkillId");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.MentorId).HasMaxLength(50);

                entity.Property(e => e.SkillId).HasMaxLength(50);

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.MentorSkills)
                    .HasForeignKey(d => d.MentorId);

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.MentorSkills)
                    .HasForeignKey(d => d.SkillId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.SessionId).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_Payment_Session");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.HasIndex(e => e.CafeId, "IX_Session_CafeId");

                entity.HasIndex(e => e.MentorId, "IX_Session_MentorId");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CafeId).HasMaxLength(50);

                entity.Property(e => e.Date).HasPrecision(2);

                entity.Property(e => e.MemberId).HasMaxLength(50);

                entity.Property(e => e.MentorId).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.SubjectId).HasMaxLength(50);

                entity.HasOne(d => d.Cafe)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.CafeId);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Session_Members");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.MentorId)
                    .HasConstraintName("FK_Session_Mentors");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.MajorId).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Subject_Major");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.CafeId).HasMaxLength(50);

                entity.Property(e => e.DayFilter)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.DiscountRate)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.EndDate)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.HourFilter)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MaxAmount)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MinPerson)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Cafe)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.CafeId)
                    .HasConstraintName("FK_Voucher_Cafes");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
