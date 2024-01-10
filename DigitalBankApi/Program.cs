using DigitalBankApi.Data;
using DigitalBankApi.Dtos;
using DigitalBankApi.DTOs;
using DigitalBankApi.Interfaces.IService;
using DigitalBankApi.Models;
using DigitalBankApi.Services;
using DigitalBankApi.Validation;
using DigitalBankApi.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdminDbContext>(options =>
                                                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(AccountCredits));
builder.Services.AddAutoMapper(typeof(Accounts));
builder.Services.AddAutoMapper(typeof(Credits));
builder.Services.AddAutoMapper(typeof(Customers));
builder.Services.AddAutoMapper(typeof(DepositWithdraws));
builder.Services.AddAutoMapper(typeof(Logins));
builder.Services.AddAutoMapper(typeof(MoneyTransfers));
builder.Services.AddAutoMapper(typeof(Payees));
builder.Services.AddAutoMapper(typeof(SupportRequests));
builder.Services.AddAutoMapper(typeof(Users));

builder.Services.AddControllers();

builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<CreditService>();
builder.Services.AddTransient<CustomerService>();
builder.Services.AddTransient<DepositWithdrawService>();
builder.Services.AddTransient<MoneyTransferService>();
builder.Services.AddTransient<PayeeService>();
builder.Services.AddTransient<SupportRequestService>();

builder.Services.AddSingleton<IValidator<AccountDto>, AccountDtoValidator>();
builder.Services.AddSingleton<IValidator<AnswerRequestDto>, AnswerRequestDtoVAlidator>();
builder.Services.AddSingleton<IValidator<UpdateBalanceDto>, UpdateBalanceDtoValidator>();
builder.Services.AddSingleton<IValidator<UpdatePasswordDto>, UpdatePasswordDtoValidator>();
builder.Services.AddSingleton<IValidator<UpdateRoleDto>, UpdateRoleDtoValidator>();
builder.Services.AddSingleton<IValidator<CreditDto>, CreditDtoValidator>();
builder.Services.AddSingleton<IValidator<CustomerDto>, CustomerDtoValidator>();
builder.Services.AddSingleton<IValidator<DepositWithdrawDto>, DepositWithdrawDtoValidator>();
builder.Services.AddSingleton<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddSingleton<IValidator<MoneyTransferDto>, MoneyTransferDtoValidator>();
builder.Services.AddSingleton<IValidator<PayeeDto>, PayeeDtoValidator>();
builder.Services.AddSingleton<IValidator<SupportRequestDto>, SupportRequestDtoValidator>();
builder.Services.AddSingleton<IValidator<UserDto>, UserDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));


builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
