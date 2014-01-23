using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MYSQL
{
    public class Link : ILink
    {
        public int InsertLink(LinkInfo link)
        {
            string cmdText = @"insert into Loachs_Links
                            (
                            type,name,href,position,target,description,displayorder,status,createdate
                            )
                            values
                            (
                            @type,@name,@href,@position,@target,@description,@displayorder,@status,@createdate
                            )";
            MySqlParameter[] prams = { 
                                MYSQLHelper.MakeInParam("@type",MySqlDbType.Int32,4,link.Type),
								MYSQLHelper.MakeInParam("@name",MySqlDbType.VarChar,100,link.Name),
                                MYSQLHelper.MakeInParam("@href",MySqlDbType.VarChar,255,link.Href),
                                MYSQLHelper.MakeInParam("@position",MySqlDbType.Int32,4,link.Position),
                                MYSQLHelper.MakeInParam("@target",MySqlDbType.VarChar,50,link.Target),
								MYSQLHelper.MakeInParam("@description",MySqlDbType.VarChar,255,link.Description),
                                MYSQLHelper.MakeInParam("@displayorder",MySqlDbType.Int32,4,link.Displayorder),
								MYSQLHelper.MakeInParam("@status",MySqlDbType.Int32,4,link.Status),
								MYSQLHelper.MakeInParam("@createdate",MySqlDbType.Date,8,link.CreateDate),
							};

            int r = MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
            if (r > 0)
            {
                return Convert.ToInt32(MYSQLHelper.ExecuteScalar("select linkid from Loachs_Links  order by linkid desc limit 0,1"));
            }
            return 0;
        }

        public int UpdateLink(LinkInfo link)
        {
            string cmdText = @"update Loachs_Links set
                                type=@type,
                                name=@name,
                                href=@href,
                                position=@position,
                                target=@target,
                                description=@description,
                                displayorder=@displayorder,
                                status=@status,
                                createdate=@createdate
                                where linkid=@linkid";
            MySqlParameter[] prams = { 
                                MYSQLHelper.MakeInParam("@type",MySqlDbType.Int32,4,link.Type),
								MYSQLHelper.MakeInParam("@name",MySqlDbType.VarChar,100,link.Name),
                                MYSQLHelper.MakeInParam("@href",MySqlDbType.VarChar,255,link.Href),
                                MYSQLHelper.MakeInParam("@position",MySqlDbType.Int32,4,link.Position),
                                MYSQLHelper.MakeInParam("@target",MySqlDbType.VarChar,50,link.Target),
								MYSQLHelper.MakeInParam("@description",MySqlDbType.VarChar,255,link.Description),
                                MYSQLHelper.MakeInParam("@displayorder",MySqlDbType.Int32,4,link.Displayorder),
								MYSQLHelper.MakeInParam("@status",MySqlDbType.Int32,4,link.Status),
								MYSQLHelper.MakeInParam("@createdate",MySqlDbType.Date,8,link.CreateDate),
                                MYSQLHelper.MakeInParam("@linkid",MySqlDbType.Int32,4,link.LinkId),
							};

            return Convert.ToInt32(MYSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteLink(int linkId)
        {
            string cmdText = "delete from Loachs_Links where linkid = @linkid";
            MySqlParameter[] prams = { 
								MYSQLHelper.MakeInParam("@linkid",MySqlDbType.Int32,4,linkId)
							};
            return MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        //public LinkInfo GetLink(int linkid)
        //{
        //    string cmdText = "select * from [Loachs_Links] where [linkid] = @linkid";
        //    MySqlParameter[] prams = { 
        //                        MYSQLHelper.MakeInParam("@linkid",MySqlDbType.Int32,4,linkid)
        //                    };

        //    List<LinkInfo> list = DataReaderToList(MYSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
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
        //    string cmdText = "select * from [Loachs_Links] where " + condition + "  order by [displayorder] asc";

        //    return DataReaderToList(MYSQLHelper.ExecuteReader(cmdText));

        //}

        public List<LinkInfo> GetLinkList()
        {

            string cmdText = "select * from Loachs_Links  order by displayorder asc,linkid asc";

            return DataReaderToList(MYSQLHelper.ExecuteReader(cmdText));

        }


        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">OleDbDataReader</param>
        /// <returns>LinkInfo</returns>
        private static List<LinkInfo> DataReaderToList(MySqlDataReader read)
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
