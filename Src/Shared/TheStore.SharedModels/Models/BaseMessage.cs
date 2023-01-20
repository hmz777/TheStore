using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.SharedModels.Models
{
	public class BaseMessage
	{
		/// <summary>
		/// Unique Identifier used by logging
		/// </summary>
		protected Guid _correlationId = Guid.NewGuid();
		public Guid CorrelationId => _correlationId;
	}
}
