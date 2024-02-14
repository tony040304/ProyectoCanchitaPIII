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
            entity.HasKey(e => e.Nombre);

            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.Canchas).IsUnicode(false);

            entity.Property(e => e.Horario).IsUnicode(false);

            entity.Property(e => e.Hubicacion).IsUnicode(false);

            entity.HasOne(d => d.NombreNavigation)
                .WithOne(p => p.Pitch)
                .HasForeignKey<Pitch>(d => d.Nombre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pitch__Nombre__02084FDA");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Pitch> entity);
    }
}
