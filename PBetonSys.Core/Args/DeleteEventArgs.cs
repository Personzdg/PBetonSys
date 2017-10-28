/*************************************************************************
 * 文件名称 ：DeleteEventArgs.cs                          
 * 描述说明 ：删除事件参数
 * 
 * 创建信息 : create by zhdg on 2012-11-10
 * 修订信息 : modify by (person) on (date) for (reason)
 * 
 * 版权信息 : Copyright (c) 2017    
 **************************************************************************/

using PBetonSys.Data;

namespace PBetonSys.Core
{
    public class DeleteEventArgs
    {
        public IDbContext db { get; set; }
        public ParamDeleteData data { get; set; }
        public int executeValue { get; set; }
    }
}
