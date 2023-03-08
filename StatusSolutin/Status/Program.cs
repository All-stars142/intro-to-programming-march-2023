using Status;
using Marten;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "host=localhost;database=status_dev;username=postgres;password=TokyoJoe138!;port=5432";
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
});

var app = builder.Build();

app.MapGet("/status", async (IDocumentSession db) =>
{
    var response = await db.Query<StatusMessage>()
    .OrderByDescending(sm => sm.When)
    .FirstOrDefaultAsync(); if (response == null)
    {
        return Results.Ok(new StatusMessage(Guid.NewGuid(), "No Status to Report", DateTimeOffset.Now));
    }
    else
    {
        return Results.Ok(response);
    }
});

app.MapPost("/status", async (StatusChangeRequest request, IDocumentSession db) =>
{
    var messageToSave = new StatusMessage(Guid.NewGuid(), request.Message, DateTimeOffset.Now);
    db.Store<StatusMessage>(messageToSave);
    await db.SaveChangesAsync();
    return Results.Ok(messageToSave);
});


app.Run(); // "Block" 









//var statusMessage = new StatusMessage(Guid.NewGuid(), "Looks Good", DateTimeOffset.Now);

//Console.WriteLine($"The id {statusMessage.Id} status: {statusMessage.Message} at {statusMessage.When}");
