using Eccomerce.Infrastructure.Filters;
using Ecomerce.Application.Validators.Products;
using Ecommerce.Persistence.ServiceRegistrations;
using FluentValidation.AspNetCore;
using FluentValidation.Results;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    // Fluentvalidation  Confugrations 
    .AddFluentValidation(
        configuration =>
        {
            // Bunu edende sadece bir classi yazmaqla diger hamisini refleksnlarla tapir ve validasiya edir
            // Deprceted 
            configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
        })
    .ConfigureApiBehaviorOptions(options =>
    {
        /*
         * BU method  Asp.net core da  gelen Hazir validasyalari deaktiv  edir
         * Yeni movcud olaninin xaricinde menim yazdigim validasyalari isled
         * (If (ModelState.Isvalid) e catmadan error verirdi bunu yazaraq bu meseleni hell edirik )
         *
         */
        options.SuppressModelStateInvalidFilter = true;
    })
    ;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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