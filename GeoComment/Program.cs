using GeoComment.Data;
using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DatabaseHandler>();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<GeoCommentDbContext>();

builder.Services.AddDbContext<GeoCommentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddIdentityCore<User>();

builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(0,1);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ApiVersionReader =
        new QueryStringApiVersionReader("api-version");
    
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider
        .GetRequiredService<DatabaseHandler>();

    await database.CreateDB();
}

app.Run();
