using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Models;
using Newtonsoft.Json;
using obukaApi;
using obukaApi.Application;
using obukaApi.Core;
using Repositories;
using Services;
using Services.Validators;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<BazaContext>();
builder.Services.AddScoped<UsersRepositories>();
builder.Services.AddScoped<UsersServices>();
builder.Services.AddScoped<CreateUserValidator>();
builder.Services.AddScoped<DeleteUserValidator>();
builder.Services.AddTransient<JwtManager>();


var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        object response = null;
        var statusCode = StatusCodes.Status500InternalServerError;
        var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;

        if(exception is EntityNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            response = new
            {
                message = "Resource not found!"
            };
        }

        if (exception is ValidationException validationException)
        {
            statusCode = StatusCodes.Status422UnprocessableEntity;
            response = new
            {
                message = "Faild due to validation errors. Email exist",
                errors = validationException.Errors.Select(x => new
                {
                    x.PropertyName,
                    x.ErrorMessage,

                })
            };
        }

        context.Response.StatusCode = statusCode;

        if (response != null)
        {
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            return;
        }
        await Task.FromResult(context.Response);
    });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
