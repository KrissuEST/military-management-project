namespace Tests.Unit;

// Uus, A.Käverilt
/*
public class ApiOwnersUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly OwnersController _controller;
    private readonly ApplicationDbContext _ctx;

    public ApiOwnersUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        // set up mock database - inMemory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new ApplicationDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<OwnersController>();

        // SUT
        _controller = new OwnersController(_ctx, logger);
    }

    [Fact(DisplayName = "GET - api/Owners with single data element")]
    public async Task testGetAllOwners()
    {
        // Arrange
        // Seed the data
        await SeedDataAsync();

        // Act
        var result = await _controller.GetOwners();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Value);
        //Assert.Empty(result.Value);
        Assert.NotEmpty(result.Value);
        Assert.Equal(1, result.Value.Count());
        Assert.Equal("Andres", result.Value.First().Name);
    }

    [Fact(DisplayName = "GET - api/Owners - Owner has just one pet")]
    public async Task TestOwnerWithOnePet()
    {
        await SeedDataAsync();
        var result = await _controller.GetOwnersWithSinglePet();

        Assert.NotNull(result);
    }


    private async Task SeedDataAsync()
    {
        _ctx.Owners.Add(new Owner()
        {
            Name = "Andres",
            Contact = "akaver"
        });
        await _ctx.SaveChangesAsync();
    }
}
*/