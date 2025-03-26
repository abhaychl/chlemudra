
using CHLeMudra.Api.Entity;
using Fairgaze.Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHLeMudra.Api.Repository
{

    public interface IDocumentSignHistoryRepository : IGenericAsyncRepository<DocumentSignHistory>
    {

    }
    public class DocumentSignHistoryRepository : GenericAsyncRepository<DocumentSignHistory>, IDocumentSignHistoryRepository
    {
        public DocumentSignHistoryRepository(CHLeMudraContext context) : base(context)
        {
        }
    }
   
}
