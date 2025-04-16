using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface IReceiveInforService
    {
        Task AddReceiveInformationAsync(ReceiverInformation infor);
        Task<ReceiverInformation> GetDefaultReceiverInfoAsync(string customerId);
        Task<ReceiverInformation> GetDefaultReceiverInfoByIdAsync(int receiverId);
    }
}
