using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AuthServer.Services.Emails
{
	public class EmailService
	{
		private readonly EmailOptions options;

		public EmailService(IOptions<EmailOptions> options)
		{
			this.options = options.Value;
		}

		public async Task SendEmailAsync(MimeMessage message, CancellationToken cancellationToken = default)
		{
			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(options.Server, options.Port, false, cancellationToken);
				await client.SendAsync(message, cancellationToken);
				await client.DisconnectAsync(true, cancellationToken);
			}
		}

		public class EmailModel
		{
			public string FromName { get; set; }
			public string FromEmail { get; set; }
			public string ToName { get; set; }
			public string ToEmail { get; set; }
			public string Subject { get; set; }
		}
	}
}
