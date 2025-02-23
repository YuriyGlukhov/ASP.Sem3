using ASP.Seminar3.Abstraction;
using ASP.Seminar3.Mapper;
using ASP.Seminar3.Mutatin;
using ASP.Seminar3.Query;
using ASP.Seminar3.Services;
using Microsoft.EntityFrameworkCore;

namespace ASP.Seminar3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IStorageServices, StorageServices>();


            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()
                .AddMutationType<MySimpleMutation>();
                

            var app = builder.Build();

            app.MapGraphQL();

            app.Run();
        }
    }
}
