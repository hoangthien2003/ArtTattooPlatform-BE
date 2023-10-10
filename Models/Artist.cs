namespace back_end.Models
{
    public class Artist
    {
        public string ArtistName { get; set; }
        public bool Gender { get; set; }
        public string NumberPhone { get; set; }
        public string Biography { get; set; }
        public int UserId { get; set; }
        public string Certificate { get; set; }
        public IFormFile AvatarArtist { get; set; }
    }
}
