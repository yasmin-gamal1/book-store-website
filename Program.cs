using BookStore_API.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using BookStoreAPI.Models;

namespace BookStore_API
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
            builder.Services.AddSwaggerGen(op => op.EnableAnnotations());
            builder.Services.AddDbContext<BookStoreContext>(op =>
                op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("bookcon")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                op.SaveToken = true;
                #region Secret Key
                string key = "welcome to my secret key Yasmin Gamal";
                var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                #endregion
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = secretKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddScoped<UnitOFWork>();

            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://example.com", "http://anotherdomain.com") // Replace with allowed origins
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // Apply CORS policy
            app.UseCors("AllowAll"); // Use the policy defined above

            app.UseAuthentication(); // Ensure authentication middleware is before authorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
