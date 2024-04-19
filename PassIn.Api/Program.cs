using FluentValidation;
using PassIn.Api.Filters;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.Validators;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped<IAttendeeRepository, AttendeeRepository>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();
builder.Services.AddScoped<IEventRepository, EventsRepository>();

builder.Services.AddScoped<AbstractValidator<Attendee>, AttendeeValidator>();
builder.Services.AddScoped<AbstractValidator<Event>, EventValidator>();

builder.Services.AddScoped<RegisterEventUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
