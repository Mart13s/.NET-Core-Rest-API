using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Create(User u);
        Task<User> Patch(int id, User u);
        Task Delete(int id);
    }

    public class UsersRepository : IUsersRepository
    {

        private readonly RestAPIContext _restApiContext;

        public UsersRepository(RestAPIContext restApiContext) 
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _restApiContext.users.ToListAsync();
        }

     
        public async Task<User> Get(int id)
        {
            return await _restApiContext.users.FirstOrDefaultAsync(o => o.id == id);

        }
        public async Task<User> Create(User u)
        {
            _restApiContext.users.Add(u);
            await _restApiContext.SaveChangesAsync();

            return u;

        }

        public async Task<User> Patch(int id, User u)
        {
            var userToUpdate = _restApiContext.users.FirstOrDefault(o => o.id == id);
            
            if(userToUpdate != default(User))
            {
                userToUpdate = u;
                await _restApiContext.SaveChangesAsync();
                return u;
            }

            else
            {
                return null;
            }
           

        }

        public async Task Delete(int id)
        {
            var user = _restApiContext.users.FirstOrDefault(o => o.id == id);

            if(user != default(User))
            {
                _restApiContext.Remove(user);
                await _restApiContext.SaveChangesAsync();
            }

            return;
            
        }
    }
}
