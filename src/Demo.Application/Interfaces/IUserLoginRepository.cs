using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface IUserLoginRepository
    {
        UserLogin? FindById(int id);
        UserLogin? FindByName(string name);
        UserLogin? FindIsActive(bool isActive);
        UserLogin? TimeTolock(bool IsLocked);
        void Add(UserLogin user);
        List<UserLogin> GetAll();
        void Update(UserLogin user);
        void Delete(int id);

    }
}
