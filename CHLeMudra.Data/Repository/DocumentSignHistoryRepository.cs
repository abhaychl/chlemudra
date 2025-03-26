using CHLeMudra.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHLeMudra.Data
{

    public interface IDocumentSignHistoryRepository : IGenericRepository<DocumentSignHistory>
    {

    }
    public class DocumentSignHistoryRepository : GenericRepository<DataBaseContext, DocumentSignHistory>, IDocumentSignHistoryRepository
    {
        private DataBaseContext _entities;
       // private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        public DocumentSignHistoryRepository()
        {
            _entities = new DataBaseContext();
        }



    }
   
}
