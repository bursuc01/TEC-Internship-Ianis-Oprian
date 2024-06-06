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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<APIDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
