using DAL;

namespace BLL.Interface
{
    public interface IBODriver
    {
        Driver InsertDriver(Driver driver);
        bool Exists(string email);
        Driver AuthenticateCredentials(string username, string password);
    }
}
