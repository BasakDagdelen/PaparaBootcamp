﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patikadev_RestfulApi.Domain;

namespace Patikadev_RestfulApi.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.Price).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Image).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.IsActive).IsRequired(true);

            builder.HasOne(b => b.Author)
              .WithMany(a => a.Books)
              .HasForeignKey(b => b.AuthorId)
              .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(b => b.Genre)
                  .WithMany(g => g.Books)
                  .HasForeignKey(b => b.GenreId)
                  .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
