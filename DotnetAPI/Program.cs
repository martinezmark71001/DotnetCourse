using System.Text;
using DotnetAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevCors", (corsBuilder) =>
        {
            corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    options.AddPolicy("ProdCors", (corsBuilder) =>
        {
            corsBuilder.WithOrigins("https://myProductionSite.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});
    
// Start for token
// This is for token validation
//
string? TokenKeyString = builder.Configuration.GetSection("AppSettings:TokenKey").Value;
           
SymmetricSecurityKey  tokenKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes( 
        TokenKeyString != null ? TokenKeyString: ""
    )
);
TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
{
    IssuerSigningKey = tokenKey,
    ValidateIssuer = false, 
    ValidateIssuerSigningKey = false,
    ValidateAudience = false
};
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });
// this is the end of token validation 

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

