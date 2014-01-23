using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MSSQL
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
                    cmdText = string.Format("select count(1) from [loachs_terms] where [Slug]='{0}' and [type]={1}", term.Slug, (int)TermType.Tag);
                }
                else
                {
                    cmdText = string.Format("select count(1) from [loachs_terms] where [Slug]='{0}'  and [type]={1} and [termid]<>{2}", term.Slug, (int)TermType.Tag, term.TagId);
                }
                int r = Convert.ToInt32(MSSQLHelper.ExecuteScalar(cmdText));
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

            string cmdText = @"insert into [loachs_terms]
                            (
                            [Type],[Name],[Slug],[Description],[Displayorder],[Count],[CreateDate]
                            )
                            values
                            (
                            @Type,@Name,@Slug,@Description,@Displayorder,@Count,@CreateDate
                            )";
            SqlParameter[] prams = { 
                                MSSQLHelper.MakeInParam("@Type",SqlDbType.Int,1,(int)TermType.Tag),
								MSSQLHelper.MakeInParam("@Name",SqlDbType.VarChar,255,tag.Name),
                                MSSQLHelper.MakeInParam("@Slug",SqlDbType.VarChar,255,tag.Slug),
								MSSQLHelper.MakeInParam("@Description",SqlDbType.VarChar,255,tag.Description),
                                MSSQLHelper.MakeInParam("@Displayorder",SqlDbType.Int,4,tag.Displayorder),
								MSSQLHelper.MakeInParam("@Count",SqlDbType.Int,4,tag.Count),
								MSSQLHelper.MakeInParam("@CreateDate",SqlDbType.Date,8,tag.CreateDate)
							};
            MSSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams);

            int newId = Convert.ToInt32(MSSQLHelper.ExecuteScalar("select top 1 [termid] from [loachs_terms] order by [termid] desc"));

            return newId;
        }

        public int UpdateTag(TagInfo tag)
        {
            CheckSlug(tag);

            string cmdText = @"update [loachs_terms] set
                                [Type]=@Type,
                                [Name]=@Name,
                                [Slug]=@Slug,
                                [Description]=@Description,
                                [Displayorder]=@Displayorder,
                                [Count]=@Count,
                                [CreateDate]=@CreateDate
                                where termid=@termid";
            SqlParameter[] prams = { 
                                MSSQLHelper.MakeInParam("@Type",SqlDbType.Int,1,(int)TermType.Tag),
								MSSQLHelper.MakeInParam("@Name",SqlDbType.VarChar,255,tag.Name),
                                MSSQLHelper.MakeInParam("@Slug",SqlDbType.VarChar,255,tag.Slug),
								MSSQLHelper.MakeInParam("@Description",SqlDbType.VarChar,255,tag.Description),
                                MSSQLHelper.MakeInParam("@Displayorder",SqlDbType.Int,4,tag.Displayorder),
								MSSQLHelper.MakeInParam("@Count",SqlDbType.Int,4,tag.Count),
								MSSQLHelper.MakeInParam("@CreateDate",SqlDbType.Date,8,tag.CreateDate),
                                MSSQLHelper.MakeInParam("@termid",SqlDbType.Int,1,tag.TagId),
							};
            return Convert.ToInt32(MSSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteTag(int tagId)
        {
            string cmdText = "delete from [loachs_terms] where [termid] = @termid";
            SqlParameter[] prams = { 
								MSSQLHelper.MakeInParam("@termid",SqlDbType.Int,4,tagId)
							};
            return MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        public TagInfo GetTag(int tagId)
        {
            string cmdText = "select * from [loachs_terms] where [termid] = @termid";
            SqlParameter[] prams = { 
								MSSQLHelper.MakeInParam("@termid",SqlDbType.Int,4,tagId)
							};

            List<TagInfo> list = DataReaderToList(MSSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
            return list.Count > 0 ? list[0] : null;
        }


        public List<TagInfo> GetTagList()
        {
            string condition = " [type]=" + (int)TermType.Tag;

            string cmdText = "select * from [loachs_terms] where " + condition + "  order by [displayorder] asc ,[termid] asc";

            return DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));

        }

        //public List<TagInfo> GetTagList(int pageSize, int pageIndex, out int recordCount)
        //{
        //    string condition = " [type]=" + (int)TermType.Tag;


        //    string cmdTotalRecord = "select count(1) from [loachs_terms] where " + condition;

        //    recordCount = Convert.ToInt32(MSSQLHelper.ExecuteScalar(CommandType.Text, cmdTotalRecord));


        //    string cmdText = MSSQLHelper.GetPageSql("[loachs_terms]", "[termid]", "*", pageSize, pageIndex, 1, condition);
        //    return DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));
        //}

        public List<TagInfo> GetTagList(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return new List<TagInfo>();
            }

            string cmdText = "select * from [loachs_terms] where  [termid] in (" + ids + ")";

            //  throw new Exception(cmdText);

            return DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));
        }


        //public int GetCount(int tagId, bool incUncategorized)
        //{
        //    string cmdText = "select count(1) from [loachs_posts] where [tag] like '%" + tagId + "}%'";

        //    if (!incUncategorized)
        //    {
        //        cmdText = "select count(1) from [loachs_posts] where [categoryid]>0 and [tag] like '%" + tagId + "}%'";
        //    }

        //    return Convert.ToInt32(MSSQLHelper.ExecuteScalar(cmdText));
        //}

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">OleDbDataReader</param>
        /// <returns>TagInfo</returns>
        private static List<TagInfo> DataReaderToList(SqlDataReader read)
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
