using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MSSQL
{
    public class Link : ILink
    {
        public int InsertLink(LinkInfo link)
        {
            string cmdText = @"insert into [loachs_links]
                            (
                            [type],[name],[href],[position],[target],[description],[displayorder],[status],[createdate]
                            )
                            values
                            (
                            @type,@name,@href,@position,@target,@description,@displayorder,@status,@createdate
                            )";
            SqlParameter[] prams = { 
                                MSSQLHelper.MakeInParam("@type",SqlDbType.Int,4,link.Type),
								MSSQLHelper.MakeInParam("@name",SqlDbType.VarChar,100,link.Name),
                                MSSQLHelper.MakeInParam("@href",SqlDbType.VarChar,255,link.Href),
                                MSSQLHelper.MakeInParam("@position",SqlDbType.Int,4,link.Position),
                                MSSQLHelper.MakeInParam("@target",SqlDbType.VarChar,50,link.Target),
								MSSQLHelper.MakeInParam("@description",SqlDbType.VarChar,255,link.Description),
                                MSSQLHelper.MakeInParam("@displayorder",SqlDbType.Int,4,link.Displayorder),
								MSSQLHelper.MakeInParam("@status",SqlDbType.Int,4,link.Status),
								MSSQLHelper.MakeInParam("@createdate",SqlDbType.Date,8,link.CreateDate),
							};

            int r = MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
            if (r > 0)
            {
                return Convert.ToInt32(MSSQLHelper.ExecuteScalar("select top 1 [linkid] from [loachs_links]  order by [linkid] desc"));
            }
            return 0;
        }

        public int UpdateLink(LinkInfo link)
        {
            string cmdText = @"update [loachs_links] set
                                [type]=@type,
                                [name]=@name,
                                [href]=@href,
                                [position]=@position,
                                [target]=@target,
                                [description]=@description,
                                [displayorder]=@displayorder,
                                [status]=@status,
                                [createdate]=@createdate
                                where linkid=@linkid";
            SqlParameter[] prams = { 
                                MSSQLHelper.MakeInParam("@type",SqlDbType.Int,4,link.Type),
								MSSQLHelper.MakeInParam("@name",SqlDbType.VarChar,100,link.Name),
                                MSSQLHelper.MakeInParam("@href",SqlDbType.VarChar,255,link.Href),
                                MSSQLHelper.MakeInParam("@position",SqlDbType.Int,4,link.Position),
                                MSSQLHelper.MakeInParam("@target",SqlDbType.VarChar,50,link.Target),
								MSSQLHelper.MakeInParam("@description",SqlDbType.VarChar,255,link.Description),
                                MSSQLHelper.MakeInParam("@displayorder",SqlDbType.Int,4,link.Displayorder),
								MSSQLHelper.MakeInParam("@status",SqlDbType.Int,4,link.Status),
								MSSQLHelper.MakeInParam("@createdate",SqlDbType.Date,8,link.CreateDate),
                                MSSQLHelper.MakeInParam("@linkid",SqlDbType.Int,4,link.LinkId),
							};

            return Convert.ToInt32(MSSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteLink(int linkId)
        {
            string cmdText = "delete from [loachs_links] where [linkid] = @linkid";
            SqlParameter[] prams = { 
								MSSQLHelper.MakeInParam("@linkid",SqlDbType.Int,4,linkId)
							};
            return MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        //public LinkInfo GetLink(int linkid)
        //{
        //    string cmdText = "select * from [loachs_links] where [linkid] = @linkid";
        //    SqlParameter[] prams = { 
        //                        MSSQLHelper.MakeInParam("@linkid",SqlDbType.Int,4,linkid)
        //                    };

        //    List<LinkInfo> list = DataReaderToList(MSSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
        //    return list.Count > 0 ? list[0] : null;
        //}


        //public List<LinkInfo> GetLinkList(int type, int position, int status)
        //{
        //    string condition = " 1=1 ";
        //    if (type != -1)
        //    {
        //        condition += " and [type]=" + type;
        //    }
        //    if (position != -1)
        //    {
        //        condition += " and [position]=" + position;
        //    }
        //    if (status != -1)
        //    {
        //        condition += " and [status]=" + status;
        //    }
        //    string cmdText = "select * from [loachs_links] where " + condition + "  order by [displayorder] asc";

        //    return DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));

        //}

        public List<LinkInfo> GetLinkList()
        {

            string cmdText = "select * from [loachs_links]  order by [displayorder] asc,[linkid] asc";

            return DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));

        }


        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">OleDbDataReader</param>
        /// <returns>LinkInfo</returns>
        private static List<LinkInfo> DataReaderToList(SqlDataReader read)
        {
            List<LinkInfo> list = new List<LinkInfo>();
            while (read.Read())
            {
                LinkInfo link = new LinkInfo();
                link.LinkId = Convert.ToInt32(read["Linkid"]);
                link.Type = Convert.ToInt32(read["Type"]);
                link.Name = Convert.ToString(read["Name"]);
                link.Href = Convert.ToString(read["Href"]);
                if (read["Position"] != DBNull.Value)
                {
                    link.Position = Convert.ToInt32(read["Position"]);
                }

                link.Target = Convert.ToString(read["Target"]);
                link.Description = Convert.ToString(read["Description"]);
                link.Displayorder = Convert.ToInt32(read["Displayorder"]);
                link.Status = Convert.ToInt32(read["Status"]);
                link.CreateDate = Convert.ToDateTime(read["CreateDate"]);

                list.Add(link);
            }
            read.Close();
            return list;
        }
    }
}
