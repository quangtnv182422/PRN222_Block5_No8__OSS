using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
    public class ReceiveInforService : IReceiveInforService
    {
        private readonly IReceiveInforRepo _inforRepo;

        public ReceiveInforService(IReceiveInforRepo inforRepo)
        {
            _inforRepo = inforRepo;
        }

        public async Task AddReceiveInformationAsync(ReceiverInformation infor)
        {
            await _inforRepo.AddReceiveInformationAsync(infor);
        }

        public async Task<ReceiverInformation> GetDefaultReceiverInfoAsync(string customerId)
        {
            return await _inforRepo.GetDefaultReceiverInfoAsync(customerId);
        }

        public async Task<ReceiverInformation> GetDefaultReceiverInfoByIdAsync(int receiverId)
        {
            return await _inforRepo.GetDefaultReceiverInfoByIdAsync(receiverId);
        }
    }
}
