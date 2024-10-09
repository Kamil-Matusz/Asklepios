global using Xunit;

[assembly: TestCollectionOrderer("Asklepios.IntegrationTests.CollectionOrderer", "Asklepios.IntegrationTests")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]
