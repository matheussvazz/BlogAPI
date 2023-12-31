using Blog.Data;
using Blog.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder   // Configura o comportamento da API
.Services
.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
   options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
builder.Services.AddDbContext<BlogDataContext>();
builder.Services.AddTransient<TokenService>();



var app = builder.Build();

app.MapControllers();

app.Run();
