

using back_end.Entities;

namespace back_end.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int? MemberId { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? StudioId { get; set; }
        public virtual TblMember? Member { get; set; }

        public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    }

}
    