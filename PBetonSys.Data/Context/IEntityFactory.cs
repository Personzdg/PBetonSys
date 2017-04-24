using System;

namespace PBetonSys.Data
{
	public interface IEntityFactory
	{
		object Create(Type type);
	}
}
