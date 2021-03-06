using System;

namespace PBetonSys.Data
{
	public interface IUpdateBuilder : IExecute
	{
		BuilderData Data { get; }
		IUpdateBuilder Column(string columnName, object value, DataTypes parameterType = DataTypes.Object, int size = 0);
		IUpdateBuilder Where(string columnName, object value, DataTypes parameterType = DataTypes.Object, int size = 0);
        IUpdateBuilder Where(string sql); //add by zhdg
        IUpdateBuilder Fill(Action<IInsertUpdateBuilder> fillMethod);
	}
}