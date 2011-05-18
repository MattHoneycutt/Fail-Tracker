using System.Linq;

namespace FailTracker.Core.Data
{
	public interface IRepository<TEntity>
	{
		void Save(TEntity entity);

		IQueryable<TEntity> Query();
	}
}