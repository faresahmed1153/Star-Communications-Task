namespace Stars_Communication.Core.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task AddAsync(T entity);

	}
}
