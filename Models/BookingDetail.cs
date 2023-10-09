namespace back_end.Models
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int? BookingId { get; set; }
        public int? Quantity { get; set; }
        public int? ServiceId { get; set; }
        public int? ArtistId { get; set; }
        public decimal? Price { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Service Service { get; set; }
        public virtual FeedbackRequest Artist { get; set; }
    }
}
