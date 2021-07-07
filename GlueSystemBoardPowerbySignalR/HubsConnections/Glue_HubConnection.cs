using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using GlueSystemBoardPowerbySignalR.Repository;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

#region 技术支持
 //整体参考：http://blog.rdiframework.net/article/225  系列文章
#endregion

namespace GlueSystemBoardPowerbySignalR.HubsConnections
{
    // 不打特性标记的话，前台调用类名或者方法名时必须第一个字母小写，不然无法调用到
    [HubName("gluehub")]
    public class Glue_HubConnection : Hub
    {
        #region 借助定时器获取数据变化
        private static bool AleadyStart = false;
        [HubMethodName("getdata")]
        public void GetData()
        {
            if (AleadyStart)
            {
                return;
            }
            AleadyStart = true;
            Timer datetime_timer = new Timer(new TimerCallback(GetNowTime), "", 0, 1000);//时间
            Timer data_timer = new Timer(new TimerCallback(GetDataList), "", 0, 1000 * 60);//表格数据
        }

        public void GetDataList(object state)
        {
            //调取客户端方法
            var dt = GlueDal.GetDataDal();
            Clients.All.RefreshTable(dt);
        }
        #endregion

        public void GetNowTime(object state)
        {
            //调取客户端方法
            Clients.All.RefreshTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }


        #region sqldependency  数据库变化时主动推送
        #region 必要条件 为数据库开启 Service Broker 
        //Tips：
        //    1.需要为数据库开启 Service Broker
        //    2.开启方法：
        //            ALTER DATABASE DatabaseName SET NEW_BROKER WITH ROLLBACK IMMEDIATE;
        //            ALTER DATABASE Databasename SET ENABLE_BROKER;
        //    3.查询是否开启：
        //    SELECT is_broker_enabled FROM sys.databases WHERE name = 'DBNAME'
        //        0 ：未开启  1：开启 

        //========================参考==============================

        //参考示例：
        //    https://www.cnblogs.com/wanghk/archive/2012/05/12/2497170.html
        //    https://www.cnblogs.com/lanchong/p/7126090.html
        #endregion
        [HubMethodName("getdatabysqldependency")]

        public void GetDatabySqldependency()
        {
            string sql = $@"
                            SELECT [Id]
                                  ,[Dep]
                                  ,[BarCode]
                                  ,[ProcessName]
                                  ,[EPNO]
                                  ,[EPName]
                                  ,[AllExpirationTime_Surplus]
                                  ,[NormalExpirationTime_Surplus]
                                  ,[OperateTime]
                                  ,[OperateState]
                              FROM [dbo].[Mes_GlueBoardInfo]
                           ";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDependency sqlden = new SqlDependency(command);
                    sqlden.OnChange += Sqlden_OnChange;
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        dt.Load(sdr);
                    }
                }
            }
            Clients.All.RefreshTable(dt);
        }

        

        private void Sqlden_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                GetDatabySqldependency();
            }
        }
        #endregion
    }
}