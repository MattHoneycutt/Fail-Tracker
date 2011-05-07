namespace TryCatchFail.CodeStock2011.FailTracker.Core.Data
{
	public interface IProvideQueries
	{
		T PrepareQuery<T>();
	}
}