using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MYSQL
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
                    cmdText = string.Format("select count(1) from Loachs_Terms where Slug='{0}' and type={1}", term.Slug, (int)TermType.Category);
                }
                else
                {
                    cmdText = string.Format("select count(1) from Loachs_Terms where Slug='{0}'  and type={1} and termid<>{2}", term.Slug, (int)TermType.Category, term.CategoryId);
                }
                int r = Convert.ToInt32(MYSQLHelper.ExecuteScalar(cmdText));
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

            string cmdText = @"insert into Loachs_Terms
                            (
                            Type,Name,Slug,Description,Displayorder,Count,CreateDate
                            )
                            values
                            (
                            @Type,@Name,@Slug,@Description,@Displayorder,@Count,@CreateDate
                            )";
            MySqlParameter[] prams = { 
                                MYSQLHelper.MakeInParam("@Type",MySqlDbType.Int32,1,(int)TermType.Category),
								MYSQLHelper.MakeInParam("@Name",MySqlDbType.VarChar,255,category.Name),
                                MYSQLHelper.MakeInParam("@Slug",MySqlDbType.VarChar,255,category.Slug),
								MYSQLHelper.MakeInParam("@Description",MySqlDbType.VarChar,255,category.Description),
                                MYSQLHelper.MakeInParam("@Displayorder",MySqlDbType.Int32,4,category.Displayorder),
								MYSQLHelper.MakeInParam("@Count",MySqlDbType.Int32,4,category.Count),
								MYSQLHelper.MakeInParam("@CreateDate",MySqlDbType.Date,8,category.CreateDate)
							};
            MYSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams);

            //int newId = Convert.ToInt32(MYSQLHelper.ExecuteScalar("select top 1 [termid] from [Loachs_Terms] order by [termid] desc"));
            int newId = Convert.ToInt32(MYSQLHelper.ExecuteScalar("select termid from Loachs_Terms order by termid desc limit 0,1"));


            return newId;
        }

        public int UpdateCategory(CategoryInfo category)
        {
            CheckSlug(category);

            string cmdText = @"update Loachs_Terms set
                                Type=@Type,
                                Name=@Name,
                                Slug=@Slug,
                                Description=@Description,
                                Displayorder=@Displayorder,
                                Count=@Count,
                                CreateDate=@CreateDate
                                where termid=@termid";
            MySqlParameter[] prams = { 
                                MYSQLHelper.MakeInParam("@Type",MySqlDbType.Int32,1,(int)TermType.Category),
								MYSQLHelper.MakeInParam("@Name",MySqlDbType.VarChar,255,category.Name),
                                MYSQLHelper.MakeInParam("@Slug",MySqlDbType.VarChar,255,category.Slug),
								MYSQLHelper.MakeInParam("@Description",MySqlDbType.VarChar,255,category.Description),
                                MYSQLHelper.MakeInParam("@Displayorder",MySqlDbType.Int32,4,category.Displayorder),
								MYSQLHelper.MakeInParam("@Count",MySqlDbType.Int32,4,category.Count),
								MYSQLHelper.MakeInParam("@CreateDate",MySqlDbType.Date,8,category.CreateDate),
                                MYSQLHelper.MakeInParam("@termid",MySqlDbType.Int32,1,category.CategoryId),
							};
            return Convert.ToInt32(MYSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteCategory(int categoryId)
        {
            string cmdText = "delete from Loachs_Terms where termid = @termid";
            MySqlParameter[] prams = { 
								MYSQLHelper.MakeInParam("@termid",MySqlDbType.Int32,4,categoryId)
							};
            return MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        public CategoryInfo GetCategory(int categoryId)
        {
            string cmdText = "select * from Loachs_Terms where termid = @termid";
            MySqlParameter[] prams = { 
								MYSQLHelper.MakeInParam("@termid",MySqlDbType.Int32,4,categoryId)
							};

            List<CategoryInfo> list = DataReaderToList(MYSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 获取全部分类
        /// </summary>
        /// <returns></returns>
        public List<CategoryInfo> GetCategoryList()
        {
            string condition = " type=" + (int)TermType.Category;

            string cmdText = "select * from Loachs_Terms where " + condition + "  order by displayorder asc,termid asc";

            return DataReaderToList(MYSQLHelper.ExecuteReader(cmdText));

        }
 
    

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">MySqlDataReader</param>
        /// <returns>CategoryInfo</returns>
        private static List<CategoryInfo> DataReaderToList(MySqlDataReader read)
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
