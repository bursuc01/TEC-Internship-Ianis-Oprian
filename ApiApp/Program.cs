using ApiApp.BusinessLogicLayer.PersonBLL;
using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.Mapper;
using ApiApp.DataAccessLayer.Repositories.PersonRepository;
using Microsoft.EntityFrameworkCore;
using ApiApp.DataAccessLayer.Repositories.SalaryRepository;
using ApiApp.BusinessLogicLayer.SalaryBLL;
using ApiApp.BusinessLogicLayer.DepartmentBLL;
using ApiApp.DataAccessLayer.Repositories.DepartmentRepository;
using ApiApp.BusinessLogicLayer.PersonDetailsBLL;
using ApiApp.DataAccessLayer.Repositories.PersonDetailsRepository;
using ApiApp.BusinessLogicLayer.UserBLL;
using ApiApp.DataAccessLayer.Repositories.UserRepository;
using ApiApp.BusinessLogicLayer.TokenBLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<APIDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration of the token on arrival 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

// Auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Services and Repositories
builder.Services.AddScoped<ISalaryService, SalaryService>();
builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPersonDetailsService, PersonDetailsService>();
builder.Services.AddScoped<IPersonDetailsRepository, PersonDetailsRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
