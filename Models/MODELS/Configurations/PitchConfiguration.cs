﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.MODELS;
using System;
using System.Collections.Generic;

namespace Models.MODELS.Configurations
{
    public partial class PitchConfiguration : IEntityTypeConfiguration<Pitch>
    {
        public void Configure(EntityTypeBuilder<Pitch> entity)
        {
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.Horario)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("horario");

            entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.Property(e => e.Password)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("password");

            entity.Property(e => e.Role)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("((2))");

            entity.Property(e => e.Ubicacion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ubicacion");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Pitch> entity);
    }
}
