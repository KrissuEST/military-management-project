namespace Tests.Unit;

// Uus, A.Käverilt
/*
public class PetsUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PetsController _controller;
    private readonly ApplicationDbContext _ctx;
    private readonly Mock<IPetNameCheckService> _petNameCheckServiceMock;

    public PetsUnitTests(ITestOutputHelper testOutputHelper)
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

        //var petNameCheckService = new PetNameCheckService();
        // create a fake verifiable PetNameCheckService
        _petNameCheckServiceMock = new Mock<IPetNameCheckService>();
        _petNameCheckServiceMock
            .Setup(x =>
                x.IsNameCoolAsync(
                    It.Is<string>(name => name.ToUpper().StartsWith("A")))
            )
            .ReturnsAsync(true)
            .Verifiable();

        // SUT
        _controller = new PetsController(_ctx, _petNameCheckServiceMock.Object);
    }


    [Fact(DisplayName = "GET - check that pet name is cool")]
    public async Task testCoolName()
    {
        // Arrange
        // Seed the data
        await SeedDataAsync();

        var id = _ctx.Pets.First().Id;
        // Act
        var result = await _controller.PetNameCheck(id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ActionResult<bool>>(result);
        Assert.True(result.Value);

        _petNameCheckServiceMock.Verify(
            x =>
                x.IsNameCoolAsync(
                    It.Is<string>(name => name.ToUpper().StartsWith("A"))),
            Times.Once
        );
        
        _petNameCheckServiceMock.Verify(
            x =>
                x.IsNameCoolAsync(
                    It.Is<string>(name => name.ToUpper().StartsWith("B"))),
            Times.Never
        );
    }

    private async Task SeedDataAsync()
    {
        _ctx.Owners.Add(new Owner()
        {
            Name = "Andres",
            Contact = "akaver",
            Pets = new List<Pet>()
            {
                new Pet()
                {
                    Breed = "labrador",
                    Name = "A-Fido",
                }
            }
        });
        await _ctx.SaveChangesAsync();
    }
}
*/