using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using sayHello.Business;
using sayHello.DataAccess;
using sayHello.Mappers;
using sayHello.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .LogTo(Console.Write, Microsoft.Extensions.Logging.LogLevel.Information));



// Register AutoMapper and Validators
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(MessageMappingProfile));
builder.Services.AddAutoMapper(typeof(MediaMappingProfile));
builder.Services.AddAutoMapper(typeof(BlockedUserMappingProfile));
builder.Services.AddAutoMapper(typeof(ArchivedUserMappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MessageValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MediaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BlockedUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ArchivedUserValidator>();


// Register the Services and Validator
builder.Services.AddScoped<UserService>();   
builder.Services.AddScoped<UserValidator>(); 
builder.Services.AddScoped<MessageService>();   
builder.Services.AddScoped<MessageValidator>(); 
builder.Services.AddScoped<MediaService>();   
builder.Services.AddScoped<MediaValidator>(); 
builder.Services.AddScoped<BlockedUserService>();   
builder.Services.AddScoped<BlockedUserValidator>(); 
builder.Services.AddScoped<ArchivedUserService>();   
builder.Services.AddScoped<ArchivedUserValidator>(); 
// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();

app.Run();