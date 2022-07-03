using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SendEmail.Domain.Interfaces;
using SendEmail.Domain.Models.Emails;
using SendEmail.FluentEmailAdapter.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SendEmail.FluentEmailAdapter
{
    public class FluentEmailAdapter :
        IFluentEmailAdapter<Communication>,
        IFluentEmailAdapter<Wellcome>
    {
        private IFluentEmailFactory _fluentEmailFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FluentEmailAdapter> _logger;

        public FluentEmailAdapter(IFluentEmailFactory fluentEmailFactory, IServiceProvider serviceProvider, ILogger<FluentEmailAdapter> logger)
        {
            _fluentEmailFactory = fluentEmailFactory ?? throw new ArgumentNullException(nameof(fluentEmailFactory));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendEmail(Communication email)
        {
            try
            {
                var template = Resources.email_communication_base
                    .Replace("#d31245", email.ConfigPrimaryColor);

                var emailToSend = _fluentEmailFactory
                    .Create()
                    .SetFrom(email.Sender)
                    .To(email.Recipient)
                    .Subject(email.Subject)
                    .UsingTemplate(template, email)
                    .Attach(new FluentEmail.Core.Models.Attachment()
                    {
                        ContentId = "img_banner_company",
                        ContentType = "image/png",
                        Filename = "img_banner_company.png",
                        IsInline = true,
                        Data = new MemoryStream(email.ConfigBannerEmail)
                    })
                    .Attach(new FluentEmail.Core.Models.Attachment()
                    {
                        ContentType = "application/pdf",
                        Filename = $"{nameof(Resources.FluentEmailGuide)}.pdf",                    
                        IsInline = false,
                        Data = new MemoryStream(Resources.FluentEmailGuide)
                    });

                await emailToSend.SendAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to send email {email.Recipient}. Exception:{ex.Message}");
                throw;
            }
        }

        public async Task SendEmail(IEnumerable<Communication> emails)
        {
            using var scope = _serviceProvider.CreateScope();
            _fluentEmailFactory = scope.ServiceProvider.GetRequiredService<IFluentEmailFactory>();
            foreach (var email in emails)
            {
                await SendEmail(email);
            }
        }

        public async Task SendEmail(Wellcome email)
        {
            try
            {
                var template = "Hello @Model.Customer, Wellcome! <br/><br/> It's a pleasure to have you here.";

                var emailToSend = _fluentEmailFactory
                   .Create()
                   .SetFrom(email.Sender)
                   .To(email.Recipient)
                   .Subject(email.Subject)
                   .UsingTemplate(template, email);

                await emailToSend.SendAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to send email {email.Recipient}. Exception:{ex.Message}");
                throw;
            }
        }

        public async Task SendEmail(IEnumerable<Wellcome> emails)
        {
            using var scope = _serviceProvider.CreateScope();
            _fluentEmailFactory = scope.ServiceProvider.GetRequiredService<IFluentEmailFactory>();
            foreach (var email in emails)
            {
                await SendEmail(email);
            }
        }
    }
}
