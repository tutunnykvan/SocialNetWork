using BusinessLogic.Concrete;
using BusinessLogic.Interface;
using MongoDal.Concrete;
using MongoDal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using SocialConsoleApp.AppUser;
using SocialConsoleApp.Menu;
using Neo4jDal.Concrete;
using Neo4jDal.Interface;

namespace SocialConsoleApp
{
    class ProgramApp
    {
        public static UnityContainer Container;
        public static string _connString;
        public static string DataName;

        public static string _connNeo4j, login, pass;
        static void Main()
        {
            _connString = "mongodb://localhost:27017/";
            DataName = "SocialNetwork";
            _connNeo4j = "http://localhost:7474/db/data/";
            login = "login";
            pass = "1111";
            ConfigureContainer();


            AppMenu.ShowEntry();

        }

        private static void ConfigureContainer()
        {
            Container = new UnityContainer();
            Container.RegisterInstance<IAppUser>(new SocialConsoleApp.AppUser.AppUser());
            Container.RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IPostManager, PostManager>()
                .RegisterType<IUserManager, UserManager>();
            Container
                .RegisterType<IUserDal, UserDal>(new InjectionConstructor(_connString,DataName))
                .RegisterType<IPostDal, PostDal>(new InjectionConstructor(_connString, DataName));
            Container
              .RegisterType<IFollowerDal, FollowerDal>(new InjectionConstructor(_connNeo4j, _login, _pass));
        }
    }
}
