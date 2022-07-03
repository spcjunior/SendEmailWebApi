using SendEmail.Domain.Interfaces;

namespace SendEmail.Domain.Models.Emails
{
    public class Communication : IEmail
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }


        public string ConfigCustomMessage { get; set; }
        public string ConfigLinkButton { get; set; }
        public string ConfigPrimaryColor { get; set; }
        public byte[] ConfigBannerEmail { get; set; }

        public string CompanySite { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyName { get; set; }
    }
}
