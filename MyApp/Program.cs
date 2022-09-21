using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyApp;
using MyApp.Common.Errors;
using MyApp.Common.Mapping;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services
        .AddApplication()
        .AddPresentation()
        .AddInfrastructure(builder.Configuration);

}


var app = builder.Build();
{
    //app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
