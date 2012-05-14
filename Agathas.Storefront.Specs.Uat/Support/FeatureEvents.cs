using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using TechTalk.SpecFlow;

namespace Agathas.Storefront.Specs.Uat.Support
{
    [Binding]
    public class FeatureEvents
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            BootStrapper.configure_dependencies();
        }

        [BeforeScenario]
        public static void BeforeSceanrio()
        {
            var cfg = TestNHConfigurator.Configuration;
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(false, true);

            var session = TestNHConfigurator.SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }


        [AfterScenario]
        public static void AfterScenario()
        {
            TestConnectionProvider.CloseDatabase();

            var sessionFactory = TestNHConfigurator.SessionFactory;
            var session = CurrentSessionContext.Unbind(sessionFactory);
            session.Close();
        }
    }
}
