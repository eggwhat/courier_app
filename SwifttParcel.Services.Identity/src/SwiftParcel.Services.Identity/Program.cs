using Convey;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Identity.Services.Messages.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication();
builder.Services.AddConvey();
// builder.Services.AddJwt();
// builder.Services.AddCommandHandlers();
// builder.Services.AddEventHandlers();
// builder.Services.AddQueryHandlers();
// builder.Services.AddWebApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Welcome to the SwiftParcel Identity service!");

app.MapPost("/sign-in", async (SignIn signIn, ICommandDispatcher commandDispatcher, HttpResponse response) =>
{
    await commandDispatcher.SendAsync(signIn);

});

app.Run();
