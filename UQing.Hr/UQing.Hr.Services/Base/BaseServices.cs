using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.Services
{
    using System.Linq.Expressions;
	using UQing.Hr.IRepository;
	using UQing.Hr.IServices;

    public class BaseServices<TEntity> where TEntity : class
    {
        // 1.0 定义数据仓储的接口
        protected IBaseRepository<TEntity> _baseDal;

        #region 2.0 查询相关方法

        /// <summary>
        /// （基本查询）根据lambda表达式进行查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
            return _baseDal.QueryWhere(where);
        }

        /// <summary>
        /// （Join连表查询）连表查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="tableNames">表名称组</param>
        /// <returns></returns>
        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            return _baseDal.QueryJoin(where, tableNames);
        }

        /// <summary>
        /// （升序排列）按照条件查询出数据后，根据外部指定的字段进行升序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> QueryOrderByAsc<TKey>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TKey>> order)
        {
            return _baseDal.QueryOrderByAsc(where, order);
        }

        /// <summary>
        /// （降序排列）按照条件查询出数据后，根据外部指定的字段进行降序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> QueryOrderByDesc<TKey>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TKey>> order)
        {
            return _baseDal.QueryOrderByDesc(where, order);
        }

        /// <summary>
        /// （分页）分页方法
        /// </summary>
        /// <typeparam name="TKey">要指定的排序属性名称（ETntity.Property）</typeparam>
        /// <param name="pageIndex">分页页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="where">排序条件lambda表达式</param>
        /// <param name="order">查询条件lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int rowCount,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TKey>> order)
        {
            return _baseDal.QueryByPage(pageIndex, pageSize, out rowCount, where, order);
        }

        #endregion

        #region 3.0 修改相关方法
        public void Edit(TEntity model, string[] properties)
        {
            _baseDal.Edit(model, properties);
        }
        #endregion

        #region 4.0 删除相关方法
        public void Delete(TEntity model, bool isAdded)
        {
            _baseDal.Delete(model, isAdded);
        }
        #endregion

        #region 5.0 新增相关方法
        public void Add(TEntity model)
        {
            _baseDal.Add(model);
        }
        #endregion

        #region 6.0 统一提交
        public int SaveChanges()
        {
            return _baseDal.SaveChanges();
        }
        #endregion

        #region 7.0 调用存储过程
        /// <summary>
        /// （调用存储过程）调用存储过程，返回一个自己指定的类型 TResult
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<TResult> RunProc<TResult>(string sql, params object[] parameters)
        {
            return _baseDal.RunProc<TResult>(sql, parameters);
        }
        #endregion
    }
}
