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
        Task<IEnumerable<UserInternal>> GetAll();
        Task<UserInternal> Get(int id);
        Task<UserInternal> Create(UserInternal u);
        Task<UserInternal> Patch(int id, UserInternal u);
        Task Delete(int id);
    }

    public class UsersRepository : IUsersRepository
    {

        private readonly RestAPIContext _restApiContext;

        public UsersRepository(RestAPIContext restApiContext) 
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<UserInternal>> GetAll()
        {
            return await _restApiContext.users.ToListAsync();
        }

     
        public async Task<UserInternal> Get(int id)
        {
            return await _restApiContext.users.FirstOrDefaultAsync(o => o.id == id);

        }
        public async Task<UserInternal> Create(UserInternal u)
        {
            _restApiContext.users.Add(u);
            await _restApiContext.SaveChangesAsync();

            return u;

        }

        public async Task<UserInternal> Patch(int id, UserInternal u)
        {
            var userToUpdate = _restApiContext.users.FirstOrDefault(o => o.id == id);
            
            if(userToUpdate != default(UserInternal))
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

            if(user != default(UserInternal))
            {
                _restApiContext.Remove(user);
                await _restApiContext.SaveChangesAsync();
            }

            return;
            
        }
    }
}
