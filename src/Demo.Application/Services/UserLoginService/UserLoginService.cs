using Demo.Application.Interfaces;
using Demo.Application.Requests.UserLogin;
using Demo.Application.Results;
using Demo.Domain.Entities;


namespace Demo.Application.Services.UserLoginService
{

    public class UserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public UserLoginService(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }
        public ResultsGeneric<UserLogin> CheckResult(UserLoginRequest request)
        {
            UserLogin? userLogin = _userLoginRepository.FindByName(request.UserName);
            if (request.UserName == null || userLogin.UserName != request.UserName)
            {
                return ResultsGeneric<UserLogin>.Fail("UserName không được để trống");
            }
            if (request.Password == null || userLogin.Password != request.Password)
            {
                return ResultsGeneric<UserLogin>.Fail("Password không được để trống");
            }
            if(userLogin.Roler == "Admin")
            {
                return ResultsGeneric<UserLogin>.Success(userLogin,"UserName không Phải Admin");
            }
            if (userLogin.Roler == "User")
            {
                return ResultsGeneric<UserLogin>.Success(userLogin, "UserName không Phải user");
            }
            return ResultsGeneric<UserLogin>.Success(userLogin, "Tim thay UserName");
        }
        public List<UserLogin> GetAll() 
        {
            return _userLoginRepository.GetAll();
        }
        public ResultsGeneric<UserLogin> AddUserLogin(AddUserLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                return ResultsGeneric<UserLogin>.Fail("UserName khong tim thay");
            }
            if (request.Password.Length == 20 || request.Password == null) 
            {
                return ResultsGeneric<UserLogin>.Fail("Pass nhieu hon 20 ky tu va khong duoc de trong");
            }
           
            UserLogin? userLoginIsActive = _userLoginRepository.FindIsActive(request.IsActive);
            if (userLoginIsActive == null) 
            {
                if (request.IsActive == true) 
                {
                    ResultsGeneric<UserLogin>.Success(userLoginIsActive,"Tai Khoan da duoc Kich Hoat");
                }
                else 
                {
                    ResultsGeneric<UserLogin>.Fail("Tai Khoan Chua duoc Kich Hoat");
                }
            }
            UserLogin? userLoginIsLock = _userLoginRepository.TimeTolock(request.IsLocked);
            if (userLoginIsLock == null)
            {
                if (request.IsLocked == true)
                {
                    ResultsGeneric<UserLogin>.Success(userLoginIsLock, "Tai Khoan da bi khoa Kich Hoat");
                }
                else
                {
                    ResultsGeneric<UserLogin>.Fail("Tai Khoan Chua bi khoa Kich Hoat");
                }
            }
            if (request.Roler == "User")
            {
                ResultsGeneric<UserLogin>.Fail("Tai Khoan cua User");
            }
            else 
            {
                
            }
            UserLogin? user = _userLoginRepository.FindById(request.Id);
            if (user == null) 
            {
                ResultsGeneric<UserLogin>.Fail("User Da ton dai");
            }
            UserLogin userLogin = new UserLogin() 
            {
                Id = request.Id,
                UserName = request.UserName,
                Password = request.Password,
                IsActive = request.IsActive,
                IsLocked = request.IsLocked,
                Roler = request.Roler,
            };
            _userLoginRepository.Add(userLogin);
            return ResultsGeneric<UserLogin>.Success(user, "Add UserLogin thanh Cong");
        }   
    }
}
