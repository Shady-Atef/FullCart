
using Application.Middleware;
using Domain.Entities.UserAggregate;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Reposatories;
using Infrastructure.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<FullCartContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqloption =>
    {
        sqloption.EnableRetryOnFailure();
        sqloption.MigrationsAssembly("Infrastructure");

    });

});
builder.Services.AddScoped<UOW>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Portal", Version = "v1" });
    #region JWT Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

    #endregion
});

builder.Services.AddCors(options =>
{
    string origins = builder.Configuration.GetSection("AllowedCors").Value;
    if (string.IsNullOrEmpty(origins) || origins == "*")
    {
        options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                   .AllowAnyHeader());
    }
    else
    {
        options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                                                                .WithOrigins(origins?.Split(',', StringSplitOptions.RemoveEmptyEntries))
                                                                );
    }
});

#region Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<long>>(
    opt =>
    {
        opt.Lockout.AllowedForNewUsers = true;
        opt.Lockout.MaxFailedAccessAttempts = builder.Configuration.GetValue<int>("Auth:MaxFailedAccessAttemptsBeforeLockout");
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetValue<double>("Auth:DefaultAccountLockoutTimeSpan"));
        opt.User.AllowedUserNameCharacters = null;
        opt.Password.RequireNonAlphanumeric = false;

    })
        .AddEntityFrameworkStores<FullCartContext>()
        .AddDefaultTokenProviders();

#endregion

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


builder.Services.AddAutoMapper(
    Assembly.Load("Application"),
    Assembly.Load("FullCart.Server"));

builder.Services.AddControllers(
    ).AddNewtonsoftJson(options =>
           options.SerializerSettings.ContractResolver =
              new CamelCasePropertyNamesContractResolver());

builder.Services.AddScoped<IUow, UOW>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Portal v1"));
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //context.Database.Migrate();

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
//app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();
