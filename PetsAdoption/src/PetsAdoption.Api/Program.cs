using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PetsAdoption.Api.Mapping;
using PetsAdoption.Api.Services;
using PetsAdoption.Api.Services.Abstractions;
using PetsAdoption.Api.Settings;
using PetsAdoption.Infrastructure.Contexts;
using PetsAdoption.Infrastructure.Repositories;
using PetsAdoption.Infrastructure.Repositories.Abstractions;
using PetsAdoption.Infrastructure.Settings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();
builder.Services.AddScoped<IPicturesRepository, PicturesRepository>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(PetsAdoptionProfile).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PetsAdoptionDBContext>(options => 
        options.UseSqlServer(connectionString, optSqLite => 
            optSqLite.MigrationsAssembly(typeof(PetsAdoptionDBContext).Assembly.GetName().Name)).EnableSensitiveDataLogging());

builder.Services.Configure<FeatureFlags>(builder.Configuration.GetSection(key: nameof(FeatureFlags)));
builder.Services.Configure<EncryptionSettings>(builder.Configuration.GetSection(key: nameof(EncryptionSettings)));
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(key: nameof(TokenSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
