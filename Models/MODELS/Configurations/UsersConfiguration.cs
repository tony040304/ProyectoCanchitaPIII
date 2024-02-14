﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.MODELS;
using System;
using System.Collections.Generic;

namespace Models.MODELS.Configurations
{
    public partial class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.HasKey(e => e.Username)
                .HasName("PK__USERS__3214EC0766B73F2A");

            entity.ToTable("USERS");

            entity.Property(e => e.Username)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Role).HasColumnName("role");

            entity.Property(e => e.Userpassword)
                .IsUnicode(false)
                .HasColumnName("userpassword");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Users> entity);
    }
}
