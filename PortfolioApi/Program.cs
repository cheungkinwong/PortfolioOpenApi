using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Logging;
IdentityModelEventSource.ShowPII = true;
IdentityModelEventSource.LogCompleteSecurityArtifact = true;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenHandlers.Clear();
    options.TokenHandlers.Add(new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler());

    Console.WriteLine("[DEBUG] JWT Bearer middleware configured");
    Console.WriteLine($"[DEBUG] Issuer: {config["Jwt:Issuer"]}");
    Console.WriteLine($"[DEBUG] Audience: {config["Jwt:Audience"]}");
    Console.WriteLine($"[DEBUG] Key length: {config["Jwt:Key"]?.Length ?? 0}");



    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
        ClockSkew = TimeSpan.FromMinutes(5),
        RequireExpirationTime = false,
        ValidateActor = false
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    // Use legacy handler directly for validation
                    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                    };

                    SecurityToken validatedToken;
                    var principal = handler.ValidateToken(token, validationParameters, out validatedToken);

                    // Bypass the middleware entirely and set user manually
                    context.Principal = principal;
                    context.Success();
                    Console.WriteLine("[DEBUG] Manual validation successful!");
                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DEBUG] Manual validation failed: {ex.Message}");
                }
                context.Token = token;
            }

            // If manual validation fails, let the middleware try
   
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"[ERROR] Authentication failed: {context.Exception.Message}");
            Console.WriteLine($"[ERROR] Exception type: {context.Exception.GetType().Name}");
            Console.WriteLine($"[ERROR] Stack trace: {context.Exception.StackTrace}");

            // Log the token that caused the failure
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (authHeader.StartsWith("Bearer "))
                {
                    var failedToken = authHeader.Substring("Bearer ".Length).Trim();
                    Console.WriteLine($"[ERROR] Failed token: '{failedToken}'");
                    Console.WriteLine($"[ERROR] Failed token length: {failedToken.Length}");
                }
            }

            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token successfully validated.");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("Authentication challenge triggered.");
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"You must be authenticated to access this resource.\"}");
        },
        OnForbidden = context =>
        {
            Console.WriteLine("Access forbidden for this token.");
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"You are not authorized to access this resource.\"}");
        }
    };
});



string Base64UrlDecode(string input)
{
    int paddingLength = input.Length % 4;
    string padded = paddingLength switch
    {
        2 => input + "==",
        3 => input + "=",
        _ => input
    };

    padded = padded.Replace('-', '+').Replace('_', '/');
    var bytes = Convert.FromBase64String(padded);
    return Encoding.UTF8.GetString(bytes);
}

builder.Services.AddAuthorization();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "http://cheungkinportfolioreactclient.runasp.net",
            "http://localhost:5273"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API V1");
    c.RoutePrefix = string.Empty;
});

app.UseCors(MyAllowSpecificOrigins);
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("RequestLogger");
    logger.LogInformation("Incoming {Method} request to {Path}", context.Request.Method, context.Request.Path);
    await next.Invoke();
});


app.Run();
public partial class Program { }
