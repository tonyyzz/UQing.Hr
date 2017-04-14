using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UQing.Hr.Repository
{
	using System.Data.Entity;
	using System.Linq.Expressions;
	using System.Data.Entity.Infrastructure;
	using UQing.Hr.IRepository;
	using System.Runtime.Remoting.Messaging;

	/// <summary>
	/// 统一父类，负责所有表的ERUD操作、分页、排序、连表
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		//1.0 实例化EF上下文对象类
		//缺点：如果一个控制器中有多个服务接口，则会在当前请求线程中产生响应个数的EF容器对象，
		//造成每次都要使用此业务逻辑对应的EF容器来进行数据库的访问，容易出错，并且性能会降低
		//BaseDbContext _dbContext = new BaseDbContext();

		//为了解决1.0步骤中的缺陷，则应该使用线程缓存来存储当前线程中的EF容器对象，保证雌线程EF容器对象唯一，
		//同时，此线程销毁后，EF容器跟着销毁
		private BaseDbContext _dbContext
		{
			get
			{
				//1.0 线从线程缓存 CallContext 中根据Key查找EF容器对象，如果没有则创建，同事保存到缓存中
				object obj = CallContext.GetData("BaseDbContext"); //指定Key
				if (obj == null)
				{
					//1.0.1 实例化EF的上下文容器对象
					obj = new BaseDbContext();
					//1.0.2 将EF容器对象存入线程缓存 CallContext 中
					CallContext.SetData("BaseDbContext", obj);
				}
				return obj as BaseDbContext;
			}
		}


		DbSet<TEntity> _dbSet;
		public BaseRepository()
		{
			_dbSet = _dbContext.Set<TEntity>();
		}

		#region 2.0 查询相关方法

		/// <summary>
		/// （基本查询）根据lambda表达式进行查询
		/// </summary>
		/// <param name="where">查询条件</param>
		/// <returns></returns>
		public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
		{
			/*
			 * 使用MVC4+EF5开发项目时，做增删改查的时候经常会出现操作失败的问题，
			 * 提示ObjectStateManager 无法跟踪具有相同键的多个对象。
			 * 信息，经过几天的跟踪测试和网上查找一些相关资料发现是EF的缓存问题，在对数据集进行增删改查的时候，
			 * EF会把查询的对象缓存到DbContext中，所以当我们在将需要操作的对象附加到上下文中时就会出现冲突，
			 * 出现此问题。
			 * 比如更新一个实体，如果之前进行过查询操作，EF就会缓存查询的实体，再进行更新时，
			 * 先把要更新的实体附加到上下文，然后再标记为Modified状态，这时会出现上述问题
			 * 
			 * 解决方案是：在查询的时候使用  DbContext.AsNoTracking().Where(f => true).ToList();  查询
			 *  摘要:  
			 *      返回一个新查询，其中返回的实体将不会在 System.Data.Entity.DbContext 中进行缓存。  
			 *      返回结果:  
			 *      应用了 NoTracking 的新查询。  
			 *      public DbQuery<TResult> AsNoTracking();  
			 */

			//return _dbSet.Where(where).ToList();
			if (where == null)
			{
				return _dbSet.AsNoTracking().ToList();
			}
			else
			{
				return _dbSet.AsNoTracking().Where(where).ToList();
			}
		}

		/// <summary>
		/// （Join连表查询）连表查询
		/// </summary>
		/// <param name="where">查询条件</param>
		/// <param name="tableNames">表名称组</param>
		/// <returns></returns>
		public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
		{
			if (tableNames == null || !tableNames.Any() == false)
			{
				throw new Exception("连表操作的表名称至少要有一个");
			}
			DbQuery<TEntity> query = _dbSet;
			foreach (var tableName in tableNames)
			{
				query.Include(tableName);
			}
			return query.Where(where).ToList();
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
			if (where == null)
			{
				return _dbSet.OrderBy(order).ToList();
			}
			else
			{
				return _dbSet.Where(where).OrderBy(order).ToList();
			}
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
			if (where == null)
			{
				return _dbSet.OrderByDescending(order).ToList();
			}
			else
			{
				return _dbSet.Where(where).OrderByDescending(order).ToList();
			}
		}

		/// <summary>
		/// （分页）分页方法
		/// </summary>
		/// <typeparam name="TKey">要指定的排序属性名称（ETntity.Property）</typeparam>
		/// <param name="pageIndex">分页页码（1：表示第一页）</param>
		/// <param name="pageSize">页容量</param>
		/// <param name="rowCount">总行数</param>
		/// <param name="where">查询条件lambda表达式</param>
		/// <param name="order">排序条件lambda表达式</param>
		/// <param name="isDesc">排序方式（默认为倒序）</param>
		/// <returns></returns>
		public List<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int pageCount, out int rowCount,
			Expression<Func<TEntity, bool>> where,
			Expression<Func<TEntity, TKey>> order, bool isDesc = true)
		{
			int skipCount = (pageIndex - 1) * pageSize;
			if (where == null)
			{
				rowCount = _dbSet.Count();
			}
			else
			{
				rowCount = _dbSet.Count(where);
			}
			pageCount = Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / pageSize));
			if (isDesc)
			{
				if (where == null)
				{
					return _dbSet.OrderByDescending(order).Skip(skipCount).Take(pageSize).ToList();
				}
				else
				{
					return _dbSet.Where(where).OrderByDescending(order).Skip(skipCount).Take(pageSize).ToList();
				}
			}
			else
			{
				if (where == null)
				{
					return _dbSet.OrderBy(order).Skip(skipCount).Take(pageSize).ToList();
				}
				else
				{
					return _dbSet.Where(where).OrderBy(order).Skip(skipCount).Take(pageSize).ToList();
				}
			}
		}

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
		public List<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int pageCount, out int rowCount,
			Expression<Func<TEntity, bool>> where,
			List<UQing.Hr.Model.Common.PageOrderCondition<TEntity, TKey>> orderConditions)
		{
			if (!orderConditions.Any())
			{
				throw new Exception("order条件不能为空");
			}
			int skipCount = (pageIndex - 1) * pageSize;
			if (where == null)
			{
				rowCount = _dbSet.Count();
			}
			else
			{
				rowCount = _dbSet.Count(where);
			}
			pageCount = Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / pageSize));
			IQueryable<TEntity> query = null;
			if (where != null)
			{
				query = _dbSet.Where(where);
			}
			if (orderConditions.Count == 1)
			{
				if (orderConditions.First().IsDesc)
				{
					if (query == null)
					{
						query = _dbSet.OrderByDescending(orderConditions.First().order);
					}
					else
					{
						query = query.OrderByDescending(orderConditions.First().order);
					}
				}
				else
				{
					if (query == null)
					{
						query = _dbSet.OrderBy(orderConditions.First().order);
					}
					else
					{
						query = query.OrderBy(orderConditions.First().order);
					}
				}
			}
			else
			{
				if (orderConditions.First().IsDesc)
				{
					if (query == null)
					{
						query = _dbSet.OrderByDescending(orderConditions.First().order);
					}
					else
					{
						query = query.OrderByDescending(orderConditions.First().order);
					}
				}
				else
				{
					if (query == null)
					{
						query = _dbSet.OrderBy(orderConditions.First().order);
					}
					else
					{
						query = query.OrderBy(orderConditions.First().order);
					}
				}
				foreach (var orderItem in orderConditions.Skip(1))
				{
					if (orderItem.IsDesc)
					{
						query = query.OrderByDescending(orderItem.order);
					}
					else
					{
						query = query.OrderBy(orderItem.order);
					}
				}
			}
			query = query.Skip(skipCount).Take(pageSize);
			return query.ToList();
		}

		#endregion

		#region 3.0 修改相关方法
		public void Edit(TEntity model, string[] properties)
		{
			if (model == null)
			{
				throw new Exception("实体不能为空");
			}
			if (properties.Any() == false)
			{
				throw new Exception("要修改的属性至少要有一个");
			}

			// 将model追加到EF容器中
			DbEntityEntry entry = _dbContext.Entry(model);
			entry.State = System.Data.EntityState.Unchanged;
			foreach (string property in properties)
			{
				entry.Property(property).IsModified = true;
			}

			// 关闭EF对于实体的合法性验证
			_dbContext.Configuration.ValidateOnSaveEnabled = false;
		}
		#endregion

		#region 4.0 删除相关方法
		public void Delete(TEntity model, bool isAdded)
		{
			if (isAdded == false) // 表示model没有追加到EF容器中
			{
				_dbSet.Attach(model);
			}
			_dbSet.Remove(model);
		}
		#endregion

		#region 5.0 新增相关方法
		public void Add(TEntity model)
		{
			_dbSet.Add(model);
		}
		#endregion

		#region 6.0 统一提交
		public int SaveChanges()
		{
			return _dbContext.SaveChanges();
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
			return _dbContext.Database.SqlQuery<TResult>(sql, parameters).ToList();
		}

		#endregion
	}
}
