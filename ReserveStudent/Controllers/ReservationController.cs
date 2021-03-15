 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveStudent.Models.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveStudent.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace ReserveStudent.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _repos;
        private readonly IReservationTypeRepository _ReservationTyperepo;
        private UserManager<IdentityUser> _userManger;

        public ReservationController(IReservationRepository repos , IReservationTypeRepository ReservationTyperepo ,UserManager<IdentityUser> userManager )
        {
            _repos = repos;
            _ReservationTyperepo = ReservationTyperepo;
            _userManger = userManager;
        }

    // GET: ReservationController
    public ActionResult Index()
        {
            var reservations = _repos.GetAll();
            return View(reservations);
           
        }

        // GET: ReservationController/Details/5
        public ActionResult Review(int id)
        {
            var reservation = _repos.GetById(id);
            var model = new Reservation
            {
                Id = reservation.Id,
                RequestingStudent = reservation.RequestingStudent,
                RequestingStudentId = reservation.RequestingStudentId,
                ReservationType = reservation.ReservationType,
                Date = reservation.Date,
                ReservationTypeId = reservation.ReservationTypeId,
                Status = reservation.Status
            };
            return View(model);
        }

        public ActionResult ApprouveRequest(int id)
        {
            try
            {
                var user = _userManger.GetUserAsync(User).Result;
                var reservation = _repos.GetById(id);
                reservation.Status = true;
                reservation.RequestingStudent.Count += 1;
                _repos.Update(reservation);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }

        public ActionResult RejectRequest(int id)
        {
            try
            {
                var user = _userManger.GetUserAsync(User).Result;
                var reservation = _repos.GetById(id);
                reservation.Status = false;
                _repos.Update(reservation);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }


        // GET: ReservationController/Create
        public ActionResult Create()
        {
            var reservationTypes = _ReservationTyperepo.GetAll();
             var resrvTypesItems = reservationTypes.Select(x => new SelectListItem
            {
                Text = x.NameType,
                Value = x.Id.ToString()
            }).ToList();
            var model = new CreateReservationViewModel
            {
               ReservationTypes = resrvTypesItems
            };
            return View(model);


           
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReservationViewModel model)
        {
            try
            {
                var Date = Convert.ToDateTime(model.Date);

                var reservationTypes = _ReservationTyperepo.GetAll().ToList();
                var reservationTypesItems = reservationTypes.Select(x => new SelectListItem
                {
                    Text = x.NameType,
                    Value = x.Id.ToString()
                }).ToList();
                model.ReservationTypes = reservationTypesItems;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var student = _userManger.GetUserAsync(User).Result;


                var reservation = new Reservation
                {
                    RequestingStudentId = student.Id,
                    Date = Date,
                    Status = null,
                    ReservationTypeId = model.ReservationTypeId

                };
              
                var isSuccuss = _repos.Create(reservation);
                if (!isSuccuss)
                {
                    ModelState.AddModelError("", "Something went wrong in the submit action");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Delete/5
        public ActionResult Delete(int id)
        {
            var reservation = _repos.GetById(id);
            var isSuccuss = _repos.Delete(reservation);
            if (!isSuccuss)
            {

                return View();
            }
            return RedirectToAction(nameof(StudentReservations));
           
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult StudentReservations()
        {
            var user = _userManger.GetUserAsync(User).Result;

            var Reservations = _repos.GetReservationsByStudent(user.Id);
           
            return View(Reservations);
        }
    }

}
