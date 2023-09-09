using AuthServer.App;
using AuthServer.Mediator.Commands;
using AuthServer.Services.Emails;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AuthServer.Mediator.Handlers
{
	public class ConfirmAccountEmailHandler : IRequestHandler<ConfirmAccountEmailRequest>
	{
		private readonly AppOptions appOptions;
		private readonly EmailOptions emailOptions;
		private readonly EmailService emailService;

		public ConfirmAccountEmailHandler(
			IOptions<AppOptions> appOptions,
			IOptions<EmailOptions> emailOptions,
			EmailService emailService)
		{
			this.appOptions = appOptions.Value;
			this.emailOptions = emailOptions.Value;
			this.emailService = emailService;
		}

		public async Task Handle(ConfirmAccountEmailRequest request, CancellationToken cancellationToken)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(appOptions.AppName, emailOptions.SecurityEmail));
			message.To.Add(new MailboxAddress(request.Name, request.Email));
			message.Subject = "Account Activation";
			message.Date = DateTime.UtcNow;
			message.Body = new TextPart("html")
			{

				Text = $"""
 Hello {request.Name}!,
 activate your account by following the link <a href="{request.ActivationUrl}">here</a>.

 The Store
"""
			};

			await emailService.SendEmailAsync(message, cancellationToken);
		}
	}
}
