using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.SharedKernel
{
	public static class Extensions
	{
		public static object? GetDefault(this Type type)
		{
			if (type.IsValueType)
				return Activator.CreateInstance(type);
			else
				return null;
		}
	}
}