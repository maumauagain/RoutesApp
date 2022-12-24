using Application.DTO;
using Application.Interfaces;
using Auth.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public List<UserListDTO> Get()
        {
            var users = _userRepository.GetAll();
            List<UserListDTO> userDto = new List<UserListDTO>();
            userDto = _mapper.Map<List<UserListDTO>>(users);

            return userDto;
        }

        public bool Post(UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email))
                return false;

            User _user = this._userRepository.Find(u => u.Email == userDto.Email && u.IsActive);
            if (_user is null)
            {
                User user = _mapper.Map<User>(userDto);
                user.Password = EncryptPassword(user.Password);
                _userRepository.Create(user);

            }
            
            return true;
        }

        public UserListDTO GetById(int id)
        {
            User _user = this._userRepository.Find(u => u.Id == id && u.IsActive);

            if (_user is null)
                throw new Exception("User Not found");

            return _mapper.Map<UserListDTO>(_user);
        }

        public bool Put(UserUpdateDTO user)
        {
            User _user = this._userRepository.Find(u => u.Id == user.Id && u.IsActive);
            if (_user is null)
                throw new Exception("User Not found");

            User _userUpdate = _mapper.Map<User>(user);
            _userUpdate.Email = _user.Email;
            _userUpdate.CreatedOn = _user.CreatedOn;
            _userUpdate.Password = _user.Password;
            _userUpdate.IsActive = _user.IsActive;
            //user.Password = EncryptPassword(user.Password);

            _userRepository.Update(_userUpdate);

            return true;
        }

        public bool Delete(int id)
        {
            User _user = this._userRepository.Find(u => u.Id == id && u.IsActive);
            if (_user is null)
                throw new Exception("User Not found");

            _userRepository.Delete(_user);

            return true;
        }

        public UserAuthenticateResponseDTO Authenticate(UserAuthenticateRequestDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("Email or Password shouldn't be empty");

            user.Password = EncryptPassword(user.Password);

            User _user = this._userRepository.Find(u => u.Email.ToLower() == user.Email.ToLower() && u.IsActive && u.Password == user.Password);
            if (_user is null)
                throw new Exception("User Not found");

            var token = new TokenService().GenerateToken(_user);
            var userDTO = _mapper.Map<UserDTO>(_user);
            return new UserAuthenticateResponseDTO(userDTO, token);
        }

        private string EncryptPassword(string password)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();

            foreach(var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }

    }
}
