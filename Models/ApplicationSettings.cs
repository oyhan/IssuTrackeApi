using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models
{
    public class ApplicationSettings :ISmsSettings , IEmailSettings
    {
        public Uri SmsUrl { get; set; }

        public string UserNameSmsService { get; set; }

        public string PasswordSmsService { get; set; }

        public string JwtKey { get; set; }
        public double JwtExpireDays { get; set; } 
        public string ClientHostAddress { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public string SmtpHostAddress { get; set; }
        public string FromAddress { get; set; } 
        public int SmtpPort { get; set; }
        public bool EnableSSl { get; set; } 

        public string SiteUrl { get; set; } 
    }
}
