using Models;

namespace Assignment.Data {
public interface IUserService {
    User ValidateUser(string userName, string password);
}
}