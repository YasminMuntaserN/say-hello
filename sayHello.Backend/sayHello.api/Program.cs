using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using sayHello.Business;
using sayHello.Business.Services;
using sayHello.DataAccess;
using sayHello.Entities;
using sayHello.Mappers;
using sayHello.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register FluentValidation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

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
builder.Services.AddAutoMapper(typeof(GroupMappingProfile));
builder.Services.AddAutoMapper(typeof(GroupMemberMappingProfile));



// Register the Services
builder.Services.AddScoped<UniqueValidatorService>();
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
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<UserConnection>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<GroupMemberService>();




// Register SignalR
builder.Services.AddSignalR();
// Register Swagger services
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") 
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

// Map the SignalR Hub to a URL endpoint
app.MapHub<ChatHub>("/chathub");

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});

app.UseAuthorization();
app.MapControllers();

app.Run();
