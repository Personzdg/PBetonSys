/*************************************************************************
 * 文件名称 ：ParamDeleteData.cs                          
 * 描述说明 ：接入参数数据
 * 
 * 创建信息 : create by zhdg on 2012-11-10
 * 修订信息 : modify by (person) on (date) for (reason)
 * 
 * 版权信息 : Copyright (c) 2017    
**************************************************************************/

using System.Collections.Generic;

namespace PBetonSys.Core
{
    public class ParamInsertData
    {
        public string From { get; set; }
        public Dictionary<string,object> Columns { get; set; }

        public ParamInsertData()
        {
            From = "";
            Columns = new Dictionary<string, object>();
        }
    }
}
