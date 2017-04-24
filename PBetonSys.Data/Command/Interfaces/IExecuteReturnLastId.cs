namespace PBetonSys.Data
{
    public interface IExecuteReturnLastId
    {
        T ExecuteReturnLastId<T>(string identityColumnName = null);        
    }
}