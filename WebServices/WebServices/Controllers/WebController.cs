using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Http;
using WebServices.Models;


namespace WebServices.Controllers {
   public class WebController : ApiController {
      private ReservationRepository repo = ReservationRepository.Curent;

      public IEnumerable<Reservation> getAllResevations() {
         return repo.GetAll();
      }

      public Reservation getReservation(int id) {
         return repo.Get(id);
      }

      public Reservation postReservation(Reservation item) {
         return repo.Add(item);
      }

      public bool putReservation(Reservation item) {
         return repo.Update(item);
      }

      public void deleteReservation(int id) {
         repo.Remove(id);
      }
      
   }
}