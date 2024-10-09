using Xunit.Abstractions;

namespace Asklepios.IntegrationTests;

public class CollectionOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderBy(tc => tc.DisplayName);
    }
}