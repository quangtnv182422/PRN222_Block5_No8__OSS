using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using System.Net;

namespace OSS_Main.Repository.Implementation
{
    public class ReceiveInforRepo : IReceiveInforRepo
    {
        private readonly Prn222ProjectContext _context;
        public ReceiveInforRepo(Prn222ProjectContext context)
        {
            _context = context;
        }
        public async Task AddReceiveInformationAsync(ReceiverInformation infor)
        {
            _context.ReceiverInformations.Add(infor);
            await _context.SaveChangesAsync();
        }

        public async Task<ReceiverInformation> GetDefaultReceiverInfoAsync(string customerId)
        {
            return await _context.ReceiverInformations
                .Where(r => r.CustomerId == customerId && r.IsDefault == true)
                .FirstOrDefaultAsync();
        }

        public async Task<ReceiverInformation> GetDefaultReceiverInfoByIdAsync(int receiverId)
        {
            return await _context.ReceiverInformations
                .Where(r => r.ReceiverId == receiverId)
                .FirstOrDefaultAsync();
        }
    }
}
