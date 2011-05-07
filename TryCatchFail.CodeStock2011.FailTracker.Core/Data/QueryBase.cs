namespace TryCatchFail.CodeStock2011.FailTracker.Core.Data
{
	public abstract class QueryBase<TOptions, TResult>
	{
		public abstract TResult Run(TOptions options);
	}
}