﻿namespace Application.API.User.Model.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public UserDto(Guid id, AddUserDto user)
        {
            Id = id;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
        }
    }
}
