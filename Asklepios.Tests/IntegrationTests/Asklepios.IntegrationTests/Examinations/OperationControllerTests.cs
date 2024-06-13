namespace Asklepios.IntegrationTests.Examinations;

[Collection("examination")]
public class OperationControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public OperationControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}