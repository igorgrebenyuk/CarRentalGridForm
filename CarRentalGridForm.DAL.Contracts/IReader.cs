namespace CarRentalGridForm.DAL.Contracts;

    /// <summary>
    /// Интерфейс для получения записей из контекста
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Предоставляет функкционалиные возможности для выполнения запросов 
        /// </summary>
       IQueryable<TEntity> Read<TEntity>() where TEntity : class;
    }

