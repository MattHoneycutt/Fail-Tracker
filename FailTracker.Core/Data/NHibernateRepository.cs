using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace FailTracker.Core.Data
{
	public class NHibernateRepository<TEntity> : IRepository<TEntity>
	{
		private readonly ISession _session;

		public NHibernateRepository(ISession session)
		{
			_session = session;
		}

		public void Save(TEntity entity)
		{
			_session.Save(entity);
			//TODO: I don't like this...
			_session.Flush();
		}

		public IQueryable<TEntity> Query()
		{
			return _session.Query<TEntity>();
		}
	}
}