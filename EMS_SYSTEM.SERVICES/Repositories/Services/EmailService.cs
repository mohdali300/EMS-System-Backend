using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using Microsoft.Extensions.Primitives;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class EmailService : GenericRepository<EmailService>,IEmailService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings>options,UnvcenteralDataBaseContext Db, UserManager<ApplicationUser> userManager , IUnitOfWork unitOfWork) : base(Db)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailSettings = options.Value;
        }
        public async Task<ResponseDTO> HandelSendEmail(string NID)
        {
            var user = _context.Staff.FirstOrDefault(s => s.NID == NID);
            if (user == null)
            {
                return new ResponseDTO
                {
                    Message = "Account Not Found",
                    IsDone = true,
                    StatusCode = 200
                };
            }
            string email = user.Email;
            string subject = "Reset Your Password";
            SendOTPEmailAsync(email, subject, user.Name, user.Email);


            return new ResponseDTO
            {
                Message = "Email sent Succssefully",
                IsDone = true,
                Model = user.Email,
                StatusCode = 200
            };
        }
        private string GeneraterBody(string name , string link)
        {
            string body = "<div>";
            body += "<h4>Hi " + name + "</h4>";
            body += "<h5>To Reset your Password please enter here</h5>";
            body += "<h5>" + link + "</h5>";
            body += "<h5>Please, Keep your password safe</h5>";
            body += "</div>";
            return body;
        }
        public void SendOTPEmailAsync(string email, string subject, string name , string password)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_emailSettings.Email));
            mail.To.Add(MailboxAddress.Parse("abdelattybadwy166@gmail.com"));
            mail.Subject = subject;
            string link = "http://localhost:5173/resetpassword";
            string body = GeneraterBody(name,link);
           
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            mail.Body = builder.ToMessageBody();
    
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host,587,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email,_emailSettings.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
            return ;    
        }


    }
}
