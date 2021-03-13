using Microsoft.EntityFrameworkCore;
using ReserveStudent.Data;
using ReserveStudent.Models.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveStudent.Models.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _db;
        public ReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Reservation entity)
        {
            var reservation = _db.Reservations.Add(entity);
            return Save();
        }

        public bool Delete(Reservation entity)
        {
            var reservation = _db.Reservations.Remove(entity);
            return Save();
        }

        public List<Reservation> GetAll()
        {
            var reservation = _db.Reservations
                .Include(x => x.ReservationType)
                .Include(x => x.RequestingStudent)
                .ToList();
            return reservation;
        }

        public Reservation GetById(int id)
        {
            var reservation = _db.Reservations
                .Include(x => x.RequestingStudent)
                .Include(x => x.ReservationType)
                .FirstOrDefault(x=>x.Id==id);
            return reservation;
        }

        public bool IsExist(int id)
        {
            var exists = _db.Reservations.Any(x => x.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Reservation entity)
        {
            var reservation = _db.Reservations.Update(entity);
            return Save();
        }
    }
}
