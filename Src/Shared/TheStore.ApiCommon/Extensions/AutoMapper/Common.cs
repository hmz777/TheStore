using AutoMapper;

namespace TheStore.ApiCommon.Extensions.AutoMapper
{
	public static class Common
	{
		public static List<TViewModel> Map<TData, TViewModel>(this List<TData> data, IMapper mapper)
		{
			return mapper.Map<List<TViewModel>>(data);
		}

		public static TViewModel? Map<TData, TViewModel>(this TData? data, IMapper mapper)
		{
			return mapper.Map<TViewModel>(data);
		}
	}
}