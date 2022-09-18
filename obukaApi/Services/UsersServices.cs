using DTO.In;
using FluentValidation;
using Models;
using Repositories;
using Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsersServices
    {
        private readonly UsersRepositories _usersRepositories;
        private readonly CreateUserValidator _createUserValidator;
        private readonly DeleteUserValidator _deleteUserValidator;

        public UsersServices(UsersRepositories usersRepositories, CreateUserValidator validationRules, DeleteUserValidator deleteUserValidator)
        {
            _usersRepositories = usersRepositories;
            _createUserValidator = validationRules;
            _deleteUserValidator = deleteUserValidator;
        }

        public IEnumerable<User> GetAll() 
        {
            var users = _usersRepositories.GetAllUsers();
            return users;
        }

        public void CreateUserService(UserDto dto) 
        {
            _createUserValidator.ValidateAndThrow(dto);
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            _usersRepositories.CreateUser(user);
        }

        public void DeleteUserService(int id)
        {
            DeleteDto dto = new DeleteDto();
            dto.Id = id;
            _deleteUserValidator.ValidateAndThrow(dto);
            _usersRepositories.DeleteUser(id);
        }
    }
    public class DeleteDto 
    {
        public int Id { get; set; }
    }
}
