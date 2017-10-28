/*************************************************************************
 * 文件名称 ：ZException.cs                          
 * 描述说明 ：框架错误定义
 * 
 * 创建信息 : create by zhdg on 2012-11-10
 * 修订信息 : modify by (person) on (date) for (reason)
 * 
 * 版权信息 : Copyright (c) 2017    
 **************************************************************************/

using System;

namespace PBetonSys.Core
{
	public class ZException : Exception
	{
		public ZException(string message): base(message){}
        public ZException(string message, Exception innerException) : base(message, innerException) { }
	}
}
