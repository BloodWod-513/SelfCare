using Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repositories
{
    public class UserRepository
    {
        private SQLiteAsyncConnection _dataBase;

        private async Task Init()
        {
            if (_dataBase != null)
                return;

            _dataBase = await DataBaseConnection.GetConnetion<User>();
        }

        public async Task<User> Get(long id)
        {
            await Init();

            var user = await _dataBase.Table<User>().FirstOrDefaultAsync(s => s.Id == id);

            return user;
        }

        public async Task<User> Get(string email)
        {
            await Init();

            var user = await _dataBase.Table<User>().FirstOrDefaultAsync(s => s.Email == email);

            return user;
        }

        public async Task Add(User model)
        {
            await Init();

            await _dataBase.InsertAsync(model);
        }

        public async Task Update(int id, User model)
        {
            await Init();

            var user = await Get(id);

            user.Email = model.Email;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Patronymic = model.Patronymic;
            user.Weight = model.Weight;
            user.DateOfBirth = model.DateOfBirth;
            user.Sex = model.Sex;

            await _dataBase.UpdateAsync(user);
        }

        public async Task Delete(long id)
        {
            await Init();

            await _dataBase.DeleteAsync<User>(id);
        }

        ~UserRepository()
        {
            _dataBase.CloseAsync();
        }
    }
}
