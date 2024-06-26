﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Cafe.Email
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var client = new SendGridClient(Options.SendGridKey);
			var mesaj = new SendGridMessage()
			{
				From = new EmailAddress("saintcorroy@gmail.com", "Mert Cafe"),
				Subject = subject,
				PlainTextContent = htmlMessage,
				HtmlContent = htmlMessage
			};
			mesaj.AddTo(new EmailAddress(email));
			try
			{
				return client.SendEmailAsync(mesaj);
			}
			catch (System.Exception)
			{

				throw;
			}
			return null;
		}
		public EmailOptions Options { get; set; }
		public EmailSender(IOptions<EmailOptions> emailOptions)
		{
			Options = emailOptions.Value;
		}
	}
}
