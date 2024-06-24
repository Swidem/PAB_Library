using Microsoft.EntityFrameworkCore;
using Kiosk.WebAPI.Persistance;
using Kiosk.WebAPI.Db.Persistance;
using Kiosk.WebAPI.Db.Middleware;
using Kiosk.WebAPI.Db.Services;
using FluentValidation;
using Kiosk.WebAPI.Db.Dto.Validators;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Dto;
using FluentValidation.AspNetCore;
using NLog.Web;
using NLog;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Rejestracja kontekstu bazy w kontenerze IoC
    var sqliteConnectionString = "Data Source=Kiosk.WebAPI.db";
    builder.Services.AddDbContext<LibraryDbContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    // Rejestracja jednostki pracy w kontenerze IoC
    builder.Services.AddScoped<ILibraryUnitOfWork, LibraryUnitOfWork>();

    // Rejestracja repozytoriów w kontenerze IoC
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<ILoanRepository, LoanRepository>();

    // Rejestracja seeder w kontenerze IoC
    builder.Services.AddTransient<DataSeeder>();

    // Rejestracja serwisów w kontenerze IoC
    builder.Services.AddScoped<IBookService, BookService>();
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<ILoanService, LoanService>();

    // Rejestracja exception middleware w kontenerze IoC
    builder.Services.AddScoped<ExceptionMiddleware>();

    // Rejestracja walidatorów
    builder.Services.AddScoped<IValidator<CreateBookDto>, RegisterCreateBookDtoValidator>();
    builder.Services.AddScoped<IValidator<CreateClientDto>, RegisterCreateClientDtoValidator>();
    builder.Services.AddScoped<IValidator<CreateEmployeeDto>, RegisterCreateEmployeeDtoValidator>();
    builder.Services.AddScoped<IValidator<CreateLoanDto>, RegisterCreateLoanDtoValidator>();

    // Rejestracja automatycznej walidacji
    builder.Services.AddFluentValidationAutoValidation();

    // Rejestracja automappera w kontenerze IoC
    builder.Services.AddAutoMapper(typeof(Program));

    // Rejestracja FileService w kontenerze IoC
    builder.Services.AddScoped<IFileService, FileService>();

    // Dodaj us³ugi dla kontrolerów
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // W³¹czenie obs³ugi plików statycznych
    app.UseStaticFiles();

    // Seeduj dane
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        seeder.Seed();
    }

    // Konfiguracja middleware
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseMiddleware<ExceptionMiddleware>();
    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}