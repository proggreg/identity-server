using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddJwtBearer(options => {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters.ValidateAudience = false;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/identity", (ClaimsPrincipal user) => 
        user.Claims.Select(c => new {c.Type, c.Value}))
            .RequireAuthorization();

app.Run();
