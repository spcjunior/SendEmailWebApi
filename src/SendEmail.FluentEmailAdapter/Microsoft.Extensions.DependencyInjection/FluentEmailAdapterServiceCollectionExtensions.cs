using FluentEmail.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SendEmail.Domain.Interfaces;
using SendEmail.Domain.Models.Emails;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;

namespace SendEmail.FluentEmailAdapter.Microsoft.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class FluentEmailAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentEmailAdapter(this IServiceCollection services, FluentEmailAdapterConfiguration adapterConfiguration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (adapterConfiguration == null) throw new ArgumentNullException(nameof(adapterConfiguration));

            //create a google account to test the send smtp
            if (string.IsNullOrEmpty(adapterConfiguration.UserName) || string.IsNullOrEmpty(adapterConfiguration.PassWord))
                throw new AccessViolationException("Set UserName and  PassWord to SMTP.");

            services.AddSingleton(adapterConfiguration);
            services
                .AddFluentEmail("email-noreply@gmail.com")
                .AddRazorRenderer()
                .AddCustomSmtpSender(adapterConfiguration);

            services.AddScoped<IFluentEmailAdapter<Communication>, FluentEmailAdapter>();
            services.AddScoped<IFluentEmailAdapter<Wellcome>, FluentEmailAdapter>();

            return services;
        }

        public static FluentEmailServicesBuilder AddCustomSmtpSender(this FluentEmailServicesBuilder builder, FluentEmailAdapterConfiguration adapterConfiguration)
        {
            builder.Services.AddTransient<ISender>(s => new CustomSmtpSender(() => new SmtpClient()
            {
                EnableSsl = adapterConfiguration.EnableSsl,
                UseDefaultCredentials = !adapterConfiguration.IsAuthenticated,
                Credentials = adapterConfiguration.IsAuthenticated ? new NetworkCredential(adapterConfiguration.UserName, adapterConfiguration.PassWord) : null,
                Host = adapterConfiguration.Host,
                Port = Int32.Parse(adapterConfiguration.Port),
                DeliveryMethod = SmtpDeliveryMethod.Network
            }));
            return builder;
        }
    }
}
