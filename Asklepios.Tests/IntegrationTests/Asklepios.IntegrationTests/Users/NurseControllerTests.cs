namespace Asklepios.IntegrationTests.Users;

[Collection("users")]
public class NurseControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public NurseControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}