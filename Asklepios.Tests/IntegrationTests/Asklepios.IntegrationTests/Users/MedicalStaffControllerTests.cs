namespace Asklepios.IntegrationTests.Users;

[Collection("users")]
public class MedicalStaffControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public MedicalStaffControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}