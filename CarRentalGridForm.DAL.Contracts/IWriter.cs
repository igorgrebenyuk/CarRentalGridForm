using System.Diagnostics.CodeAnalysis;

namespace CarRentalGridForm.DAL.Contracts;

    public interface IWriter
    {
        void Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

        void Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

        void Delete<TEntity>([NotNull] TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();
}

