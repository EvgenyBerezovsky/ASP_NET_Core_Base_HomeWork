using Lesson008.Models;
using Lesson008.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddOptions<JwtConfigurationOptions>()
    .BindConfiguration("JwtConfiguration");

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Input JWT in format: Bearer {token}",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var key = builder.Configuration.GetValue<string>("JwtConfiguration:SecretKey");
var issuer = builder.Configuration.GetValue<string>("JwtConfiguration:Issuer");
var audience = builder.Configuration.GetValue<string>("JwtConfiguration:Audience");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"Token validation failed: {context.AuthenticateFailure?.Message}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            (context.User.IsInRole("Admin") && context.User.HasClaim("Department", "IT")) ||
            (context.User.IsInRole("Manager") && context.User.HasClaim("Department", "HumanResources")));
    });

    options.AddPolicy("UserPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("FinanceOnlyPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            (context.User.HasClaim("Department", "Finance")) ||
            (context.User.IsInRole("Manager") && context.User.HasClaim("Department", "HumanResources")));
    });

    options.AddPolicy("HRManagerOnlyPolisy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == "Department" && c.Value == "HumanResources"));
        policy.RequireRole("Manager");
    });

    options.AddPolicy("FlexibleAccess", policy =>
    policy.RequireAssertion(context =>

        (context.User.IsInRole("Admin") && context.User.HasClaim("Department", "IT")) ||
        (context.User.IsInRole("Manager") && context.User.HasClaim("Department", "HumanResources")))
    );
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
