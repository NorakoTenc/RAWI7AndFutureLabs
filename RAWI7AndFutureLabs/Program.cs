using RAWI7AndFutureLabs.Services.Comment;
using RAWI7AndFutureLabs.Services.Post;
using RAWI7AndFutureLabs.Services.User;
using RAWI7AndFutureLabs.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using RAWI7AndFutureLabs.Services.Data;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services.AUsers;
using RAWI7AndFutureLabs.Services.Auth;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using RAWI7AndFutureLabs.Services.HealthCheck;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

await mSerilog.SerilogTaskAsync();

builder.Services.AddHealthChecks()
    .AddCheck("1", new CustomService1HealthCheck())
    .AddCheck("2", new CustomService2HealthCheck());
builder.Services.Configure<HealthCheckOptions>(options =>
{
    options.ResponseWriter = WriteResponse;
});

async static Task WriteResponse(HttpContext httpContext, HealthReport result)
{
    httpContext.Response.ContentType = "application/json";

    var healthCheckResult = new
    {
        status = result.Status.ToString(),
        results = result.Entries.Select(pair => new
        {
            name = pair.Key,
            status = pair.Value.Status.ToString(),
            description = pair.Value.Description,
            data = pair.Value.Data
        })
    };

    var responseJson = JsonSerializer.Serialize(healthCheckResult, new JsonSerializerOptions
    {
        WriteIndented = true
    });

    await httpContext.Response.WriteAsync(responseJson);
}


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RAWI7AndFutureLabs", Version = "v1" });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID" },
                    { "profile", "Profile" }
                }
            }
        }
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    };
    c.AddSecurityRequirement(securityRequirement);
});


builder.Services.AddScoped<IDataService, DataService>();
/*AddScoped обрано тому, що він створює новий екземпляр сервісу для кожної області видимості (кожного HTTP-запиту),
  що підходить для веб-додатків де кожен запит мав свій власний екземпляр*/
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IPostsService, PostsService>();
//AddTransient обрано тому, що він створює новий екземпляр сервісу кожного разу, коли його запитують
builder.Services.AddTransient<IUsersService, UsersService>();

builder.Services.AddScoped<IAUsersService, AUsersService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });
builder.Host.UseSerilog();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RAWI7AndFutureLabs v1");
        c.SwaggerEndpoint("/health/swagger", "Health Checks");
        c.OAuthClientId("swagger");
        c.OAuthAppName("Swagger UI");
    });
}
app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
    claims: claims,
    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.MapControllers();

app.Run();
