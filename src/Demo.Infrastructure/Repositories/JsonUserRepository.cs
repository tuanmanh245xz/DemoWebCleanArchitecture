using Demo.Application.Interfaces;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class JsonUserRepository : IUserRepository
    {
        //filePath
        private readonly string _filePath = "User.json";

        public void Add(User user)
        {
            List<User> users = ReadUsers();
            //tang Id
            int newId = users.Any()
              ? users.Max(x => x.Id) + 1
              : 1;

            user.Id = newId;
            users.Add(user);
            SaveUsers(users);
        }
        public User? FindById(int id)
        {
            List<User> users = ReadUsers();
            User? user = users.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                return null;
            }

            return user;
        }
        public List<User> GetAll()
        {
            return ReadUsers();
        }
        public void Update(User user)
        {
            List<User> users = ReadUsers();
            User? Olduser = users.FirstOrDefault(x => x.Id == user.Id);
            if (Olduser == null) 
            {
                return ;
            }
            Olduser.Id = user.Id;
            Olduser.UserName = user.UserName;
            Olduser.Email = user.Email;
            Olduser.Phone = user.Phone;
            Olduser.Address = user.Address;
            Olduser.City = user.City;
            Olduser.Region = user.Region;
            Olduser.PostalCode = user.PostalCode;

            SaveUsers(users);
        }
        public void Delete(int id)
        {
            List<User> users = ReadUsers();
           User? user =  users.FirstOrDefault(x => x.Id == id);
            if(users == null)
            {
                return ;
            }
            users.Remove(user);
            SaveUsers(users);
        }
        private List<User> ReadUsers()
        {
            if (!File.Exists(_filePath))
            {
                return CreateDefaultUsers();

            }
            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<User>();
            }
            List<User>? users = JsonSerializer.Deserialize<List<User>>(json);
            return users?? new List<User>();
        }
        public void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            File.WriteAllText(_filePath, json);
        }
        private List<User> CreateDefaultUsers()
        {
            List<User> users = new() 
            {
                new User
                {
                    Id = 1,
                    UserName = "Test",
                    Email = "test@gmail.com",
                    Phone = 0868143729,
                    Address = "Ha Noi",
                    City = "Ha Noi",
                    Region = "Viet Nam",
                    PostalCode = "10000N",
                },
                 new User
                {
                    Id = 2,
                    UserName = "Test2",
                    Email = "test@gmail.com",
                    Phone = 0868143729,
                    Address = "Ha Noi",
                    City = "Ha Noi",
                    Region = "Viet Nam",
                    PostalCode = "10000N",
                },
                  new User
                {
                    Id = 3,
                    UserName = "Test3",
                    Email = "test@gmail.com",
                    Phone = 0868143729,
                    Address = "Ha Noi",
                    City = "Ha Noi",
                    Region = "Viet Nam",
                    PostalCode = "10000N",
                },

            };
            SaveUsers(users);
            return users;
        }
    }
}
