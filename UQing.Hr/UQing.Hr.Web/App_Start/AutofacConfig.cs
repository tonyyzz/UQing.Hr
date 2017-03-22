using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UQing.Hr.Web
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Reflection;
    using System.Web.Mvc;
	using UQing.Hr.Common;

    public class AutofacConfig
    {
        /// <summary>
        /// 负责调用Autofac框架实现业务逻辑层和数据仓储层程序集中的类型对象的创建
        /// 负责创建MVC控制器类的对象（调用控制器中的有参构造函数），接管DefaultControllerFactory的工作
        /// </summary>
        public static void Register()
        {
            //1.0 实例化一个Autofac的创建容器
            var builder = new ContainerBuilder();

            //2.0 利用反射，告诉Autofac框架，将来要创建的控制器类存放在哪个程序集（Mall.Site）
			Assembly controllerAssembly = Assembly.Load("UQing.Hr.Web");
            builder.RegisterControllers(controllerAssembly);

            //3.0 告诉Autofac框架注册数据仓储层所在程序集中的所有类中的对象实例
			Assembly repositoryAssembly = Assembly.Load("UQing.Hr.Repository");
            // 创建repositoryAssembly中的所有类的实例以此类的实现接口存储
            builder.RegisterTypes(repositoryAssembly.GetTypes()).AsImplementedInterfaces();

            //4.0 告诉Autofac框架注册业务逻辑层所在程序集中的所有类中的对象实例
			Assembly servicesAssembly = Assembly.Load("UQing.Hr.Services");
            // 创建servicesAssembly中的所有类的实例以此类的实现接口存储
            builder.RegisterTypes(servicesAssembly.GetTypes()).AsImplementedInterfaces();

            //5.0 创建一个Autofac的容器
            var container = builder.Build();
            //5.0.1 将container对象缓存到HttpRuntime.Cache中，并且是永久有效
            CacheHelper.SetData(Keys.AutofacContainer, container);

            //6.0 将MVC的控制器对象实例，交给Autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}