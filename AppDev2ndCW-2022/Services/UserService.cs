using AppDev2ndCW_2022.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppDev2ndCW_2022.Services
{
    public class UserService
    {
        private readonly DataBaseContext _context;
        public UserService(DataBaseContext context)
        {
            _context = context;
        }

        internal User GetUserById(int id)
        {
            var appUser = _context.User.Find(id);
            return appUser;
        }

        internal bool TryValidateUser(string username, string contact, out List<Claim> claims)
        {
            claims = new List<Claim>();
            var appUser = _context.User
                .Where(a => a.UserName == username)
                .Where(a => a.UserType == contact).FirstOrDefault();
            if (appUser is null)
            {
                return false;
            }
            else
            {
                claims.Add(new Claim("id", appUser.UserNumber.ToString()));
                claims.Add(new Claim("name", appUser.name));
                claims.Add(new Claim("username", username));
                claims.Add(new Claim("contacts", appUser.contacts));
                claims.Add(new Claim("usertype", appUser.UserType));
                claims.Add(new Claim("password", appUser.UserPassword));
            }
            return true;
        }
    }
}