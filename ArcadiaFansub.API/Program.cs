using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Services.Services.AnimeServices;
using ArcadiaFansub.Services.Services.CommentServices;
using ArcadiaFansub.Services.Services.EpisodeServices;
using ArcadiaFansub.Services.Services.NotificationServices;
using ArcadiaFansub.Services.Services.TicketServices;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.AspNetCore.RateLimiting;
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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "fixed", options =>
{
    options.PermitLimit = 10;
    options.Window = TimeSpan.FromSeconds(12);
    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    options.QueueLimit = 2;
}));


var app = builder.Build();
app.UseRateLimiter();
// Configure the HTTP request pipeline.
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
