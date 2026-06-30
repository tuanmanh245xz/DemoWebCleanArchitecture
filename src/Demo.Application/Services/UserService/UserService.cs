using Demo.Application.Interfaces;
using Demo.Application.Requests.Users;
using Demo.Application.Results;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Services.UserService
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResultsGeneric<User> CheckResult(int id)
        {
            if (id == 0)
            {
                return ResultsGeneric<User>.Fail("Khong Tim thay ");
            }
            User? user = _userRepository.FindById(id);
            if (user == null)
            {
                return ResultsGeneric<User>.Fail("Khong tim thay User");
            }
            return ResultsGeneric<User>.Success(user, "Tim thay User");
        }
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public ResultsGeneric<User> AddUser(AddUserRequest request)
        {
          
            if (string.IsNullOrEmpty(request.UserName))
            {
                return ResultsGeneric<User>.Fail("Ten khong duoc de trong");
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                return ResultsGeneric<User>.Fail("Email phai dung format va khong duoc de trong");

            }

            if (request.Phone > 12)
            {
                return ResultsGeneric<User>.Fail("Phai la 12 chua so");
            }
            if (string.IsNullOrWhiteSpace(request.Address))
            {
                return ResultsGeneric<User>.Fail("Dia chi khong duoc de trong");
            }
            if (string.IsNullOrWhiteSpace(request.City) && string.IsNullOrWhiteSpace(request.Region))
            {
                return ResultsGeneric<User>.Fail("City va Region not null");
            }
            if (string.IsNullOrWhiteSpace(request.PostalCode))
            {
                return ResultsGeneric<User>.Fail("10 ky tu khong duoc de trong");
            }
            //kiem tra xem da ton tai chua
            User? user = _userRepository.FindById(request.Id);
            if (user == null)
            {
                return ResultsGeneric<User>.Fail("User chua ton tai");
            }
            User users = new User()
            {
                Id = request.Id,
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
            };
            _userRepository.Add(users);
            return ResultsGeneric<User>.Success(user, "Add User Thanh cong");
        }
        
        public ResultsGeneric<User> UpdateUser(UpdateUserRequest request)
        {
            //if (request.Id == 0) 
            //{
            //    return ResultsGeneric<User>.Fail("Not found User");
            //}
            if (string.IsNullOrEmpty(request.UserName))
            {
                return ResultsGeneric<User>.Fail("Ten khong duoc de trong");
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                return ResultsGeneric<User>.Fail("Email phai dung format va khong duoc de trong");

            }
            
            if(request.Phone > 12)
            {
                return ResultsGeneric<User>.Fail("Phai la 12 chua so");
            }
            if (string.IsNullOrWhiteSpace(request.Address))
            {
                return ResultsGeneric<User>.Fail("Dia chi khong duoc de trong");
            }
            if (string.IsNullOrWhiteSpace(request.City) && string.IsNullOrWhiteSpace(request.Region)) 
            {
                return ResultsGeneric<User>.Fail("City va Region not null");
            }
            if (string.IsNullOrWhiteSpace(request.PostalCode))
            {
                return ResultsGeneric<User>.Fail("10 ky tu khong duoc de trong");
            }
            User? user = _userRepository.FindById(request.Id);
            if (user == null) 
            {
                return ResultsGeneric<User>.Fail("Khong tim thay User");
            }
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.Address = request.Address;
            user.City = request.City;
            user.Region = request.Region;
            user.PostalCode = request.PostalCode;
            _userRepository.Update(user);

            return ResultsGeneric<User>.Success(user,"Update User Thanh cong");
        }
        public ResultsGeneric<User> DeleteUser(DeleteUserRequest request)
        {
            if(request.Id == 0)
            {
                return ResultsGeneric<User>.Fail("khong tim thay Id");
            }
            User? user = _userRepository.FindById(request.Id);
            if (user == null)
            {
                return ResultsGeneric<User>.Fail("Khong co User de Delete");
            }
            _userRepository.Delete(request.Id);
            return ResultsGeneric<User>.Success(user, "Xoa Thanh Cong User");
        }
    }
}
