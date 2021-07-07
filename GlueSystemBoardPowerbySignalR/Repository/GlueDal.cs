using GlueSystemBoardPowerbySignalR.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GlueSystemBoardPowerbySignalR.Repository
{
    public class GlueDal
    {
        public static DataTable GetDataDal()
        {
            string sql = $@"
                            SELECT  * FROM  Mes_GlueBoardInfo mgbi ;
                        ";
            return DbHelper.ExecuteQueryDataTable(sql);
        }
    }
}