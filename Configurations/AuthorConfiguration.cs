using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patikadev_RestfulApi.Domain;
using System.Reflection.Emit;

namespace Patikadev_RestfulApi.Configurations;

public class AuthorConfiguration: IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.BirthDate).IsRequired(true);


    }

   

}
