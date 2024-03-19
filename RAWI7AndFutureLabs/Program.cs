using RAWI7AndFutureLabs.Services.Comment;
using RAWI7AndFutureLabs.Services.Post;
using RAWI7AndFutureLabs.Services.User;
using RAWI7AndFutureLabs.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" });
});

/*AddScoped обрано тому, що він створює новий екземпляр сервісу для кожної області видимості (кожного HTTP-запиту),
  що підходить для веб-додатків де кожен запит мав свій власний екземпляр*/
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IPostsService, PostsService>();
//AddTransient обрано тому, що він створює новий екземпляр сервісу кожного разу, коли його запитують
builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
