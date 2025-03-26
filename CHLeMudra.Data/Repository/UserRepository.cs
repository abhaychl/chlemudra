using CHLeMudra.Data;
using CHLeMudra.Entity;

using System;
using System.Data.Entity;
using System.Linq;

namespace CHLeMudra.Data
{
    public interface IUserRepository : IGenericRepository<Usermaster>
    {

        Usermaster DoLogin(string userName, string PWD);
        Usermaster GetUser(int UserId);
        TokenManagement TokenManagement(int UserId);
        TokenManagement GetTokenDetails(int UserId);
        bool IsEmailExist(string userName);
    }
    public class UserRepository : GenericRepository<DataBaseContext, Usermaster>, IUserRepository/*, IGenericRepository<Usermaster>*/
    {
        private DataBaseContext _entities;
        public UserRepository()
        {
            _entities = new DataBaseContext();
        }
        public Usermaster DoLogin(string userName, string PWD)
        {
            var LoginDetails = _entities.UserMasters.Where(x => x.UserName == userName.Trim() && x.Password == PWD.Trim()).FirstOrDefault();
            return LoginDetails;
        }
        public bool IsEmailExist(string userName)
        {
            var user = _entities.UserMasters.Where(x => x.UserName.ToLower() == userName.ToLower().Trim() && x.IsDeleted == false).FirstOrDefault();
            return user == null ? false : true;
        }
        public TokenManagement GetTokenDetails(int UserId)
        {
            return _entities.TokenManagements.Where(x => x.UserId == UserId && x.Status == true).FirstOrDefault();
        }

        public TokenManagement TokenManagement(int UserId)
        {
            DateTime ExpiresOn = DateTime.Now.AddMinutes(2000);
            
            String Token1 = Guid.NewGuid().ToString().Substring(0,8); 
            Token1 = CommonHelper.Encrypt(UserId + "|" + Token1);
            var TokenDetails = _entities.TokenManagements.Where(x => x.UserId == UserId && x.Status == true).FirstOrDefault();
            if (TokenDetails != null)
            {
                TokenDetails.Token = Token1;
                TokenDetails.Status = true;
                TokenDetails.ModifiedOn = DateTime.Now;
                _entities.Entry(TokenDetails).State = EntityState.Modified;
                _entities.SaveChanges();
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
                _entities.TokenManagements.Add(objToken);
                _entities.SaveChanges();
                return objToken;
            }
        }

        public Usermaster GetUser(int UserId)
        {
            return _entities.UserMasters.FirstOrDefault(c => c.IsDeleted == false && c.Status == true && c.Id == UserId);
        }

    }
}
