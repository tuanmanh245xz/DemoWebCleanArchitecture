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
        public ResultsGeneric<UserLogin> CheckResult(int id)
        {
            if (id == 0)
            {
                return ResultsGeneric<UserLogin>.Fail("Khong tim thay User");
            }
            UserLogin? userLogin = _userLoginRepository.FindById(id);
            if (userLogin == null)
            {
                return ResultsGeneric<UserLogin>.Fail("khong tim thay User Name");
            }
            return ResultsGeneric<UserLogin>.Success(userLogin, "Tim thay UserNam");
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
