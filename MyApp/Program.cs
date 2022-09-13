using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyApp.Errors;
using MyApp.Filters;
using MyApp.Middleware;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    //2/3 (vila exception filter attribute) Add Filter Error Handling For All Controllers
   // builder.Services.AddControllers(opt => opt.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();

    //3/3 (custom problem details Factory) Can Custom Problem Details Info and Add New Key Values. copy from aspnetcore original source
    builder.Services.AddSingleton<ProblemDetailsFactory, MyAppProblemDetailsFactory>();
}


var app = builder.Build();
{
    //1/3 (via middlevare) - Error Handling with Middleware
    // app.UseMiddleware<ErrorHandlingMiddleware>();

    //3/3 (via error endpoint) Use ExceptionAddress
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
