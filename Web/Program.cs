var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Database Configuration
builder.Services.AddDatabaseContext(builder.Configuration);

// Application Services
builder.Services.AddApplicationServices(builder.Configuration);

// FluentValidation Configuration
builder.Services.AddFluentValidationConfiguration();
builder.Services.AddFluentValidationAutoValidation();

// NToastNotify Configuration
builder.Services.AddNToastNotifyConfiguration();

// Identity Configuration
builder.Services.AddIdentityConfiguration();

// Build the application
var app = builder.Build();

// Seed Database
await app.SeedDatabaseAsync();

// Configure Middleware
app.ConfigureMiddleware();

// Configure Routes
app.ConfigureRoutes();

app.Run();