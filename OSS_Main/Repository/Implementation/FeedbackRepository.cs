using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;

namespace OSS_Main.Repository.Implementation
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly Prn222ProjectContext _context;

        public FeedbackRepository(Prn222ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Medias)
                .ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Medias)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
