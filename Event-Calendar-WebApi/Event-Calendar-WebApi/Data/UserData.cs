﻿using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Models;

namespace Event_Calendar_WebApi.Data
{
    public class UserData : IUserData
    {
        private readonly DataContext _context;

        public UserData(DataContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
        }

        public User GetUser(int userId)
        {
            return _context.Users.Find(userId);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public List<User> GetUsersByFilter(string filter)
        {
            return _context.Users.Where(p=> p.FirstName.Contains(filter) || p.LastName.Contains(filter)).ToList();
        }

        public User UpdateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
