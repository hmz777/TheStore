﻿namespace TheStore.Web.BlazorApp.Client.Auth
{
	public class AntiforgeryHandler : DelegatingHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request.Headers.Add("X-CSRF", "1");
			return base.SendAsync(request, cancellationToken);
		}
	}
}