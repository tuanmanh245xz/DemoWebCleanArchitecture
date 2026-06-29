using Demo.Domain.Entities;

namespace Demo.Application.Interfaces
{
    public interface IUserRepository
    {
        User? FindById(int id);
        void Add(User user);
        List<User> GetAll();
        void Update(User user);
        void Delete(int id);

    }
}
