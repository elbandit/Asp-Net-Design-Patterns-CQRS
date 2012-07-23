using Agathas.Storefront.Infrastructure.Repositories;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Agathas.Storefront.Specs.Uat.Support
{
  public  class TestNHConfigurator : ISessionCoordinator
  {

    private const string CONN_STR = "Data Source=:memory:;Version=3;New=True;";

    private static readonly Configuration _configuration;
    private static readonly ISessionFactory _sessionFactory;

    static TestNHConfigurator()
    {

      _configuration = new Configuration().Configure()
        .DataBaseIntegration(db =>
        {
          db.Dialect<SQLiteDialect>();
          db.Driver<SQLite20Driver>();
          db.ConnectionProvider<TestConnectionProvider>();
          db.ConnectionString = CONN_STR;
        }) 
        .SetProperty(Environment.CurrentSessionContextClass,
          "thread_static");
      
      var props = _configuration.Properties;
      if (props.ContainsKey(Environment.ConnectionStringName))
        props.Remove(Environment.ConnectionStringName);

      _sessionFactory = _configuration.BuildSessionFactory();
    }

    public static Configuration Configuration
    {
      get
      {
        return _configuration;
      }
    }

    public static ISessionFactory SessionFactory
    {
      get
      {
        return _sessionFactory;
      }
    }

      public ISession get_current_session()
      {
          return SessionFactory.GetCurrentSession();
      }
  }

}
