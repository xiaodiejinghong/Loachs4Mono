using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MSSQL
{
    public class Category : ICategory
    {
        /// <summary>
        /// 检查别名是否重复
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private bool CheckSlug(CategoryInfo term)
        {
            while (true)
            {
                string cmdText = string.Empty;
                if (term.CategoryId == 0)
                {
                    cmdText = string.Format("select count(1) from [loachs_terms] where [Slug]='{0}' and [type]={1}", term.Slug, (int)TermType.Category);
                }
                else
                {
                    cmdText = string.Format("select count(1) from [loachs_terms] where [Slug]='{0}'  and [type]={1} and [termid]<>{2}", term.Slug, (int)TermType.Category, term.CategoryId);
                }
                int r = Convert.ToInt32(MSSQLHelper.ExecuteScalar(cmdText));
                if (r == 0)
                {
                    return true;
                }
                term.Slug += "-2";
            }
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int InsertCategory(CategoryInfo category)
        {
            CheckSlug(category);

            string cmdText = @"insert into [loachs_terms]
                            (
                            [Type],[Name],[Slug],[Description],[Displayorder],[Count],[CreateDate]
                            )
                            values
                            (
                            @Type,@Name,@Slug,@Description,@Displayorder,@Count,@CreateDate
                            )";
            SqlParameter[] prams = { 
                                MSSQLHelper.MakeInParam("@Type",SqlDbType.Int,1,(int)TermType.Category),
								MSSQLHelper.MakeInParam("@Name",SqlDbType.VarChar,255,category.Name),
                                MSSQLHelper.MakeInParam("@Slug",SqlDbType.VarChar,255,category.Slug),
								MSSQLHelper.MakeInParam("@Description",SqlDbType.VarChar,255,category.Description),
                                MSSQLHelper.MakeInParam("@Displayorder",SqlDbType.Int,4,category.Displayorder),
								MSSQLHelper.MakeInParam("@Count",SqlDbType.Int,4,category.Count),
								MSSQLHelper.MakeInParam("@CreateDate",SqlDbType.Date,8,category.CreateDate)
							};
            MSSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams);

            int newId = Convert.ToInt32(MSSQLHelper.ExecuteScalar("select top 1 [termid] from [loachs_terms] order by [termid] desc"));

            return newId;
        }

        public int UpdateCategory(CategoryInfo category)
        {
            CheckSlug(category);

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
                                MSSQLHelper.MakeInParam("@Type",SqlDbType.Int,1,(int)TermType.Category),
								MSSQLHelper.MakeInParam("@Name",SqlDbType.VarChar,255,category.Name),
                                MSSQLHelper.MakeInParam("@Slug",SqlDbType.VarChar,255,category.Slug),
								MSSQLHelper.MakeInParam("@Description",SqlDbType.VarChar,255,category.Description),
                                MSSQLHelper.MakeInParam("@Displayorder",SqlDbType.Int,4,category.Displayorder),
								MSSQLHelper.MakeInParam("@Count",SqlDbType.Int,4,category.Count),
								MSSQLHelper.MakeInParam("@CreateDate",SqlDbType.Date,8,category.CreateDate),
                                MSSQLHelper.MakeInParam("@termid",SqlDbType.Int,1,category.CategoryId),
							};
            return Convert.ToInt32(MSSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteCategory(int categoryId)
        {
            string cmdText = "delete from [loachs_terms] where [termid] = @termid";
            SqlParameter[] prams = { 
								MSSQLHelper.MakeInParam("@termid",SqlDbType.Int,4,categoryId)
							};
            return MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        public CategoryInfo GetCategory(int categoryId)
        {
            string cmdText = "select * from [loachs_terms] where [termid] = @termid";
            SqlParameter[] prams = { 
								MSSQLHelper.MakeInParam("@termid",SqlDbType.Int,4,categoryId)
							};

            List<CategoryInfo> list = DataReaderToList(MSSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 获取全部分类
        /// </summary>
        /// <returns></returns>
        public List<CategoryInfo> GetCategoryList()
        {
            string condition = " [type]=" + (int)TermType.Category;

            string cmdText = "select * from [loachs_terms] where " + condition + "  order by [displayorder] asc,[termid] asc";

            return DataReaderToList(MSSQLHelper.ExecuteReader(cmdText));

        }
 
    

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">SqlDataReader</param>
        /// <returns>CategoryInfo</returns>
        private static List<CategoryInfo> DataReaderToList(SqlDataReader read)
        {
            List<CategoryInfo> list = new List<CategoryInfo>();
            while (read.Read())
            {
                CategoryInfo category = new CategoryInfo();
                category.CategoryId = Convert.ToInt32(read["termid"]);
              //  category.Type = Convert.ToInt32(read["Type"]);
                category.Name = Convert.ToString(read["Name"]);
                category.Slug = Convert.ToString(read["Slug"]);
                category.Description = Convert.ToString(read["Description"]);
                category.Displayorder = Convert.ToInt32(read["Displayorder"]);
                category.Count = Convert.ToInt32(read["Count"]);
                category.CreateDate = Convert.ToDateTime(read["CreateDate"]);

                list.Add(category);
            }
            read.Close();
            return list;
        }

        
    }
}
