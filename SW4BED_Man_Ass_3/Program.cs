using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.hub;

internal class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
		                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));
		builder.Services.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
			.AddEntityFrameworkStores<ApplicationDbContext>();

		builder.Services.AddRazorPages();

		builder.Services.AddSignalR();


		builder.Services.AddAuthorization(options =>
		{
			options.AddPolicy("Reception",
				policyBuilder => policyBuilder.RequireClaim("Reception")
			);
			options.AddPolicy("Cook",
				policyBuilder => policyBuilder.RequireClaim("Cook"));

			options.AddPolicy("Waiter",
				policyBuilder => policyBuilder.RequireClaim("Waiter")
			);
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();
		app.MapHub<HubKitchen>("/HubKitchen");
		app.MapRazorPages();

		using (var scope = app.Services.CreateScope())
		{
			var serviceProvider=scope.ServiceProvider;
			var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
			if (userManager != null)
			{
				Program.SeedUsers(userManager);
			}
			else throw new Exception("Unable to get User");
		}

		app.Run();
	}

	public static void SeedUsers(UserManager<IdentityUser> userManager)
	{
		const string receptionEmail = "reception@mail.com";
		const string waiterEmail = "waiter@mail.com";
		const string password = "Kode.123";


		if (userManager==null)
		{
			throw new ArgumentNullException(nameof(userManager));
		}

		if (userManager.FindByNameAsync(receptionEmail).Result==null)
		{
			var receptionist = new IdentityUser();
			receptionist.UserName = receptionEmail;
			receptionist.Email=receptionEmail;
			receptionist.EmailConfirmed = true;
			IdentityResult result = userManager.CreateAsync(receptionist, password).Result;
			if (result.Succeeded)
			{
				var receptionUser = userManager.FindByNameAsync(receptionEmail).Result;
				var claim = new Claim("Reception", "true");
				var claimAdded = userManager.AddClaimAsync(receptionUser, claim).Result;
			}
		}
		if(userManager.FindByNameAsync(waiterEmail).Result == null)
		{
			var waiter = new IdentityUser();
			waiter.UserName = waiterEmail;
			waiter.Email = waiterEmail;
			waiter.EmailConfirmed = true;
			IdentityResult result = userManager.CreateAsync(waiter, password).Result;
			if (result.Succeeded)
			{
				var waiterUser = userManager.FindByNameAsync(waiterEmail).Result;
				var claim = new Claim("Waiter", "true");
				var claimAdded = userManager.AddClaimAsync(waiterUser, claim).Result;
			}
		}
	}

}