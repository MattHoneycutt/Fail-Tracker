using Moq;
using SpecsFor;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;

namespace TryCatchFail.CodeStock2011.UnitTests.Util
{
	public static class MockIProvideQueriesExtensions
	{
		public static Mock<TQuery> GetQueryMockFor<TQuery>(this ITestState state) where TQuery : class
		{
			var mockQuery = new Mock<TQuery>(null);
			state.GetMockFor<IProvideQueries>()
				.Setup(q => q.PrepareQuery<TQuery>())
				.Returns(mockQuery.Object);

			return mockQuery;
		}
	}
}