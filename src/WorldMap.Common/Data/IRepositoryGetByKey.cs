namespace WorldMap.Common.Data
{
	public interface IRepositoryGetByKey<TKey, T> where T : class
	{
		T Get(TKey id);
	}
}