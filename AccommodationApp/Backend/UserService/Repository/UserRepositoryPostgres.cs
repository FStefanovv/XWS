﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.DTO;
using Users.Model;

namespace Users.Repository
{
    public class UserRepositoryPostgres : IUserRepository
    {
        private readonly PostgresDbContext _context;
        private PasswordHasher<User> _hasher;


        public UserRepositoryPostgres(PostgresDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<User>();

        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool CheckIfEMailInUse(string email)
        {
            return _context.Users.Where(user => user.Email == email).FirstOrDefault() != null;
        }

        public bool CheckIfUsernameInUse(string username)
        {
            return _context.Users.Where(user => user.Username == username).FirstOrDefault() != null;

        }

        public User GetUserWithCredentials(LoginCredentialsDTO credentials)
        {
            foreach (User user in _context.Users.ToList())
            {
                if (user.Username == credentials.Username && _hasher.VerifyHashedPassword(user, user.Password, credentials.Password) != 0)
                    return user;
            }
            return null;
        }
    }
}