namespace PBetonSys.Data
{
    public interface IParameterValue
    {
        TParameterType ParameterValue<TParameterType>(string outputParameterName);        
    }
}