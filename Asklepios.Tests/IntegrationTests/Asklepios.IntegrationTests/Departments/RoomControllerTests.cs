namespace Asklepios.IntegrationTests.Departments;

[Collection("departments")]
public class RoomControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public RoomControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}