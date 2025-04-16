using OSS_Main.Models.Entity;

namespace OSS_Main.Repository.Interface
{
    public interface IReceiveInforRepo
    {
        Task AddReceiveInformationAsync(ReceiverInformation infor);
        Task<ReceiverInformation> GetDefaultReceiverInfoAsync(string customerId);
        Task<ReceiverInformation> GetDefaultReceiverInfoByIdAsync(int receiverId);
    }
}
