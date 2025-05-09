using OSS_Main.Models.DTO.Feedback;
using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetFeedbackListAsync(string sortBy, string sortOrder, int page, int pageSize);
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<bool> ChangeFeedbackStatusAsync(int feedbackId, string newStatus);
        Task<bool> DeleteFeedbackAsync(int id);
        Task<bool> UpdateFeedbackAsync(Feedback feedback);
    }
}
