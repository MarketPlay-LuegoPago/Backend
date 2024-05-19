using System.Text; //Este hace parte del token
using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer; //Esta es la libreria de Jwt
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//Agregamos los servicios de Jwt
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
    options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer ="https://localhost:5205", //Validacion de usuario para el token
        ValidAudience = "https://localhost:5205", //Validacion de los que generan el token o la audiencia
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ncjdncjvurbuedxwn233nnedxee+dfr-")) //Clave maestra para generar token
    };
   });

builder.Services.AddDbContext<BaseContext>(options =>
                            options.UseMySql(
                            builder.Configuration.GetConnectionString("MySqlConnection"),
                            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//Añadimos el cors MTO
builder.Services.AddCors( options =>{
    options.AddPolicy("AllowAnyOrigin",
    builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

// Register the repository
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();
app.UseCors("AllowAnyOrigin"); //Usamos el Cors MTO
app.MapControllers(); // Este tambien se comparte con el token

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Este se comparte con la libreria del token
//Agregamos Autenticaciones para el token
app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
