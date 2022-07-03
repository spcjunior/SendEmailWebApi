using FluentEmail.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendEmail.Domain.Interfaces;
using SendEmail.Domain.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmail.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendEmailController : ControllerBase
    {

        [HttpGet("send-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendEmail(
            [FromServices] IFluentEmailFactory emailFactory,
            string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
            {
                return BadRequest("Provide a valid e-mail.");
            }

            var emailTest = emailFactory
                .Create()
                .To(recipient)
                .Subject("E-mail FluentEmail Test")
                .Body("This is the first test with FluentEmail");

            await emailTest.SendAsync();

            return Ok();
        }

        [HttpGet("send-email-razor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendEmailWithRazor(
            [FromServices] IFluentEmailAdapter<Wellcome> fluentEmailAdapter,
            int totalSend,
            string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
            {
                return BadRequest("Provide a valid e-mail.");
            }

            if (totalSend is < 1 or > 50)
            {
                return BadRequest("Provide a totalSend between 1 and 50.");
            }

            var emails = Enumerable.Range(1, totalSend).Select(i =>
                new Wellcome
                {
                    Subject = $"E-mail FluentAdapter Test-{i}",
                    Recipient = recipient,
                    Sender = "youremail@gmail.com",
                    Customer = $"João Marcos Felipe{i}",
                }).ToList();

            await Task.Factory.StartNew(() =>
            {
                fluentEmailAdapter.SendEmail(emails);
            });

            return Ok();
        }

        [HttpGet("send-email-razor-custom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendEmailWithRazorCustom(
            [FromServices] IFluentEmailAdapter<Communication> fluentEmailAdapter,
            Companies idCompany,
            string recipient)
        {
            if (!((int)idCompany is 1 or 7))
            {
                return BadRequest("Enter with 1 ou 7.");
            }
            if (string.IsNullOrEmpty(recipient))
            {
                return BadRequest("Provide a valid e-mail.");
            }

            var configCompany1 = new
            {
                ConfigPrimaryColor = "#d31245",
                Banner = FluentEmailAdapter.Templates.Resources.img_banner_company1
            };

            var configCompany7 = new
            {
                ConfigPrimaryColor = "#00653a",
                Banner = FluentEmailAdapter.Templates.Resources.img_banner_company7
            };


            await Task.Factory.StartNew(() =>
            {
                fluentEmailAdapter.SendEmail(new Communication
                {
                    Subject = "E-mail FluentAdapter Test",
                    Recipient = recipient,
                    Sender = "sebastiao.cerqueira@dtidigital.com.br",

                    CustomerName = "Felipe Marcos",
                    CustomerEmail = "felipe.m@gmail.com",
                    CustomerPhone = "(21) 4567-9875",

                    CompanyEmail = "goodforyou@goodforyou.com",
                    CompanyName = "Company Good For You",
                    CompanySite = "http://www.goodforyou.com.br",
                    CompanyPhone = "(23) 1233-2123",

                    ConfigLinkButton = "https://github.com/login?return_to=https%3A%2F%2Fgithub.com%2Fspcjunior",
                    ConfigCustomMessage = "Hello Felipe, Lorem ipsum eu sociosqu vivamus et dui, ut euismod venenatis elit massa, sagittis quisque adipiscing massa himenaeos. suscipit ligula fermentum mauris vehicula morbi etiam eu tellus volutpat in nisi dolor, sodales magna senectus ut ipsum cras fringilla aptent arcu erat tincidunt. sit eget pulvinar aptent libero potenti donec praesent, rhoncus sagittis maecenas nibh consequat sit congue dolor, etiam accumsan rhoncus proin dapibus non. nec vulputate dolor est facilisis felis molestie mattis fames lectus, non aliquam suspendisse et tempus litora per quisque auctor, praesent convallis sem vulputate rutrum eleifend netus lacinia. ",
                    ConfigPrimaryColor = (int)idCompany == 1 ? configCompany1.ConfigPrimaryColor : configCompany7.ConfigPrimaryColor,
                    ConfigBannerEmail = (int)idCompany == 1 ? configCompany1.Banner : configCompany7.Banner,
                });
            });

            return Ok();
        }
    }

    public enum Companies
    {
        Company1 = 1,
        Company7 = 7
    }
}
