

using back_end.Entities;

namespace back_end.Models
{
    public class Booking
    {

        public int StudioID { get; set; }

        public int ServiceID { get; set; }

        public string BookingDate { get; set; }
        public string FullName {  get; set; }
        public string PhoneNumber { get; set; }
        public decimal Total { get; set; }
        public int Quantity {  get; set; }
    }

}
    