using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using NotificationApp.Interfaces;
using NotificationApp.Managers;
using NotificationApp.Services;
using Microsoft.Extensions.Configuration;
using NotificationApp.DTO;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
builder.Services.AddScoped<ISMSNotificationService, SMSNotificationService>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();

builder.Services.AddScoped<NotificationManager>();
builder.Services.AddScoped<EmailNotificationManager>();
builder.Services.AddScoped<SMSNotificationManager>();
builder.Services.AddScoped<PushNotificationManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mailConfigurationSection = builder.Configuration.GetSection("MailConfiguration");
builder.Services.Configure<MailConfiguration>(mailConfigurationSection);

var smsConfigurationSection = builder.Configuration.GetSection("SMSConfiguration");
builder.Services.Configure<SMSConfiguration>(smsConfigurationSection);

// Initialize FirebaseAdmin
var firebaseCredential = GoogleCredential.FromFile("../NotificationApp/Configuration/firebase-credentials.json");
var firebaseApp = FirebaseApp.Create(new AppOptions
{
    Credential = firebaseCredential
});

// Add FCM client as a singleton
builder.Services.AddSingleton(FirebaseMessaging.GetMessaging(firebaseApp));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
