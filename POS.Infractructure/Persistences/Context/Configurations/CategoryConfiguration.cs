﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infractructure.Persistences.Context.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Categori__19093A0B0ABE882A");
            builder.Property(e => e.Id)
                .HasColumnName("CategoryId");

            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
