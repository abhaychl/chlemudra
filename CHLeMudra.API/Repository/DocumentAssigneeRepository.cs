
using CHLeMudra.Api.Entity;
using Fairgaze.Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHLeMudra.Api.Repository
{

    public interface IDocumentAssigneeRepository : IGenericAsyncRepository<DocumentAssignee>
    {

    }
    public class DocumentAssigneeRepository : GenericAsyncRepository<DocumentAssignee>, IDocumentAssigneeRepository
    {
        public DocumentAssigneeRepository(CHLeMudraContext context) : base(context)
        {
        }
    }
   
}
