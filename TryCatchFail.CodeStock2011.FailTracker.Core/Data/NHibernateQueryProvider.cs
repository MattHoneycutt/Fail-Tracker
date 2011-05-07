using StructureMap;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Data
{
	public class NHibernateQueryProvider : IProvideQueries
	{
		private readonly IContainer _container;

		public NHibernateQueryProvider(IContainer container)
		{
			_container = container;
		}

		public T PrepareQuery<T>()
		{
			return _container.GetInstance<T>();
		}
	}
}