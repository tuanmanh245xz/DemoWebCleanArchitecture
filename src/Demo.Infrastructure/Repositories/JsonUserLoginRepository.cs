using Demo.Application.Interfaces;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.Infrastructure.Repositories
{
    public class JsonUserLoginRepository : IUserLoginRepository
    {
        //filePath
        private readonly string _filePath = "UserLogin.json";

        public void Add(UserLogin user)
        {
            List<UserLogin> users = ReadUsers();
            //tang Id
            int newId = users.Any()
              ? users.Max(x => x.Id) + 1
              : 1;

            user.Id = newId;
            users.Add(user);
            SaveUsers(users);
        }
        public UserLogin? FindById(int id)
        {
            List<UserLogin> users = ReadUsers();
            UserLogin? user = users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }

            return user;
        }
        public List<UserLogin> GetAll()
        {
            return ReadUsers();
        }
        public void Update(UserLogin user)
        {
            List<UserLogin> users = ReadUsers();
            UserLogin? Olduser = users.FirstOrDefault(x => x.Id == user.Id);
            if (Olduser == null)
            {
                return;
            }
                Olduser.Id = user.Id;
                Olduser.UserName = user.UserName;
                Olduser.IsActive = user.IsActive;
                Olduser.IsLocked = user.IsLocked;
                Olduser.Roler = user.Roler;

            SaveUsers(users);
        }
        public void Delete(int id)
        {
            List<UserLogin> users = ReadUsers();
            UserLogin? user = users.FirstOrDefault(x => x.Id == id);
            if (users == null)
            {
                return;
            }
            users.Remove(user);
            SaveUsers(users);
        }
        private List<UserLogin> ReadUsers()
        {
            if (!File.Exists(_filePath))
            {
                return CreateDefaultUserLogin();

            }
            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<UserLogin>();
            }
            List<UserLogin>? users = JsonSerializer.Deserialize<List<UserLogin>>(json);
            return users ?? new List<UserLogin>();
        }
        public void SaveUsers(List<UserLogin> userLogin)
        {
            string json = JsonSerializer.Serialize(userLogin, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            File.WriteAllText(_filePath, json);
        }
        private List<UserLogin> CreateDefaultUserLogin()
        {
            List<UserLogin> userLogin = new()
            {
                new UserLogin
                {
                    Id = 1,
                    UserName = "Test",
                    Password = "Admin",
                    IsActive = true,
                    IsLocked = false,
                    Roler = "User",
                },
               new UserLogin
                {
                    Id = 2,
                    UserName = "Test2",
                    Password = "Admin1",
                    IsActive = true,
                    IsLocked = true,
                    Roler = "User",
                },
                 new UserLogin
                {
                    Id = 3,
                    UserName = "Admin",
                    Password = "Admin",
                    IsActive = true,
                    IsLocked = false,
                    Roler = "User",
                },

            };
            SaveUsers(userLogin);
            return userLogin;
        }

        public UserLogin? FindIsActive(bool isActive)
        {
            throw new NotImplementedException();
        }

        public UserLogin? TimeTolock(bool IsLocked)
        {
            throw new NotImplementedException();
        }
    }
}
