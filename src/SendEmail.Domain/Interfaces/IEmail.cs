namespace SendEmail.Domain.Interfaces
{
    public interface IEmail
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
    }
}
