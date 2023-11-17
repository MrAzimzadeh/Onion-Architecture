using System.Text;
using Ecomerce.Application.ServiceRegistrations;
using Ecomerce.Application.Validators.Products;
using Ecomerce.Infrastructure.Filters;
using Ecomerce.Infrastructure.ServiceRegistration;
using Ecomerce.Infrastructure.Services.Storage.Azure;
using Ecomerce.Infrastructure.Services.Storage.Local;
using Ecommerce.Persistence.ServiceRegistrations;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureService();
builder.Services.AddApplicationService();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Ecommerce", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddStorage<AzureStorage>();
// JWT Bearer

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme); // bu default olaraq 401 error verir
/*
 * bu Appe token uzerinden bir istek gelirse  bu tokeni yoxlayan da yeni dogrulayan zaman onun JWT oldugunu bidir
 *  ve asagidaki parametrlere gore dogrulama edir
 */
builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme) //
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true, // Audience  yoxlayir yeni istek gonderenin  kim oldugunu yoxlayir 
            ValidateIssuer = true, // Issuer  yoxlayir yeni istek gonderenin  kim oldugunu yoxlayir
            ValidateLifetime = true, // Yaratdigimiz tokenin omrunu yoxlayir
            ValidateIssuerSigningKey = true, // yaradilcaq token deyerinin app e aid bir deyer olub olmadigini yoxlayir 
            ValidAudience = builder.Configuration["Token:Audience"], // istek gonderenin  kim oldugunu yoxlayir
            ValidIssuer = builder.Configuration["Token:Issuer"], // istek gonderenin  kim oldugunu yoxlayir
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        };
    });

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

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("Ecommerce");
app.MapControllers();

app.Run();