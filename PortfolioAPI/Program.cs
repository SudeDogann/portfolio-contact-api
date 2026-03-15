using PortfolioAPI.Services;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Resend configuration
builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = builder.Configuration["re_eEn4QzhE_4VV633i87hpzqVfXXarLiekk"];
});

// Resend HttpClient
builder.Services.AddHttpClient<ResendClient>();

// Mail Service
builder.Services.AddScoped<MailService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://yourfrontenddomain.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Render port
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Swagger (production dahil)
app.UseSwagger();
app.UseSwaggerUI();

// CORS
app.UseCors("AllowReact");

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();