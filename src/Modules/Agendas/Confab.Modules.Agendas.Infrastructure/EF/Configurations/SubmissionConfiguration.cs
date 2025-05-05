using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations;

internal sealed class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));
            
        builder
            .Property(x => x.ConferenceId)
            .HasConversion(x => x.Value, x => new ConferenceId(x));

        builder
            .Property(x => x.Version)
            .IsConcurrencyToken();

        builder
            .Property(x => x.Tags)
            .HasConversion(
                convertToProviderExpression: collection => string.Join(",", collection),
                convertFromProviderExpression: flattenString => flattenString.Split(",", StringSplitOptions.None));

        builder
            .Property(x => x.Tags)
            .Metadata
            .SetValueComparer(
                new ValueComparer<IEnumerable<string>>(
                    equalsExpression: (c1, c2) => c1.SequenceEqual(c2),
                    hashCodeExpression: c => c.Aggregate(0, (a, next) => HashCode.Combine(a, next.GetHashCode())),
                    snapshotExpression: c=>c.ToArray()));
    }
}