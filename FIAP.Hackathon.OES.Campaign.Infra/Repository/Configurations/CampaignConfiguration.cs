using FIAP.Hackathon.OES.Campaign.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.FCG.Campaign.Infrastructure.Repository.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<CampaignEntity>
{
    public void Configure(EntityTypeBuilder<CampaignEntity> builder)
    {
        builder.ToTable("Campaign");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("bigint")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.StartedDateTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(p => p.FinishedDateTime)
            .HasColumnType("timestamp without time zone");

        builder.Property(p => p.FinancialGoal)
            .IsRequired();

        builder.Property(p => p.TotalDonationsCollected)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.Status)
            .HasConversion<int>();
    }
}
