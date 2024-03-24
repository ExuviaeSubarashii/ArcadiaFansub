using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Services.Services.AnimeServices;
using ArcadiaFansub.Services.Services.CommentServices;
using ArcadiaFansub.Services.Services.EpisodeServices;
using ArcadiaFansub.Services.Services.MemberServices;
using ArcadiaFansub.Services.Services.NotificationServices;
using ArcadiaFansub.Services.Services.TicketServices;
using ArcadiaFansub.Services.Services.UserServices;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<ArcadiaFansubContext>();
builder.Services.AddDbContext<ArcadiaFansubContext>();
builder.Services.AddScoped<AnimeHandler>();
builder.Services.AddScoped<EpisodeHandler>();
builder.Services.AddScoped<UserHandler>();
builder.Services.AddScoped<UserAuthentication>();
builder.Services.AddScoped<TicketHandler>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<CommentHandler>();
builder.Services.AddScoped<MemberHandler>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("fixed", httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(10)
        }));
});



var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseRateLimiter();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
