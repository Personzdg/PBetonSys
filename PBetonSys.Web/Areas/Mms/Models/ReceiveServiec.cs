using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Receive : ModelBase 
    {


        //public int pno { get; set; }
        public DateTime ctime { get; set; }
        public string bajarid { get; set; }
        public string hid { get; set; }
        public int carserial { get; set; }
        public string empid { get; set; }
        public decimal qty { get; set; }
        public string custid { get; set; }
        public string formulaid { get; set; }
        public string carid { get; set; }
        public string slump { get; set; }
        public string stgid { get; set; }
        public string Mstring4 { get; set; }
        public string Mstring5 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal num4 { get; set; }
        public decimal num5 { get; set; }
        public decimal num6 { get; set; }
        public decimal num7 { get; set; }
        public decimal num8 { get; set; }
        public decimal num9 { get; set; }
        public decimal num10 { get; set; }
        public decimal num11 { get; set; }
        public decimal num12 { get; set; }
        public decimal num13 { get; set; }
        public decimal num14 { get; set; }
        public decimal mtvalue1 { get; set; }
        public decimal mtvalue2 { get; set; }
        public decimal mtvalue3 { get; set; }
        public decimal mtvalue4 { get; set; }
        public decimal mtvalue5 { get; set; }
        public decimal mtvalue6 { get; set; }
        public decimal mtvalue7 { get; set; }
        public decimal mtvalue8 { get; set; }
        public decimal mtvalue9 { get; set; }
        public decimal mtvalue10 { get; set; }
        public decimal mtvalue11 { get; set; }
        public decimal mtvalue12 { get; set; }
        public decimal mtvalue13 { get; set; }
        public decimal mtvalue14 { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
        public decimal Price5 { get; set; }
        public decimal Price6 { get; set; }
        public decimal Price7 { get; set; }
        public decimal Price8 { get; set; }
        public decimal Price9 { get; set; }
        public decimal Price10 { get; set; }
        public decimal Price11 { get; set; }
        public decimal Price12 { get; set; }
        public decimal Price13 { get; set; }
        public decimal Price14 { get; set; }
        public decimal mone1 { get; set; }
        public decimal mone2 { get; set; }
        public decimal mone3 { get; set; }
        public decimal mone4 { get; set; }
        public decimal mone5 { get; set; }
        public decimal mone6 { get; set; }
        public decimal mone7 { get; set; }
        public decimal mone8 { get; set; }
        public decimal mone9 { get; set; }
        public decimal mone10 { get; set; }
        public decimal mone11 { get; set; }
        public decimal mone12 { get; set; }
        public decimal mone13 { get; set; }
        public decimal mone14 { get; set; }
        
        public int Bodyno { get; set; }
        public decimal sumNum { get; set; }
        public decimal 方量 { get; set; }
        public string 工程名称 { get; set; }
        public string 标号 { get; set; }
         public string 楼号 { get; set; }
         public decimal 单价合计 { get; set; }
         public decimal 总胶量 { get; set; }
         public decimal summoney { get; set; }
         public decimal 泵费 { get; set; }
         public decimal 运费 { get; set; } 
        

        
            
        
     
    }

    public class ReceiveService : ServiceBase<Receive>
    {
        public ReceiveService()
        {
            base.ModuleName = "Betonsys";
        }
        public List<Receive> GetReceiveData(string BegDayDate, string EndDatetime,int flag)
        {            //pno,
            var strSql = String.Format(@"
                         select ctime,bajarid,hid,carserial,empid,qty,custid,formulaid,carid,slump,stgid,Mstring4,Mstring5,
                         num1,num2,num3,num4,num5,num6,num7,num8,num9,num10,num11,num12,num13,num14,Bodyno,sumNum
                         from Betonsys..Receive('{0}','{1}','{2}')  
                         ", BegDayDate, EndDatetime + " 23:59:59",flag );

            return db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql).QueryMany<Receive>();
        }//消耗明细


        public dynamic GetReceiveData(string BegDayDate, string EndDatetime,int flag,int pageIndex,int pageSize) 
        {
            var strSql = String.Format(@"
                         select row_number() over (order by ctime) as rownumber,ctime,bajarid,hid,carserial,empid,qty,custid,formulaid,carid,slump,stgid,Mstring4,Mstring5,
                         num1,num2,num3,num4,num5,num6,num7,num8,num9,num10,num11,num12,num13,num14,Bodyno,sumNum
                         from Betonsys..Receive('{0}','{1}','{2}')  
                         ", BegDayDate, EndDatetime + " 23:59:59", flag);

            string strSql1 = String.Format(@" select top {0} ctime,bajarid,hid,carserial,empid,qty,custid,formulaid,carid,slump,stgid,Mstring4,Mstring5,
                         num1,num2,num3,num4,num5,num6,num7,num8,num9,num10,num11,num12,num13,num14,Bodyno,sumNum from ({1}) as temp where temp.rownumber>({2}-1)*{0}", pageSize, strSql, pageIndex);
            string strSql2 = String.Format(@" select count(1) from ({0}) as temp",strSql);
            dynamic result = new ExpandoObject();
            result.rows = db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql1).QueryMany<Receive>();
            result.total = db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql2).QuerySingle<int>();
            return result;
        }//消耗明细分页
        public dynamic GetTotalReceiveData(string BegDayDate, string EndDatetime,int flag)
        {
            var strSql = String.Format(@"
                         select sum(qty) as qty, sum(num1) as num1 ,sum(num2) as num2 ,sum(num3) as num3,sum(num4) as num4,sum(num5) as num5,sum(num6) as num6,sum(num7) as num7,
                          sum(num8) as num8,sum(num9) as num9,sum(num10) as num10,sum(num11) as num11,sum(num12) as num12,sum(num13) as num13,sum(isnull(num14,0)) as num14,sum(sumNum) as sumNum
                         from Betonsys..Receive('{0}','{1}','{2}')  
                         ", BegDayDate, EndDatetime + " 23:59:59",flag );

            return db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql).QuerySingle<Receive>();

           
        }//消耗明细分页

        //////////////////
        public dynamic GetProjectNameReceivedtData(string BegDayDate, string EndDatetime, int Flag ,int pageIndex, int pageSize)
        {
            var strSql = String.Format(@"
                         select row_number() over (order by 工程名称) as rownumber,方量,工程名称,标号,楼号,单价合计,总胶量,summoney,泵费,运费,
                         mtvalue1,Price1,mone1,mtvalue2,Price2,mone2,mtvalue3,Price3,mone3,mtvalue4,Price4,mone4,mtvalue5,Price5,mone5,mtvalue6,Price6,mone6,
                         mtvalue7,Price7,mone7,mtvalue8,Price8,mone8,mtvalue9,Price9,mone9
                         from Betonsys..ProjectNameReceivedt('{0}','{1}','{2}')  
                         ", BegDayDate, EndDatetime + " 23:59:59", Flag);

            string strSql1 = String.Format(@" select top {0} 方量,工程名称,标号,楼号,单价合计,总胶量,summoney,泵费,运费,
                              mtvalue1,Price1,mone1,mtvalue2,Price2,mone2,mtvalue3,Price3,mone3,mtvalue4,Price4,mone4,mtvalue5,Price5,mone5,mtvalue6,Price6,mone6,
                              mtvalue7,Price7,mone7,mtvalue8,Price8,mone8,mtvalue9,Price9,mone9
                              from ({1}) as temp where temp.rownumber>({2}-1)*{0}", pageSize, strSql, pageIndex);
            string strSql2 = String.Format(@" select count(1) from ({0}) as temp", strSql);
            dynamic result = new ExpandoObject();
            result.rows = db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql1).QueryMany<Receive>();
            result.total = db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql2).QuerySingle<int>();
            return result;
        }
    }   
}