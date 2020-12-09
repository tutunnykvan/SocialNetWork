using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDTO;

namespace MongoDal.Interface
{
    public interface IUserDal
    {
        UserDTO CreateUser(UserDTO user);

        List<UserDTO> GetAllUsers();

        void Delete(int id);

        bool Login(string Login, string Password);

        UserDTO GetById(int id);

        UserDTO GetByLogin(string login);
               
        UserDTO UpdateUser(UserDTO user);

    }
}
