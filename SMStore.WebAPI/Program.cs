using SMStore.Data;
using SMStore.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // api leri test ediyor.
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Oturum açar login olan kullanıcı
app.UseAuthorization();

app.MapControllers();

app.Run();
