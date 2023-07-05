
using Microsoft.EntityFrameworkCore;
using NewsSystem.DAL;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace NewsSystem.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Database

            var connectionString = builder.Configuration.GetConnectionString("NewsConnection") ?? throw new InvalidOperationException("Connection string not found.");
            builder.Services.AddDbContext<SystemContext>(options
                => options.UseSqlServer(connectionString));

            #endregion

            #region Repos

            builder.Services.AddScoped<IGenericRepository<News>, NewsRepository>();
            builder.Services.AddScoped<IGenericRepository<Author>, AuthorsRepository>();

            #endregion

            #region Identity Managers

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<SystemContext>();

            #endregion

            #region Authentication

            builder.Services.AddAuthentication(options =>
            {
                //Used Authentication Scheme
                options.DefaultAuthenticateScheme = "CoolAuthentication";

                //Used Challenge Authentication Scheme
                options.DefaultChallengeScheme = "CoolAuthentication";
            })
                .AddJwtBearer("CoolAuthentication", options =>
                {
                    var secretKeyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
                    var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
                    var secretKey = new SymmetricSecurityKey(secretKeyInBytes);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = secretKey,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}