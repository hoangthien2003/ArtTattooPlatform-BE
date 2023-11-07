namespace back_end.Models
{
    public class BookingDetail
    {
        public int BookingDetailID { get; set; }
        public string? BookingID { get; set; }
        public int? Quantity { get; set; }
        public int? ServiceID { get; set; }
        public int? ArtistID { get; set; }
        public decimal? Price { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Service Service { get; set; }
        public virtual Feedback Artist { get; set; }
    }
}
