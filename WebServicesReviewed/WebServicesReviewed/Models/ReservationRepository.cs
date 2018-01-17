using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicesReviewed.Models {
   public class ReservationRepository {
      private static ReservationRepository repo = new ReservationRepository();

      public static ReservationRepository Current {
         get {
            return repo;
         }
      }

      private List<Reservation> data = new List<Reservation> {
         new Reservation {
            ReservationId = 1, ClientName = "Adam", Location = "Board Room"},
         new Reservation {
            ReservationId = 2, ClientName = "Jacqui", Location = "Lecture Hall"},
         new Reservation {
            ReservationId = 3, ClientName = "Russell", Location = "Meeting Room 1"},
         };

      public IEnumerable<Reservation> GetAll() {
         return data;
      }

      public Reservation Get(int id) {
         return data.Where(i => i.ReservationId == id).FirstOrDefault();
      }

      public Reservation Add(Reservation item) {
         item.ReservationId = getNextId();
         data.Add(item);
         return item;
      }

      public void Remove(int id) {
         Reservation item = Get(id);
         if (item != null) {
            data.Remove(item);
         }
      }

      public bool Update(Reservation item) {
         Reservation old = Get(item.ReservationId);
         if (old != null) {
            old.Location = item.Location;
            old.ClientName = item.ClientName;
            return true;
         }
         return false;
      }

      private int getNextId() {
         return data.Count + 1;
      }
   }
}