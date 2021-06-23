using Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repositories
{
    public class DoctorRepository
    {
        private SQLiteAsyncConnection _dataBase;

        private async Task Init()
        {
            if (_dataBase != null)
                return;

            _dataBase = await DataBaseConnection.GetConnetion<Doctor>();
        }

        public async Task<Doctor> Get(long id)
        {
            await Init();

            var doctor = await _dataBase.Table<Doctor>().FirstOrDefaultAsync(d => d.Id == id);

            return doctor;
        }

        public async Task Add(Doctor model)
        {
            await Init();

            await _dataBase.InsertAsync(model);
        }

        public async Task Update(int id, Doctor model)
        {
            await Init();

            var doctor = await Get(id);

            doctor.Name = model.Name;
            doctor.Specialization = model.Specialization;
            doctor.Prices = model.Prices;
            doctor.Geolocation = model.Geolocation;

            await _dataBase.UpdateAsync(doctor);
        }

        public async Task Delete(long id)
        {
            await Init();

            await _dataBase.DeleteAsync<Doctor>(id);
        }

        ~DoctorRepository()
        {
            _dataBase.CloseAsync();
        }
    }
}
