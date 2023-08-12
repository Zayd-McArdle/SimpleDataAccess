namespace SimpleDataAccess
{
    public interface IDataAccess
    {
        Task<IEnumerable<TClass>> GetDataAsync<TClass, TParameter>(string storedProcedure, TParameter parameter);
        Task<TClass> GetDataFirstOrDefaultAsync<TClass, TParameter>(string storedProcedure, TParameter parameter);
    }
}