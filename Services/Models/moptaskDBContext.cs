using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Services.Models
{
    public partial class moptaskDBContext : DbContext
    {
        public moptaskDBContext()
        {
        }

        public moptaskDBContext(DbContextOptions<moptaskDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswerVote> AnswerVotes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionVote> QuestionVotes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-AAC3EBK;Database=moptaskDB;Trusted_Connection=True; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AnswerVote>(entity =>
            {
                entity.ToTable("answerVotes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.OperationCode)
                    .HasMaxLength(20)
                    .HasColumnName("operation_code");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.AnswerVotes)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("fk_votes_answers");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.QuestionText)
                    .HasMaxLength(256)
                    .HasColumnName("question_text");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_question");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.ToTable("questionAnswers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnswerText)
                    .HasMaxLength(256)
                    .HasColumnName("answer_text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("fk_question_answer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_answer");
            });

            modelBuilder.Entity<QuestionVote>(entity =>
            {
                entity.ToTable("questionVotes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OperationCode)
                    .HasMaxLength(20)
                    .HasColumnName("operation_code");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionVotes)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("fk_votes_questions");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
