using CHLeMudra.Api.Repository;
using Fairgaze.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CHLeMudra.API
{
    public class DependencyResolver
    {
        public static void AddDependency(IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CHLeMudraContext>(options => options.UseSqlServer(sqlConnectionString));
            services.AddTransient<IApiHelper, ApiHelper>();
            services.AddTransient<IDocumentSignHistoryRepository, DocumentSignHistoryRepository>();
            services.AddTransient<IDocumentAssigneeRepository, DocumentAssigneeRepository>();


        }

    }
}
