using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Business.Abstractions.Services
{
    public interface IEmailService : IService
    {
        Task SendMailWithData<DATATYPE>(string name, string email, string title, string templatePath, DATATYPE data);
    }
}
