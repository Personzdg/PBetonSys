/*************************************************************************
 * 文件名称 ：ICompress.cs                          
 * 描述说明 ：文件压缩接口
 * 
 * 创建信息 : create by zhdg on 2012-11-10
 * 修订信息 : modify by (person) on (date) for (reason)
 * 
 * 版权信息 : Copyright (c) 2017    
**************************************************************************/

using System.IO;

namespace PBetonSys.Core
{
    public interface ICompress
    {
        string Suffix(string orgSuffix);
        Stream Compress(Stream fileStream,string fullName);
    }
}
