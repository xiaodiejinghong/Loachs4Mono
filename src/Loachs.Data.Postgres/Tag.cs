using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Data;

using Loachs.Entity;
using Loachs.Data;
using NpgsqlTypes;

namespace Loachs.Data.Postgres
{
    public class Tag : ITag
    {
        /// <summary>
        /// 检查别名是否重复
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private bool CheckSlug(TagInfo term)
        {
            while (true)
            {
                string cmdText = string.Empty;
                if (term.TagId == 0)
                {
                    cmdText = string.Format("select count(1) from Loachs_Terms where Slug='{0}' and type={1}", term.Slug, (int)TermType.Tag);
                }
                else
                {
                    cmdText = string.Format("select count(1) from Loachs_Terms where Slug='{0}'  and type={1} and termid<>{2}", term.Slug, (int)TermType.Tag, term.TagId);
                }
                int r = Convert.ToInt32(PGSQLHelper.ExecuteScalar(cmdText));
                if (r == 0)
                {
                    return true;
                }
                term.Slug += "-2";
            }
        }

        public int InsertTag(TagInfo tag)
        {
            CheckSlug(tag);

            string cmdText = @"insert into Loachs_Terms
                            (
                            Type,Name,Slug,Description,Displayorder,Count,CreateDate
                            )
                            values
                            (
                            @Type,@Name,@Slug,@Description,@Displayorder,@Count,@CreateDate
                            )";
            NpgsqlParameter[] prams = { 
                                PGSQLHelper.MakeInParam("@Type",NpgsqlDbType.Integer,1,(int)TermType.Tag),
								PGSQLHelper.MakeInParam("@Name",NpgsqlDbType.Varchar,255,tag.Name),
                                PGSQLHelper.MakeInParam("@Slug",NpgsqlDbType.Varchar,255,tag.Slug),
								PGSQLHelper.MakeInParam("@Description",NpgsqlDbType.Varchar,255,tag.Description),
                                PGSQLHelper.MakeInParam("@Displayorder",NpgsqlDbType.Integer,4,tag.Displayorder),
								PGSQLHelper.MakeInParam("@Count",NpgsqlDbType.Integer,4,tag.Count),
								PGSQLHelper.MakeInParam("@CreateDate",NpgsqlDbType.Date,8,tag.CreateDate)
							};
            PGSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams);

            //int newId = Convert.ToInt32(PGSQLHelper.ExecuteScalar("select top 1 termid from Loachs_Terms order by termid desc"));
            int newId = Convert.ToInt32(PGSQLHelper.ExecuteScalar("select termid from Loachs_Terms order by termid desc limit  1 offset 0"));


            return newId;
        }

        public int UpdateTag(TagInfo tag)
        {
            CheckSlug(tag);

            string cmdText = @"update Loachs_Terms set
                                Type=@Type,
                                Name=@Name,
                                Slug=@Slug,
                                Description=@Description,
                                Displayorder=@Displayorder,
                                Count=@Count,
                                CreateDate=@CreateDate
                                where termid=@termid";
            NpgsqlParameter[] prams = { 
                                PGSQLHelper.MakeInParam("@Type",NpgsqlDbType.Integer,1,(int)TermType.Tag),
								PGSQLHelper.MakeInParam("@Name",NpgsqlDbType.Varchar,255,tag.Name),
                                PGSQLHelper.MakeInParam("@Slug",NpgsqlDbType.Varchar,255,tag.Slug),
								PGSQLHelper.MakeInParam("@Description",NpgsqlDbType.Varchar,255,tag.Description),
                                PGSQLHelper.MakeInParam("@Displayorder",NpgsqlDbType.Integer,4,tag.Displayorder),
								PGSQLHelper.MakeInParam("@Count",NpgsqlDbType.Integer,4,tag.Count),
								PGSQLHelper.MakeInParam("@CreateDate",NpgsqlDbType.Date,8,tag.CreateDate),
                                PGSQLHelper.MakeInParam("@termid",NpgsqlDbType.Integer,1,tag.TagId),
							};
            return Convert.ToInt32(PGSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteTag(int tagId)
        {
            string cmdText = "delete from Loachs_Terms where termid = @termid";
            NpgsqlParameter[] prams = { 
								PGSQLHelper.MakeInParam("@termid",NpgsqlDbType.Integer,4,tagId)
							};
            return PGSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        public TagInfo GetTag(int tagId)
        {
            string cmdText = "select * from Loachs_Terms where termid = @termid";
            NpgsqlParameter[] prams = { 
								PGSQLHelper.MakeInParam("@termid",NpgsqlDbType.Integer,4,tagId)
							};

            List<TagInfo> list = DataReaderToList(PGSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
            return list.Count > 0 ? list[0] : null;
        }


        public List<TagInfo> GetTagList()
        {
            string condition = " type=" + (int)TermType.Tag;

            string cmdText = "select * from Loachs_Terms where " + condition + "  order by displayorder asc ,termid asc";

            return DataReaderToList(PGSQLHelper.ExecuteReader(cmdText));

        }

        //public List<TagInfo> GetTagList(int pageSize, int pageIndex, out int recordCount)
        //{
        //    string condition = " [type]=" + (int)TermType.Tag;


        //    string cmdTotalRecord = "select count(1) from [Loachs_Terms] where " + condition;

        //    recordCount = Convert.ToInt32(PGSQLHelper.ExecuteScalar(CommandType.Text, cmdTotalRecord));


        //    string cmdText = PGSQLHelper.GetPageSql("[Loachs_Terms]", "[termid]", "*", pageSize, pageIndex, 1, condition);
        //    return DataReaderToList(PGSQLHelper.ExecuteReader(cmdText));
        //}

        public List<TagInfo> GetTagList(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return new List<TagInfo>();
            }

            string cmdText = "select * from Loachs_Terms where  termid in (" + ids + ")";

            //  throw new Exception(cmdText);

            return DataReaderToList(PGSQLHelper.ExecuteReader(cmdText));
        }


        //public int GetCount(int tagId, bool incUncategorized)
        //{
        //    string cmdText = "select count(1) from [Loachs_Posts] where [tag] like '%" + tagId + "}%'";

        //    if (!incUncategorized)
        //    {
        //        cmdText = "select count(1) from [Loachs_Posts] where [categoryid]>0 and [tag] like '%" + tagId + "}%'";
        //    }

        //    return Convert.ToInt32(PGSQLHelper.ExecuteScalar(cmdText));
        //}

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">OleDbDataReader</param>
        /// <returns>TagInfo</returns>
        private static List<TagInfo> DataReaderToList(NpgsqlDataReader read)
        {
            List<TagInfo> list = new List<TagInfo>();
            while (read.Read())
            {
                TagInfo tag = new TagInfo();
                tag.TagId = Convert.ToInt32(read["termid"]);
                //  tag.Type = Convert.ToInt32(read["Type"]);
                tag.Name = Convert.ToString(read["Name"]);
                tag.Slug = Convert.ToString(read["Slug"]);
                tag.Description = Convert.ToString(read["Description"]);
                tag.Displayorder = Convert.ToInt32(read["Displayorder"]);
                tag.Count = Convert.ToInt32(read["Count"]);
                tag.CreateDate = Convert.ToDateTime(read["CreateDate"]);

                list.Add(tag);
            }
            read.Close();
            return list;
        }


    }
}
