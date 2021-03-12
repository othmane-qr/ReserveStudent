using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveStudent.Models.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveStudent.Models;

namespace ReserveStudent.Controllers
{
    public class ReservationTypeController : Controller
    {
        private readonly IReservationTypeRepository _repo;
        public ReservationTypeController(IReservationTypeRepository repo)
        {
            _repo = repo;
        }

        // GET: ReservationType
        public ActionResult Index()
        {
            var reservationTypes = _repo.GetAll();
            return View(reservationTypes);
        }

        // GET: ReservationType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationType reservation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(reservation);
                }

                var reservationType = new ReservationType
                {
                    Id = reservation.Id,
                    Name = reservation.Name,
                    Number = reservation.Number
                };

                var isSuccess = _repo.Create(reservationType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(reservation);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationType/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.IsExist(id))
            {
                return NotFound();
            }
            var AbsenceType = _repo.GetById(id);
            var model = new ReservationType
            {
                Id = AbsenceType.Id,
                Name = AbsenceType.Name,
                Number = AbsenceType.Number
            };
            return View(model);
        }

        // POST: ReservationType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReservationType model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                var ReservationType = new ReservationType
                {
                    Id = model.Id,
                    Name = model.Name,
                    Number = model.Number
                };
                var isSuccess = _repo.Update(ReservationType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        // GET: ReservationType/Delete/5
        public ActionResult Delete(int id)
        {
            var reservationType = _repo.GetById(id);
            if (reservationType == null)
            {
                return NotFound();
            }
            var IsSuccess = _repo.Delete(reservationType);
            if (!IsSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ReservationType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var reservationType = _repo.GetById(id);
                if (reservationType == null)
                {
                    return NotFound();
                }
                var IsSuccess = _repo.Delete(reservationType);
                if (!IsSuccess)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
