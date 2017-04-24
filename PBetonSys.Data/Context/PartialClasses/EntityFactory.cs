﻿namespace PBetonSys.Data
{
	public partial class DbContext
	{
		public IDbContext EntityFactory(IEntityFactory entityFactory)
		{
			Data.EntityFactory = entityFactory;
			return this;
		}
	}
}
