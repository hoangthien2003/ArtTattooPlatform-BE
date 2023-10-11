

using back_end.Entities;

namespace back_end.Models
{
    public class Booking
    {
        public int? BookingID { get; set; }

        public int MemberID { get; set; }

        public int StudioID { get; set; }

        public int ServiceID { get; set; }

        public DateTime BookingDate { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Total { get; set; }
    }

}
    