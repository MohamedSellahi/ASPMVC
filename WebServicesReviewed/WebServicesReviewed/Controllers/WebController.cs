using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServicesReviewed.Models;

namespace WebServicesReviewed.Controllers {
   public class WebController : ApiController {
      ReservationRepository repo = ReservationRepository.Current;

      public IEnumerable<Reservation> GetAllReservations() {
         return repo.GetAll();
      }

      public Reservation GetReservation(int id) {
         return repo.Get(id);
      }


      // PostReservation
      [HttpPost]
      public Reservation CreateReservation(Reservation item) {
         return repo.Add(item);
      }

      //PutReservation
      [HttpPut]
      public bool PutReservation(Reservation item) {
         return repo.Update(item);
      }

      [HttpDelete]
      public void DeleteReservation(int id) {
         repo.Remove(id);
      }
   }
}
