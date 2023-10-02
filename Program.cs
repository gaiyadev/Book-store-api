using System.Text;
using Amazon.S3;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Middleware;
using BookstoreAPI.Repositories.BookGenre;
using BookstoreAPI.Repositories.CartItem;
using BookstoreAPI.Repositories.Product;
using BookstoreAPI.Repositories.Role;
using BookstoreAPI.Repositories.User;
using BookstoreAPI.Services;
using BookstoreAPI.Services.BookGenre;
using BookstoreAPI.Services.Product;
using BookstoreAPI.Services.role;
using BookstoreAPI.Services.User;
using BookstoreAPI.Templates;
using dotenv.net;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: false));

// Add services to the container.

builder.Services.AddControllers();

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException();
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException();
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_SECRET_ISSUER") ?? throw new InvalidOperationException();
var jwtAudience = Environment.GetEnvironmentVariable("JWT_SECRET_AUDIENCE") ?? throw new InvalidOperationException();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(databaseUrl));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });

    // Add JWT bearer token authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Use "bearer" as the authentication type
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddScoped<IBookGenreRepository, BookGenreRepository>();

builder.Services.AddScoped<IBookGenreService, BookGenreService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();


builder.Services.AddTransient<EmailService>();

builder.Services.AddTransient<EmailNotificationTemplate>();

builder.Services.AddTransient<PasswordService>();

builder.Services.AddTransient<JwtService>();

builder.Services.AddTransient<AuthUserIdExtractor>();

builder.Services.AddTransient<SlugGenerator>();

builder.Services.AddTransient<Awss3Service>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtIssuer,
        ValidAudience =  jwtAudience,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        opt =>
        {
            opt.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services .AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateGenreDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateRoleDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<SignupDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();