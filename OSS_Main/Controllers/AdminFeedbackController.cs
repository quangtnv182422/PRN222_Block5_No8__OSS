using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class AdminFeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly int _pageSize = 5;

        public AdminFeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> FeedbackList(string sortBy = "CreatedAt", string sortOrder = "desc", int page = 1)
        {
            // Lấy danh sách feedback đã phân trang
            var feedbacks = await _feedbackService.GetFeedbackListAsync(sortBy, sortOrder, page, _pageSize);

            // Tính tổng số feedback để xác định tổng số trang
            var allFeedbacks = await _feedbackService.GetFeedbackListAsync(null, null, 1, int.MaxValue);
            var totalFeedbacks = allFeedbacks.Count();
            var totalPages = (int)Math.Ceiling(totalFeedbacks / (double)_pageSize);

            // Truyền các tham số vào View
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;

            // Danh sách trạng thái
            ViewBag.FeedbackStatuses = new List<SelectListItem>
        {
            new SelectListItem { Value = "Enable", Text = "Enable" },
            new SelectListItem { Value = "Approved", Text = "Approved" }
        };

            return View(feedbacks);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return Json(new { success = false, message = "Feedback not found" });
            }
            return Json(new { success = true, feedback });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeFeedbackStatus(int feedbackId, string status)
        {
            var result = await _feedbackService.ChangeFeedbackStatusAsync(feedbackId, status);
            return Json(new { success = result, message = result ? "Status updated successfully" : "Invalid status or feedback not found" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var result = await _feedbackService.DeleteFeedbackAsync(id);
            return Json(new { success = result, message = result ? "Feedback deleted successfully" : "Feedback not found" });
        }
       
    }
}
