using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OnlineStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DbOnlineStore>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbOnlineStore")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://books-store12.netlify.app")
                     .WithMethods("POST", "GET", "PUT", "DELETE")
                     .AllowAnyHeader(); 
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
