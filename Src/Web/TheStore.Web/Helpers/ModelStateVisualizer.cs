﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheStore.Web.Services.StatusMessage;

namespace TheStore.Web.Helpers
{
	public class ModelStateVisualizer
	{
		public static void Visualize(IStatusMessageService statusMessageService, ModelStateDictionary modestateDict)
		{
			var query = modestateDict.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

			foreach (var error in query)
			{
				statusMessageService.AddMessage(error, MessageType.Error);
			}
		}
	}
}
