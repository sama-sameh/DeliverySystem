using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using MyFirstProject.Models.Entities;
using MyFirstProject.Middlewares; 
using Scalar.AspNetCore;
using MyFirstProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// builder.Services.AddControllers().AddOData(options =>
// {
//     options.Select()
//            .Filter()
//            .OrderBy()
//            .Expand()
//            .Count()
//            .SetMaxTop(100)
//            .AddRouteComponents("odata", GetEdmModel());
// });

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = builder.Configuration["AppSettings:Issuer"],
          ValidateAudience = true,
          ValidAudience = builder.Configuration["AppSettings:Audience"],
          ValidateLifetime = true,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
          ValidateIssuerSigningKey = true

      };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();

}

// app.UseMiddleware<TransactionMiddleware>();

app.UseHttpsRedirection();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
static IEdmModel GetEdmModel()
{
    //read my classes
    var builder = new ODataConventionModelBuilder();
    //make endpoint with name Employees
    builder.EntitySet<Customer>("CustomerOdata");

    return builder.GetEdmModel();
}

