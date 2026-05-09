using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>().ToTable("Candidates");
            modelBuilder.Entity<Skill>().ToTable("Skills");
            modelBuilder.Entity<CandidateSkill>().ToTable("CandidateSkills");

            #region CandidateSkill Configuration

            modelBuilder.Entity<CandidateSkill>()
                .HasKey(cs => new { cs.CandidateId, cs.SkillId });

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(cs => cs.Candidate)
                .WithMany(c => c.CandidateSkills)
                .HasForeignKey(cs => cs.CandidateId);
            modelBuilder.Entity<CandidateSkill>()
                .HasOne(cs => cs.Skill)
                .WithMany(s => s.CandidateSkills)
                .HasForeignKey(cs => cs.SkillId);

            #endregion

            #region Candidate Configuration

            modelBuilder.Entity<Candidate>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Candidate>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Candidate>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.ContactNumber)
                .IsUnique();

            modelBuilder.Entity<Candidate>()
                .Property(c => c.ContactNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<Candidate>()
                .Property(c => c.DateOfBirth)
                .IsRequired();

            #endregion

            #region Skill Configuration

            modelBuilder.Entity<Skill>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Skill>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            #endregion

            SeedData.Seed(modelBuilder);
        }
    }
}
