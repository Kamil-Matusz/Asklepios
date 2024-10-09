using Shouldly;

namespace Asklepios.IntegrationTests
{
    [Collection("database")]
    public class DatabaseConnectionTests : BaseControllerTest, IDisposable
    {
        private readonly TestDatabase _testDatabase;

        public DatabaseConnectionTests(OptionsProvider optionsProvider) : base(optionsProvider)
        {
            _testDatabase = new TestDatabase();
        }
    
        public void Dispose()
        {
            _testDatabase?.Dispose();
        }

        [Fact]
        public async Task Should_Establish_Connection_To_Database()
        {
            // Act
            var canConnect = await _testDatabase.DbContext.Database.CanConnectAsync();

            // Assert
            canConnect.ShouldBeTrue();
        }
    }
}