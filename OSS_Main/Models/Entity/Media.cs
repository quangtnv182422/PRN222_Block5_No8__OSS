using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Models.Entity
{
    public enum MediaType : byte
    {
        Image = 1,
        Video = 2
    }

    public class Media
    {
        public int MediaId { get; set; }

        public int FeedbackId { get; set; } // <-- thay vì ProductId
        public Feedback Feedback { get; set; } // <-- navigation property

        [Required, MaxLength(500)]
        public string Url { get; set; }

        [MaxLength(200)]
        public string PublicId { get; set; }

        public MediaType MediaType { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
