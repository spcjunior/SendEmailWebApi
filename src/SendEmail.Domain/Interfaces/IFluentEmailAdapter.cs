using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendEmail.Domain.Interfaces
{
    public interface IFluentEmailAdapter<in T>  where T : IEmail
    {
        Task SendEmail(T email);
        Task SendEmail(IEnumerable<T> emails);
    }
}
