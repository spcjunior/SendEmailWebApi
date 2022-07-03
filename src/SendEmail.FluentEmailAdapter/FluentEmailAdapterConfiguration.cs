using System.ComponentModel.DataAnnotations;

namespace SendEmail.FluentEmailAdapter
{
    public class FluentEmailAdapterConfiguration
    {
        [Required]
        public bool IsAuthenticated { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public bool EnableSsl { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Port { get; set; }
        [Required]
        public bool DevEnvironment { get; set; } = true;
    }
}
