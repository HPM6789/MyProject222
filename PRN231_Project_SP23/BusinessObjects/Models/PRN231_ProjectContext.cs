﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.Models
{
    public partial class PRN231_ProjectContext : DbContext
    {
        public PRN231_ProjectContext()
        {
        }

        public PRN231_ProjectContext(DbContextOptions<PRN231_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SubmitAssignment> SubmitAssignments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.AssignmentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Assignmen__Cours__2F10007B");

                entity.HasOne(d => d.Uploader)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.UploaderId)
                    .HasConstraintName("FK__Assignmen__Uploa__300424B4");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.MaterialName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Materials__Cours__2B3F6F97");

                entity.HasOne(d => d.Uploader)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.UploaderId)
                    .HasConstraintName("FK__Materials__Uploa__2C3393D0");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubmitAssignment>(entity =>
            {
                entity.ToTable("SubmitAssignment");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.SubmitAssignmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.SubmitAssignments)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK__SubmitAss__Assig__33D4B598");

                entity.HasOne(d => d.Uploader)
                    .WithMany(p => p.SubmitAssignments)
                    .HasForeignKey(d => d.UploaderId)
                    .HasConstraintName("FK__SubmitAss__Uploa__32E0915F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}