// using AdminWebAPI.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.Annotations;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         var builder = WebApplication.CreateBuilder(args);

//         // Register services in ConfigureServices

//         // Add services to the container.
//         builder.Services.AddControllers();

//         // Add DbContext
//         builder.Services.AddDbContext<AdminWebAPI.Data.DbContext>(options =>
//             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//         // Add Swagger generation
//         builder.Services.AddEndpointsApiExplorer();
//         builder.Services.AddSwaggerGen(options =>
//         {
//             options.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
//             options.EnableAnnotations();
//         });

//         var app = builder.Build();

//         // Use middleware for Swagger and other configurations
//         if (app.Environment.IsDevelopment())
//         {
//             app.UseDeveloperExceptionPage();  // Shows detailed exception details
//             app.UseSwagger();
//             app.UseSwaggerUI(options =>
//             {
//                 options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
//             });
//         }
//         else
//         {
//             app.UseExceptionHandler("/Home/Error");  // Custom error page in production
//             app.UseHsts();
//         }

//         // Configure the HTTP request pipeline.
//         app.UseHttpsRedirection();

//         // Register controller routes
//         app.MapControllers();

//         // Run the application
//         app.Run();
//     }
// }

// /*using AdminWebAPI.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.Annotations;

// public class Program
// {
//     public static void Main(string[] args)
//     {

// var builder = WebApplication.CreateBuilder(args);

// // Register services in ConfigureServices

// // Add services to the container.
// builder.Services.AddControllers();
        
// // Add DbContext
// builder.Services.AddDbContext<AdminWebAPI.Data.DbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//     // Register DbContext (ensure this matches your actual connection string)
// //var app = builder.Build();---commented

// // Add Swagger generation
// builder.Services.AddEndpointsApiExplorer();
// //builder.Services.AddControllers();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
// });

// builder.Services.AddSwaggerGen(options =>
// {
//     options.EnableAnnotations();
// });

// var app = builder.Build();
// // Use middleware for Swagger and other configurations
// // Use Swagger and Swagger UI
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
//     });
//     //builder.Services.AddSwaggerGen(options => {options.EnableAnnotations();});

//     app.UseDeveloperExceptionPage();  // Shows detailed exception details
// }
// else
// {
//     app.UseExceptionHandler("/Home/Error");  // Custom error page in production
//     app.UseHsts();
// }

// // Configure the HTTP request pipeline.

// app.UseSwagger();

// app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.MapControllers();

// app.Run();        
//     }
// }*/


using AdminWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Add DbContext
        builder.Services.AddDbContext<AdminWebAPI.Data.DbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add CORS policy to allow requests from the Angular frontend
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
            {
                policy.WithOrigins("http://localhost:4200") // Angular app's URL
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // Add Swagger generation
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
            options.EnableAnnotations();
        });

        var app = builder.Build();

        // Use CORS middleware
        app.UseCors("AllowAngularApp"); // Apply the CORS policy

        // Use middleware for Swagger and other configurations
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();  // Shows detailed exception details
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");  // Custom error page in production
            app.UseHsts();
        }

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        // Register controller routes
        app.MapControllers();

        // Run the application
        app.Run();
    }
}
