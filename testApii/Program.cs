using Microsoft.EntityFrameworkCore;
using testApii.DAL;
using testApii.DAL.Interfaces;
using testApii.DAL.Repositories;
using testApii.Auth.Authorization;
using testApii.Auth.Helper;
using testApii.DAL.Concreate.Interfaces;
using testApii.DAL.Concreate.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors();

//Context
builder.Services.AddDbContext<TestDbContext>(optionsBuilder =>
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TestDbContext).Assembly.FullName)));

// configure automapper with all automapper profiles from this assembly
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Add services to the container.
builder.Services.AddHttpClient();
#region Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IInjectionUnitRepository, InjectionUnitRepository>();
builder.Services.AddScoped<ISantralRepository, SantralRepository>();
#endregion

builder.Services.AddScoped<DataGenerator>();
builder.Services.AddScoped<IHelpers, Helpers>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

#region Cors
app.UseCors(options => options
.WithOrigins(new[] { "http://localhost:3000" })
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader());
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();


// Otomatik veritabanýna verileri ekleme
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DataGenerator>();
    await dbInitializer.Initialize();
}

app.Run();