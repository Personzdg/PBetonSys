﻿using System;

namespace PBetonSys.Data
{
	public class FluentDataException : Exception
	{
		public FluentDataException(string message)
			: base(message)
		{
		}
		public FluentDataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
