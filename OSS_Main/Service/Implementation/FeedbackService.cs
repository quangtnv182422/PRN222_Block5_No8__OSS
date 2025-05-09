using OSS_Main.Models.DTO.Feedback;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackListAsync(string sortBy, string sortOrder, int page, int pageSize)
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbacksAsync();

            // Sắp xếp
            switch (sortBy?.ToLower())
            {
                case "feedbackid":
                    feedbacks = sortOrder == "asc" ? feedbacks.OrderBy(f => f.FeedbackId) : feedbacks.OrderByDescending(f => f.FeedbackId);
                    break;
                case "productid":
                    feedbacks = sortOrder == "asc" ? feedbacks.OrderBy(f => f.ProductId) : feedbacks.OrderByDescending(f => f.ProductId);
                    break;
                case "customerid":
                    feedbacks = sortOrder == "asc" ? feedbacks.OrderBy(f => f.Customer.UserName) : feedbacks.OrderByDescending(f => f.Customer.UserName);
                    break;
                case "status":
                    feedbacks = sortOrder == "asc" ? feedbacks.OrderBy(f => f.Status) : feedbacks.OrderByDescending(f => f.Status);
                    break;
                case "ratedstar":
                    feedbacks = sortOrder == "asc" ? feedbacks.OrderBy(f => f.RatedStar) : feedbacks.OrderByDescending(f => f.RatedStar);
                    break;
                case "createdat":
                    feedbacks = sortOrder == "asc" ? feedbacks.OrderBy(f => f.CreatedAt) : feedbacks.OrderByDescending(f => f.CreatedAt);
                    break;
                default:
                    feedbacks = feedbacks.OrderByDescending(f => f.CreatedAt);
                    break;
            }

            // Phân trang
            var pagedFeedbacks = feedbacks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedFeedbacks;
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _feedbackRepository.GetFeedbackByIdAsync(id);
        }

        public async Task<bool> ChangeFeedbackStatusAsync(int feedbackId, string newStatus)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(feedbackId);
            if (feedback == null) return false;

            if (newStatus != "Unenable" && newStatus != "Enable") return false;

            feedback.Status = newStatus;
            feedback.ModifyAt = DateTime.Now;
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
            return true;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            await _feedbackRepository.DeleteFeedbackAsync(id);
            return true;
        }

        public async Task<bool> UpdateFeedbackAsync(Feedback feedback)
        {
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
            return true;
        }
    }
}
