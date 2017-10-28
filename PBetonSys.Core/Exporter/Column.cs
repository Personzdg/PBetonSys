/*************************************************************************
 * 文件名称 ：Column.cs                          
 * 描述说明 ：定义题头
 * 
 * 创建信息 : create by zhdg on 2012-11-10
 * 修订信息 : modify by (person) on (date) for (reason)
 * 
 * 版权信息 : Copyright (c) 2017    
**************************************************************************/

namespace PBetonSys.Core
{
    public class Column
    {
        public Column()
        {
            rowspan = 1;
            colspan = 1;
        }
        public string field { get; set; }
        public string title { get; set; }
        public int rowspan { get; set; }
        public int colspan { get; set; }
        public bool hidden { get; set; }
    }
}
