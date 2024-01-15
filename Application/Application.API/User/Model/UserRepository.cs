﻿using Application.API.User.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.API.User.Model
{
    public class UserRepository : IUserRepository
	{
		private readonly UserContext _context;

		public UserRepository(UserContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
		}

		public async Task<IEnumerable<UserDto>> Get()
		{
			return await _context.User.ToListAsync();
		}

		public async Task<UserDto> Get(Guid id)
		{
			var user = await _context.User.FindAsync(id);
			if(user == null)
			{
				throw new Exception("User not found!");
			}
			else
			{
				return user;
			}
		}

		public async Task<UserDto> Add(AddUserDto user)
		{
			var existingUser = await _context.User.FindAsync(user.Email);
			if (existingUser != null)
			{
				throw new Exception("User with this email already exists!");
			}
			else
			{
				var guid = Guid.NewGuid();
                _context.User.Add(new UserDto(guid, user));
				await _context.SaveChangesAsync();
				var newUser = await _context.User.FindAsync(guid);
				if (newUser == null)
				{
					throw new Exception("A problem occurred when creating new user!");
				}
				else
				{
					return newUser;
				}
			}
		}

		public async Task<UserDto> Update(Guid id, UpdateUserDto user)
		{
			if(_context.User.FindAsync(id).Result == null)
			{
				throw new Exception("User not found!");
			}
			else
			{
				_context.Entry(user).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				var updatedUser = await _context.User.FindAsync(id);
				if (updatedUser == null)
				{
                    throw new Exception("A problem occurred when updating new user!");
                }
				else
				{
                    return updatedUser;
                }
			}
		}

		public async Task Delete(Guid id)
		{
			var user = await _context.User.FindAsync(id);
			if (user == null)
			{
				throw new Exception("User not found!");
			}
			else
			{
				_context.User.Remove(user);
				await _context.SaveChangesAsync();
			}
		}
	}
}