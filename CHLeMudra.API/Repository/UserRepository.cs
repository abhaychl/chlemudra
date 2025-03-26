
using CHLeMudra.Api.Entity;
using Fairgaze.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;

using System.Linq;

namespace CHLeMudra.Api.Repository
{
    public interface IUserRepository : IGenericAsyncRepository<Usermaster>
    {

        Usermaster DoLogin(string userName, string PWD);
        Usermaster GetUser(int UserId);
        TokenManagement TokenManagement(int UserId);
        TokenManagement GetTokenDetails(int UserId);
        bool IsEmailExist(string userName);
    }
    public class UserRepository : GenericAsyncRepository<Usermaster>, IUserRepository
    {
       
        public UserRepository(CHLeMudraContext context) : base(context)
        {
          
        }
        public Usermaster DoLogin(string userName, string PWD)
        {
            var LoginDetails = _context.Usermasters.Where(x => x.UserName == userName.Trim() && x.Password == PWD.Trim()).FirstOrDefault();
            return LoginDetails;
        }
        public bool IsEmailExist(string userName)
        {
            var user = _context.Usermasters.Where(x => x.UserName.ToLower() == userName.ToLower().Trim() && x.IsDeleted == false).FirstOrDefault();
            return user == null ? false : true;
        }
        public TokenManagement GetTokenDetails(int UserId)
        {
            return _context.TokenManagements.Where(x => x.UserId == UserId && x.Status == true).FirstOrDefault();
        }

        public TokenManagement TokenManagement(int UserId)
        {
            DateTime ExpiresOn = DateTime.Now.AddMinutes(2000);
            
            String Token1 = Guid.NewGuid().ToString().Substring(0,8); 
           // Token1 = CommonHelper.Encrypt(UserId + "|" + Token1);
            var TokenDetails = _context.TokenManagements.Where(x => x.UserId == UserId && x.Status == true).FirstOrDefault();
            if (TokenDetails != null)
            {
                TokenDetails.Token = Token1;
                TokenDetails.Status = true;
                TokenDetails.ModifiedOn = DateTime.Now;
                _context.Entry(TokenDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return TokenDetails;
            }
            else
            {
                TokenManagement objToken = new TokenManagement();
                objToken.UserId = UserId;
                objToken.Token = Token1;
                objToken.Status = true;
                objToken.CreatedOn = DateTime.Now;
                objToken.ModifiedOn = DateTime.Now;
                _context.TokenManagements.Add(objToken);
                _context.SaveChanges();
                return objToken;
            }
        }

        public Usermaster GetUser(int UserId)
        {
            return _context.Usermasters.FirstOrDefault(c => c.IsDeleted == false && c.Status == true && c.Id == UserId);
        }

    }
}
