using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    class ApplicationContextInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            //Для удобства обновления использовал метод Seed в миграциях, так как использовал несколько рабочих баз
            /*SearchConnector s1 = new SearchConnector { Name = "Google", ResultNamePattern = "g s", ResultBodyPattern = "g h" };
            db.SearchConnector.Add(s1);
            db.SaveChanges();*/
        }
    }
}
