using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MSSQL
{
    public class Statistics : IStatistics
    {
        public bool UpdateStatistics(StatisticsInfo statistics)
        {
            string cmdText = @"update [loachs_sites] set 
                                PostCount=@PostCount,
                                CommentCount=@CommentCount,
                                VisitCount=@VisitCount,
                                TagCount=@TagCount";
            SqlParameter[] prams = {
					                        MSSQLHelper.MakeInParam("@PostCount", SqlDbType.Int,4,statistics.PostCount),
					                        MSSQLHelper.MakeInParam("@CommentCount", SqlDbType.Int,4,statistics.CommentCount),
					                        MSSQLHelper.MakeInParam("@VisitCount", SqlDbType.Int,4,statistics.VisitCount),
					                        MSSQLHelper.MakeInParam("@TagCount", SqlDbType.Int,4,statistics.TagCount),
                                        };

            return MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams) == 1;
        }

        public StatisticsInfo GetStatistics()
        {
            string cmdText = "select top 1 * from [loachs_sites]";

            string insertText = "insert into [loachs_sites] ([PostCount],[CommentCount],[VisitCount],[TagCount],[setting]) values ( '0','0','0','0','<?xml version=\"1.0\" encoding=\"utf-8\"?><SettingInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"></SettingInfo>')";

            List<StatisticsInfo> list = DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));

            if (list.Count == 0)
            {
                MSSQLHelper.ExecuteNonQuery(insertText);
            }
            list = DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));

            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">OleDbDataReader</param>
        /// <returns>TermInfo</returns>
        private static List<StatisticsInfo> DataReaderToList(SqlDataReader read)
        {
            List<StatisticsInfo> list = new List<StatisticsInfo>();
            while (read.Read())
            {
                StatisticsInfo _site = new StatisticsInfo();

                _site.PostCount = Convert.ToInt32(read["PostCount"]);
                _site.CommentCount = Convert.ToInt32(read["CommentCount"]);
                _site.VisitCount = Convert.ToInt32(read["VisitCount"]);
                _site.TagCount = Convert.ToInt32(read["TagCount"]);

                list.Add(_site);
            }
            read.Close();
            return list;
        }
    }
}
