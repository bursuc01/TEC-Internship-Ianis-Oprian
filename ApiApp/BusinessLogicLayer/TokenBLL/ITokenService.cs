using ApiApp.DataAccessLayer.Model;

namespace ApiApp.BusinessLogicLayer.TokenBLL
{
    public interface ITokenService
    {
        string CreateTokenOptions(User user);
        public string Decrypt(string data, int key);  
     }
}
