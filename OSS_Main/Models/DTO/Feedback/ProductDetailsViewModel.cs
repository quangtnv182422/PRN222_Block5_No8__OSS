using OSS_Main.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Models.DTO.Feedback
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<FeedbackViewModel> Reviews { get; set; }
        public CreateFeedbackInputModel NewReview { get; set; }
    }
    public class FeedbackViewModel
    {
        public string CustomerName { get; set; }
        public string AvatarUrl { get; set; }
        public string Content { get; set; }
        public int RatedStar { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateFeedbackInputModel
    {
        public int ProductId { get; set; }
        [Required, StringLength(500)]
        public string FeedbackContent { get; set; }
        [Range(1, 5)]
        public int RatedStar { get; set; }
    }
}
