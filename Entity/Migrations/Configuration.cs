namespace Entity.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Entity.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Entity.ApplicationContext context)
        {
            context.Set<SearchConnector>().AddOrUpdate(new SearchConnector
            {
                Id = 1,
                Name = "Google",
                QueryPattern = "https://www.google.ru/search?q=%query%",
                ResultNamePattern = ".r a",
                ResultBodyPattern = ".s .st",
                ResultLinkPattern = ".r a",
                ResultPattern = ".g",
                DirectLink = "https://www.google.ru"
            });
            context.Set<SearchConnector>().AddOrUpdate(new SearchConnector
            {
                Id = 2,
                Name = "Yandex",
                QueryPattern = "https://yandex.ru/search/?text=%query%",
                ResultNamePattern = ".organic__title-wrapper h2 a",
                ResultBodyPattern = ".extended-text",
                ResultLinkPattern = ".r a",
                ResultPattern = ".serp-item",
                DirectLink = ""
            });
            context.Set<SearchConnector>().AddOrUpdate(new SearchConnector
            {
                Id = 3,
                Name = "Bing",
                QueryPattern = "https://www.bing.com/search?q=%query%",
                ResultNamePattern = "h2 a",
                ResultBodyPattern = ".b_caption p",
                ResultLinkPattern = "h2 a",
                ResultPattern = ".b_algo",
                DirectLink = ""
            });
        }
    }
}
