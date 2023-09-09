using AuthServer.Models;
using MediatR;

namespace AuthServer.Mediator.Commands
{
	public class ConfirmAccountEmailRequest : IRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
        public string ActivationUrl { get; set; }

		public ConfirmAccountEmailRequest(string name, string email, string activationUrl)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Email = email ?? throw new ArgumentNullException(nameof(email));
			ActivationUrl = activationUrl ?? throw new ArgumentNullException(nameof(activationUrl));
		}
	}
}