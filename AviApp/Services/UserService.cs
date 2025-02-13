using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Results;
using AviApp.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

    public class UserService(AvipAppDbContext context) : IUserService
    {

        public async Task<Result<List<User>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await context.Users.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            return users;
        }

        public async Task<Result<User>> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
            {
                return Error.NotFound("User not found");
            }

            return user;
        }
        
        public async Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email, cancellationToken);

                if (existingUser != null)
                {
                
                    return Error.BadRequest("Email already exists.");
                }
                
                context.Users.Add(user);
                await context.SaveChangesAsync(cancellationToken); 
        
                return user;
            }
            catch 
            {
                
                return Error.BadRequest("Couldn't create user. ");
            }
        }

        public async Task<Result<User>> UpdateUserAsync(User updatedUser, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(new object[] { updatedUser.Id }, cancellationToken);

            if (user == null)
            {
                return Error.NotFound("User not found");
            }

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;

            try
            {
                await context.SaveChangesAsync(cancellationToken);
                return user;
            }
            catch
            {
                return Error.BadRequest("Couldn't update user");
            }

        }
        
        public async Task<Result<Deleted>> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(new object[] { id }, cancellationToken);

            if (user == null)
            {
                return Error.NotFound("Customer Not Found");
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            return new Deleted();

        }
        public async Task<bool> UserExistsAsync(int userId, CancellationToken cancellationToken)
        {
            return await context.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        }
    }

