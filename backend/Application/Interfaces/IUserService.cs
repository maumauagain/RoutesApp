using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        List<UserListDTO> Get();
        bool Post(UserDTO user);
        UserListDTO GetById(int id);
        bool Put(UserUpdateDTO user);
        bool Delete(int id);
        UserAuthenticateResponseDTO Authenticate(UserAuthenticateRequestDTO user);
    }
}
