using ONLINEEXAMINATION.API.Repositorys;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuizzService, QuizzService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IOptionService, OptionService>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuizzRepository, QuizzRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IUserQuizRepository, UserQuizRepository>();
            services.AddScoped<IUserOptionRepository, UserOptionRepository>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void CustomCode(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Riyaz here\n");
                await next();
            });
        }
    }
}
