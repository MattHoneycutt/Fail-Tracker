using System.Linq;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Data
{
	public interface IRepository<TEntity>
	{
		void Save(TEntity entity);

		IQueryable<TEntity> Query();
	}
}