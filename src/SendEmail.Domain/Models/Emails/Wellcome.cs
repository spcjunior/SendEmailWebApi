using SendEmail.Domain.Interfaces;

namespace SendEmail.Domain.Models.Emails
{
    public class Wellcome : IEmail
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }

        public string Customer { get; set; }
    }
}
