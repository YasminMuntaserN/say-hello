using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using sayHello.api;
using sayHello.api.Authorization;
using sayHello.Business;
using sayHello.Business.Services;
using sayHello.DataAccess;
using sayHello.Entities;
using sayHello.Mappers;
using sayHello.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .LogTo(Console.Write, Microsoft.Extensions.Logging.LogLevel.Information));



// Register FluentValidation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());



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
builder.Services.AddScoped<AuthService>();
// Add Swagger configuration
builder.Services.AddSwaggerConfig();

// add jwt
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication().
    AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SignKey))
    };
});

// Add policies for each permission to make the Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Permissions.View.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.View)));
    options.AddPolicy(Permissions.ManageUsers.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.ManageUsers)));
    options.AddPolicy(Permissions.SendMessages.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.SendMessages)));
    options.AddPolicy(Permissions.ManageGroups.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.ManageGroups)));
    options.AddPolicy(Permissions.BlockUsers.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.BlockUsers)));
    options.AddPolicy(Permissions.ArchiveChats.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.ArchiveChats)));
    options.AddPolicy(Permissions.AddGroupMember.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.AddGroupMember)));
    options.AddPolicy(Permissions.RemoveGroupMember.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.RemoveGroupMember)));
    options.AddPolicy(Permissions.ViewChats.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.ViewChats)));
    options.AddPolicy(Permissions.AuthenticateUsers.ToString(), 
        policy => policy.Requirements.Add(new PermissionRequirement(Permissions.AuthenticateUsers)));
});


builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

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
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
