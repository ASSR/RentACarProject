using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryUserDal : IUserDal
    {
        List<User> _Users;

        public InMemoryUserDal()
        {
            _Users = new List<User> {
                new User{UserId=1, FirstName="Aşır",LastName="ÇALIŞIR",Email="asircalisir@gmail.com"}
            };
        }

        public void Add(User User)
        {
            _Users.Add(User);
        }

        public void Delete(User User)
        {
            User UserToDelete = _Users.SingleOrDefault(p => p.UserId == p.UserId);
            _Users.Remove(UserToDelete);
        }

        public List<User> GetAll()
        {
            return _Users;
        }

        public void Update(User User)
        {
            User UserToUpdate = _Users.SingleOrDefault(p => p.UserId == p.UserId);
            UserToUpdate.FirstName = User.FirstName;
            UserToUpdate.LastName = User.LastName;
            UserToUpdate.Email = User.Email;
        }

        public List<User> GetAllByFirstName(string firstName)
        {
            return _Users.Where(p => p.FirstName == firstName).ToList();
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<UserDetailDTO> GetUserDetails()
        {
            throw new NotImplementedException();
        }

        public List<OperationClaim> GetClaims(User user)
        {
            throw new NotImplementedException();
        }
    }
}