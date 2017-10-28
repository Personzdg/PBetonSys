/*************************************************************************
 * 文件名称 ：ParamDeleteData.cs                          
 * 描述说明 ：删除参数数据
 * 
 * 创建信息 : create by zhdg on 2012-11-10
 * 修订信息 : modify by (person) on (date) for (reason)
 * 
 * 版权信息 : Copyright (c) 2017    
**************************************************************************/

using System.Collections.Generic;

namespace PBetonSys.Core
{
    public class ParamDeleteData
    {
        public string From { get; set; }
        public List<ParamWhere> Where { get; set; }
        public string WhereSql { get { return ParamUtils.GetWhereSql(Where); } }

        
        public object GetValue(string column)
        {
            var first = Where.Find(x => x.Data.Column == column);
            return first == null ? null : first.Data.Value;
        }

        public ParamDeleteData()
        {
            From = "";
            Where = new List<ParamWhere>();
        }
    }
}
