using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UQing.Hr.IServices
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        #region 2.0 查询相关方法

        /// <summary>
        /// （基本查询）根据lambda表达式进行查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// （Join连表查询）连表查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="tableNames">表名称组</param>
        /// <returns></returns>
        List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames);

        /// <summary>
        /// （升序排列）按照条件查询出数据后，根据外部指定的字段进行升序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        List<TEntity> QueryOrderByAsc<TKey>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TKey>> order);

        /// <summary>
        /// （降序排列）按照条件查询出数据后，根据外部指定的字段进行降序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        List<TEntity> QueryOrderByDesc<TKey>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TKey>> order);

        /// <summary>
        /// （分页）分页方法
        /// </summary>
        /// <typeparam name="TKey">要指定的排序属性名称（ETntity.Property）</typeparam>
		/// <param name="pageIndex">分页页码（1：表示第一页）</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="where">排序条件lambda表达式</param>
        /// <param name="order">查询条件lambda表达式</param>
        /// <returns></returns>
		List<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int pageCount, out int rowCount,
            Expression<Func<TEntity, bool>> where,
			Expression<Func<TEntity, TKey>> order, bool isDesc = true);

		/// <summary>
		/// （分页）分页方法（多条件排序）
		/// </summary>
		/// <typeparam name="TKey">要指定的排序属性名称（ETntity.Property）</typeparam>
		/// <param name="pageIndex">分页页码（1：表示第一页）</param>
		/// <param name="pageSize">页容量</param>
		/// <param name="pageCount">页总量</param>
		/// <param name="rowCount">总行数</param>
		/// <param name="where">查询条件lambda表达式</param>
		/// <param name="orderConditions">排序条件lambda表达式集合（包含是否是倒序）</param>
		/// <returns></returns>
		List<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int pageCount, out int rowCount,
			Expression<Func<TEntity, bool>> where,
			List<UQing.Hr.Model.Common.PageOrderCondition<TEntity, TKey>> orderConditions);
        #endregion

        #region 3.0 修改相关方法
        void Edit(TEntity model, string[] properties);

        #endregion

        #region 4.0 删除相关方法
        void Delete(TEntity model, bool isAdded);

        #endregion

        #region 5.0 新增相关方法
        void Add(TEntity model);

        #endregion

        #region 6.0 统一提交
        int SaveChanges();

        #endregion

        #region 7.0 调用存储过程

        /// <summary>
        /// （调用存储过程）调用存储过程，返回一个自己指定的类型 TResult
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<TResult> RunProc<TResult>(string sql, params object[] parameters);

        #endregion
    }
}
