using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafePass.Data
{
    public class UserService
    {
        private readonly SafePassContext _context;

        public UserService(SafePassContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of users.</returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users!.ToListAsync();
        }

        /// <summary>
        /// Retrieves a single user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user, or null if not found.</returns>
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users!.FindAsync(id);
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The added user.</returns>
        public async Task<User> AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid(); // Ensure a new ID is generated
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash the password
            await _context.Users!.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="user">The user with updated details.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users!.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            existingUser.UserName = user.UserName;

            // Only hash the password if it's updated
            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password= BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users!.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Validates a user's credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The plain text password.</param>
        /// <returns>The user if validation succeeds, or null if it fails.</returns>
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            try
            {
                Console.WriteLine($"Attempting to validate user: {username}");

                var user = await _context.Users!
                    .FirstOrDefaultAsync(u => u.UserName == username);

                if (user == null)
                {
                    Console.WriteLine($"No user found with username: {username}");
                    return null;
                }

                Console.WriteLine($"User found: {user.UserName}. Verifying password...");

                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    Console.WriteLine($"Password mismatch for user: {username}");
                    return null;
                }

                Console.WriteLine($"User validation succeeded for {username}");
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ValidateUserAsync: {ex}");
                throw; // Let this propagate to see the full error in logs
            }
        }

    }
}
