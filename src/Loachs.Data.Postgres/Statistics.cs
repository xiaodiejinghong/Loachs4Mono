using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;
using Loachs.Entity;
using Loachs.Data;
using NpgsqlTypes;


namespace Loachs.Data.Postgres
{
    public class Statistics : IStatistics
    {
        public bool UpdateStatistics(StatisticsInfo statistics)
        {
            string cmdText = @"update Loachs_Sites set 
                                PostCount=@PostCount,
                                CommentCount=@CommentCount,
                                VisitCount=@VisitCount,
                                TagCount=@TagCount";
            NpgsqlParameter[] prams = {
					                        PGSQLHelper.MakeInParam("@PostCount", NpgsqlDbType.Integer,4,statistics.PostCount),
					                        PGSQLHelper.MakeInParam("@CommentCount", NpgsqlDbType.Integer,4,statistics.CommentCount),
					                        PGSQLHelper.MakeInParam("@VisitCount", NpgsqlDbType.Integer,4,statistics.VisitCount),
					                        PGSQLHelper.MakeInParam("@TagCount", NpgsqlDbType.Integer,4,statistics.TagCount),
                                        };

            return PGSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams) == 1;
        }

        public StatisticsInfo GetStatistics()
        {
            //string cmdText = "select top 1 * from [Loachs_Sites]";
            string cmdText = "select  * from Loachs_Sites limit  1 offset 0 ";


            string insertText = "insert into Loachs_Sites (PostCount,CommentCount,VisitCount,TagCount,setting) values ( '0','0','0','0','<?xml version=\"1.0\" encoding=\"utf-8\"?><SettingInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"></SettingInfo>')";

            List<StatisticsInfo> list = DataReaderToList(PGSQLHelper.ExecuteReader(cmdText));

            if (list.Count == 0)
            {
                PGSQLHelper.ExecuteNonQuery(insertText);
            }
            list = DataReaderToList(PGSQLHelper.ExecuteReader(cmdText));

            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">OleDbDataReader</param>
        /// <returns>TermInfo</returns>
        private static List<StatisticsInfo> DataReaderToList(NpgsqlDataReader read)
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
