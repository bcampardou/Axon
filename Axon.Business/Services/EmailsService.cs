using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Services;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using RazorEngine;
using RazorEngine.Templating;
using RazorLight;

namespace Axon.Business.Services
{
    public class EmailService : Service, IEmailService
    {
        private MailKit.Net.Smtp.SmtpClient _smtpClient;
        public MailKit.Net.Smtp.SmtpClient SmtpClient
        {
            get
            {
                if (_smtpClient == null)
                {
                    _smtpClient = new MailKit.Net.Smtp.SmtpClient();
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    _smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                }
                return _smtpClient;
            }
        }

        private RazorLightEngine _razorEngine;
        public EmailService(ILoggerFactory loggerFactory, IConfiguration configuration, IEasyCachingProvider cachingProvider, ClaimsPrincipal currentUser, IServiceProvider serviceProvider, RazorLightEngine razorEngine) : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider)
        {
            _razorEngine = razorEngine;
        }

        public async Task SendMailWithData<DATATYPE>(string name, string email, string title, string templatePath, DATATYPE data)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["SMTP:Mail"], _configuration["SMTP:FromMail"]));
                message.To.Add(new MailboxAddress(name, email));
                message.Subject = title;

                message.Body = new TextPart("html")
                {
                    Text = await _razorEngine.CompileRenderAsync(templatePath, data) //Engine.Razor.Run(templatePath, typeof(DATATYPE), data)
                };

                await _SendMailAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "send mail failed");
                throw e;
            }
        }


        protected void _ConnectSmtp()
        {

            _smtpClient.Connect(_configuration["SMTP:Server"], int.Parse(_configuration["SMTP:Port"]), bool.Parse(_configuration["SMTP:UseSSL"]));

            // Note: only needed if the SMTP server requires authentication
            _smtpClient.Authenticate(_configuration["SMTP:Credentials:Login"], _configuration["SMTP:Credentials:Password"]);
        }

        protected void _DisconnectSmtp()
        {
            _smtpClient.Disconnect(true);
        }

        protected async Task _SendMailAsync(MimeMessage message)
        {
            using (var client = SmtpClient)
            {
                _ConnectSmtp();

                await client.SendAsync(message);

                _DisconnectSmtp();
            }
        }
    }
}
