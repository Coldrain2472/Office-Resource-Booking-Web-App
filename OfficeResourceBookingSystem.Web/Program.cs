using OfficeResourceBookingSystem.Repository;
using OfficeResourceBookingSystem.Repository.Implementations.Employee;
using OfficeResourceBookingSystem.Repository.Implementations.Reservation;
using OfficeResourceBookingSystem.Repository.Implementations.Resource;
using OfficeResourceBookingSystem.Repository.Interfaces.Employee;
using OfficeResourceBookingSystem.Repository.Interfaces.Reservation;
using OfficeResourceBookingSystem.Repository.Interfaces.Resource;
using OfficeResourceBookingSystem.Services.Implementations.Authentication;
using OfficeResourceBookingSystem.Services.Implementations.Employee;
using OfficeResourceBookingSystem.Services.Implementations.Reservation;
using OfficeResourceBookingSystem.Services.Implementations.Resource;
using OfficeResourceBookingSystem.Services.Interfaces.Authentication;
using OfficeResourceBookingSystem.Services.Interfaces.Employe;
using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
using OfficeResourceBookingSystem.Services.Interfaces.Resource;

namespace OfficeResourceBookingSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IResourceService, ResourceService>();


            // repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IResourceRepository, ResourceRepository>();

            // connecting to the db
            ConnectionFactory.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            //builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
