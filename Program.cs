using BikeShopDSD605.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

//Allow CORS Global
var CORSAllowSpecificOrigins = "CORSAllowed";
var builder = WebApplication.CreateBuilder(args);


//Add CORS to the Project
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORSAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://www.contoso.com");
    });
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    //options.UseInMemoryDatabase("InMemoryStockAPITest");
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = false;
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Adding Policies
builder.Services.AddAuthorization(options =>
{
    // Joining date 6 months ago
    options.AddPolicy("ViewRolePolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
        if (DateTime.TryParse(joiningDateClaim, out var joiningDate))
        {
            var hasViewClaimsClaim = context.User.HasClaim("Permission", "View Claims");
            var hasViewRolesClaim = context.User.HasClaim("Permission", "View Roles");
            return (hasViewClaimsClaim || hasViewRolesClaim) && joiningDate > DateTime.MinValue && joiningDate < DateTime.Now.AddMonths(-6);
        }
        return false;
    }));

    // Delete Stock
    options.AddPolicy("DeleteStockPolicy", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();  // Require the user to be authenticated
        policyBuilder.RequireClaim("Permission", "Delete Stock"); // Require the "Permission" claim with the value "Delete Stock"
    });

    // Edit Stock
    options.AddPolicy("EditStockPolicy", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();  // Require the user to be authenticated
        policyBuilder.RequireClaim("Permission", "Edit Stock"); // Require the "Permission" claim with the value "Edit Stock"
    });

    // Add Stock if over 18 years
    options.AddPolicy("CreateStockOver18Policy", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();  // Require the user to be authenticated
        policyBuilder.RequireAssertion(context =>
        {
            var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth)?.Value;
            if (DateTime.TryParse(dateOfBirthClaim, out var dateOfBirth))
            {
                var currentDate = DateTime.Now;
                var minimumAge = 18;
                return (currentDate.Year - dateOfBirth.Year) >= minimumAge;
            }
            return false;
        });
    });

    // View Claim Policy
    options.AddPolicy("ViewClaimPolicy", policyBuilder => policyBuilder.RequireClaim("Permission", "View Claims"));
    // View Claim Policy
    options.AddPolicy("ViewRolePolicy", policyBuilder => policyBuilder.RequireClaim("Permission", "View Roles"));
});

builder.Services.AddRazorPages(options =>
{
    // Authorize for more than 6 months at the company
    options.Conventions.AuthorizeFolder("/RoleManager", "ViewRolePolicy");
    options.Conventions.AuthorizeFolder("/ClaimsManager", "ViewClaimPolicy");

    // Delete Stock
    options.Conventions.AuthorizeFolder("/Stock/Delete", "DeleteStockPolicy");
    // Edit Stock
    options.Conventions.AuthorizeFolder("/Stock/Edit", "EditStockPolicy");
    // Add Stock
    options.Conventions.AuthorizeFolder("/Stock/Create", "CreateStockOver18Policy");
});

builder.Services.AddSwaggerGen();

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
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Allow CORS
app.UseCors(CORSAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();

public partial class Program { }