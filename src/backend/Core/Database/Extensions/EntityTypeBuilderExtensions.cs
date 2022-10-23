using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Core.Database.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static IndexBuilder<TEntity> HasUniqueConstraint<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, object?>> indexExpression)
            where TEntity : class
        {
            return builder.HasIndex(indexExpression).IsUnique();
        }
    }
}
