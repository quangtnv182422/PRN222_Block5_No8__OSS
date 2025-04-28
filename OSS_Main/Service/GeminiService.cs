using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Interface;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using static OSS_Main.Models.DTO.GHN.GHNOrderDetails;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly UserManager<AspNetUser> _userManager;
    public GeminiService(HttpClient httpClient,
                         IConfiguration configuration,
                         IUserService userService,
                         IOrderService orderService,
                         UserManager<AspNetUser> userManager)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _userService = userService;
        _orderService = orderService;
        _userManager = userManager;
    }

    public async Task<string> AskGeminiAsync(string userPrompt, HttpContext httpContext)
    {
        var apiKey = _configuration["Gemini:ApiKey"];
        var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        // Thêm dữ liệu nội bộ của bạn tại đây
        var internalContext = @"
                    Thông tin hệ thống:
                Website bán hoa quả tên là Fruitable.


                Các loại sản phẩm hiện có:
                1. *Cherry (Loại Cherry)*:
                   - *Cherry Jumbo New Zealand*: 
                     - Mô tả: Cherry Jumbo New Zealand, đặc biệt là chủng Dunstan Hills, được mệnh danh là 'Kim Cương Đỏ' trong các loại trái cây và là loại cherry ngon nhất thế giới. Với chất lượng vượt trội, cherry Dunstan Hills không chỉ nổi bật về hương vị mà còn là sản phẩm độc quyền của Klever Fruit tại thị trường Việt Nam. Cherry New Zealand được chăm sóc tỉ mỉ từ khâu thu hoạch cho đến đóng gói, chỉ 48h sau khi thu hoạch, trái Cherry New Zealand tươi ngon đã có mặt tại cửa hàng Klever Fruit.
                     - Các kích cỡ: 
                       - Trung ~ 2kg: Giá gốc 1,998,000 VNĐ, giá khuyến mãi 1,598,000 VNĐ, còn 100 sản phẩm.
                       - Lớn ~ 3kg: Giá gốc 2,997,000 VNĐ, giá khuyến mãi 2,397,000 VNĐ, còn 100 sản phẩm.
                       - Thùng nguyên đai ~ 5kg: Giá gốc 4,995,000 VNĐ, giá khuyến mãi 3,995,000 VNĐ, còn 100 sản phẩm.
                     - Hình ảnh sản phẩm: 
                       - ![Cherry Jumbo New Zealand 1](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-01-kg-900-900_4b1210220ba247d59f0bf217941264ea_master_earzgo.webp)
                       - ![Cherry Jumbo New Zealand 2](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-02-kg-900-900_6a071493f7044927bd44f991dc3f51a4_master_cjusfh.webp)
                       - ![Cherry Jumbo New Zealand 3](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705875/thung-nguyen-dai-cherry-jumbo-new-zealand-5kg_73e386644c144ddc81e4f9ddfe655110_master_kdukkx.webpp)

                   - *Cherry Đỏ New Zealand*: 
                     - Mô tả: Cherry Dunstan Hills New Zealand là dòng cherry cao cấp nổi tiếng toàn cầu, mang đến chất lượng vượt trội và hương vị độc đáo. Sản phẩm được thiết kế dành riêng cho thị trường Việt Nam.
                     - Các kích cỡ:
                       - Mini ~ 0.6kg: Giá gốc 599,000 VNĐ, giá khuyến mãi 479,000 VNĐ, còn 100 sản phẩm.
                       - Nhỏ ~ 1kg: Giá gốc 999,000 VNĐ, giá khuyến mãi 799,000 VNĐ, còn 100 sản phẩm.
                       - Trung ~ 2kg: Giá gốc 1,998,000 VNĐ, giá khuyến mãi 1,598,000 VNĐ, còn 100 sản phẩm.
                       - Lớn ~ 3kg: Giá gốc 2,997,000 VNĐ, giá khuyến mãi 2,397,000 VNĐ, còn 100 sản phẩm.
                     - Hình ảnh sản phẩm:
                       - ![Cherry Đỏ New Zealand](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-01-kg-900-900_4b1210220ba247d59f0bf217941264ea_master_earzgo.webp)

                2. *Táo (Loại Táo)*:
                   - *Táo Haruka Nhật Bản*:
                     - Mô tả: Táo Haruka Nhật Bản được mệnh danh là một trong những giống táo quý hiếm và độc đáo bậc nhất thế giới. Với xuất xứ từ Nhật Bản, nơi nổi tiếng với kỹ thuật canh tác sạch và độc đáo, táo Haruka gây ấn tượng mạnh bởi hương vị ngọt ngào, kích thước lớn và lõi mật ong đặc biệt mà không có bất kỳ một loại táo nào có được.
                     - Các kích cỡ: 
                       - Mini ~ 0.6kg: Giá gốc 599,000 VNĐ, giá khuyến mãi 479,000 VNĐ, còn 101 sản phẩm.
                       - Nhỏ ~ 1kg: Giá gốc 999,000 VNĐ, giá khuyến mãi 799,000 VNĐ, còn 100 sản phẩm.
                     - Hình ảnh sản phẩm:
                       - ![Táo Haruka Nhật Bản](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706175/z5119132584349_16739af07b5afae7cf60744beb76581d_24a5df035d1040a2aa81223bda9e39ef_master_jk4qbw.webp)

                3. *Dâu Tây (Loại Dâu Tây)*:
                   - *Dâu Tây Hàn Quốc VIP*:
                     - Mô tả: Dâu Tây Hàn Quốc VIP độc quyền tại Klever Fruit, mang vẻ đẹp tự nhiên và đạt đỉnh cao về hương vị và giá trị dinh dưỡng. Đây là dòng sản phẩm cao cấp, đại diện cho triết lý ""Trái cây chín đúng thời điểm.""
                     - Các kích cỡ: 
                       - Hộp 420 gram: Giá gốc 250,000 VNĐ, giá khuyến mãi 200,000 VNĐ, còn 150 sản phẩm.
                     - Hình ảnh sản phẩm:
                       - ![Dâu Tây Hàn Quốc VIP](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706303/dau-tay-vip-han-quoc-hop-840-gram_-_copy_b1507ea5d25641479606abc5e6486144_master_mknbre.webp)

                4. *Nho (Loại Nho)*:
                   - *Nho Muscat Beauty Chile*:
                     - Mô tả: Nhập khẩu trực tiếp từ Chile, giống nho không hạt cao cấp, đáp ứng tiêu chuẩn xuất khẩu sang các thị trường khó tính như châu Âu và Mỹ.
                     - Các kích cỡ:
                       - Trung ~ 1kg: Giá gốc 350,000 VNĐ, giá khuyến mãi 280,000 VNĐ, còn 120 sản phẩm.
                     - Hình ảnh sản phẩm:
                       - ![Nho Muscat Beauty Chile](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706416/z5334222072666_147590186bb7fb9f7f2cc7c1cae36e82_4266a208323249279642839cb0522d3d_master_bnmjfr.webp)

                5. *Dưa (Loại Dưa)*:
                   - *Dưa Giống Nhật Musk Melon*:
                     - Mô tả: Dưa quý hiếm được trồng tại Đà Lạt, với độ ngọt hoàn hảo và hình dáng kiêu sa. Sản phẩm nổi bật với sự đồng đều và chất lượng vượt trội.
                     - Các kích cỡ: 
                       - Quả ~ 1.5kg: Giá gốc 499,000 VNĐ, giá khuyến mãi 399,000 VNĐ, còn 50 sản phẩm.
                     - Hình ảnh sản phẩm:
                       - ![Dưa Giống Nhật Musk Melon](https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706668/1_d0b6f93573804d418064c55936640cae_master_udm2dk.webp)

                Trạng thái đơn hàng:
                Đang xử lý
                Đang giao
                Đã giao
                Đã hủy


                Các đường link đến các trang khác nhau:
                 - Đây là trang dẫn đến sản phẩm chung (https://localhost:7012/Products)
                 - Đây là trang dẫn đến trang chủ (https://localhost:7012)
                 - Đây là trang dẫn đến trang đăng nhập (https://localhost:7012/Identity/Account/Login)
                 - Đây là trang dẫn đến trang đăng ký (https://localhost:7012/Identity/Account/Register?returnUrl=%2F)
                 - Đây là trang dẫn đến trang quên mật khẩu (https://localhost:7012/Identity/Account/ForgotPassword)


                Thông tin bổ sung:
                - *Tích hợp GHN*: Hỗ trợ tra cứu trạng thái đơn hàng theo mã vận đơn.
                Người dùng có thể đặt hàng, hủy đơn, trả hàng.
                
                Có 3 phương thức thanh toán là Thanh toán bằng mã QR, Thanh toán bằng vnPay, và COD

                Dữ liệu này sẽ hỗ trợ các câu hỏi của người dùng về sản phẩm, tình trạng đơn hàng,
                và thông tin chi tiết về các mặt hàng bán trên website của bạn và ngoài các dữ liệu này có thể trả lời dùng dữ liệu ngoài để trả lời hoặc dùng các kiến thức chung trên mạng để đáp ứng các loại câu hỏi.
                                        "
        ;

        var currentUserId = await _userService.GetUserIdAsync(httpContext);
        var currentUser = await _userService.GetCurrentUserAsync(currentUserId);

        var internalIdentity = "";

        if (currentUser != null)
        {
            var roles = await _userManager.GetRolesAsync(currentUser);
            //promt data dành cho Customers

            if (roles.Contains("Customer"))
            {
                internalIdentity = $@"
                    Đây là
                    Thông tin hệ thống sau khi đã đăng nhập người dùng với vai trò là Khách hàng (Customer):
                Website bán hoa quả tên là Fruitable.

                 - Đây là thông tin người dùng đang đăng nhập:
                    - Tên người dùng: {currentUser.UserName}
                    - Email: {currentUser.Email}
                    - Phone: {currentUser.PhoneNumber}
                

                 - Đây là trang dẫn đến sản phẩm theo dõi sản phẩm [https://localhost:7012/Tracking]
            
                                ";


                var listOrderStatus = await _orderService.GetAllOrderStatusAsync();
                if (listOrderStatus != null)
                {
                    internalIdentity += $@"
                                        Đây là danh sách các trạng thái của order tương ứng với id, name để đánh dấu trạng thái của order đó, display dùng để hiển thị cho user:
                                        ";
                    foreach (var status in listOrderStatus)
                    {
                        internalIdentity += $@"
                                      -  Order Id : {status.OrderStatusId}
                                      -  Order Name: {status.OrderStatusName}
                                      -  Order Display: {status.OrderDisplay}
                                         
                                        ";
                    }
                }


                var orderTracking = await _orderService.GetAllOrderByUserReceiverAsync(currentUserId);
                if (orderTracking != null)
                {
                    foreach (var item in orderTracking)
                    {
                        internalIdentity += $@"
                                        Đây là các order của người dùng:
                                      -  Order Id : {item.OrderId}
                                      -  Order Code bên Giao Hàng Nhanh (GHN) : {item.OrderCode_GHN}
                                      -  Order được tạo lúc : {item.OrderAt}
                                      -  Order được thanh toán bằng phương thức : {item.PaymentMethod}
                                      -  Order có tổng tiền là : {item.TotalCost}
                                      -  Order đang có trạng thái là : {item.OrderStatus.OrderDisplay}
                                      -  Order được giao tại địa chỉ : {item.Receiver.Address}, {item.Receiver.WardName_GHN}, {item.Receiver.DistrictName_GHN}, {item.Receiver.ProvinceName_GHN}
                                         
                                        ";
                    }
                }

                internalIdentity += " Dữ liệu này sẽ hỗ trợ các câu hỏi của người dùng về sản phẩm, tình trạng đơn hàng,\r\n      " +
                    "          và thông tin chi tiết về các mặt hàng bán trên website của bạn và ngoài các dữ liệu này có thể trả lời dùng dữ liệu ngoài để trả lời.";
            }

            //promt data dành cho Admin and Sales
            if (roles.Contains("Admin"))
            {
                internalIdentity = $@"
                    Đây là
                    Thông tin hệ thống sau khi đã đăng nhập người dùng với vai trò là Admin :
                Website bán hoa quả tên là Fruitable.

                 - Đây là thông tin người dùng đang đăng nhập:
                    - Tên người dùng: {currentUser.UserName}
                    - Email: {currentUser.Email}
                    - Phone: {currentUser.PhoneNumber}
            
                                ";


                var listOrderStatus = await _orderService.GetAllOrderStatusAsync();
                if (listOrderStatus != null)
                {
                    internalIdentity += $@"
                                        Đây là danh sách các trạng thái của order tương ứng với id, name để đánh dấu trạng thái của order đó, display dùng để hiển thị cho user:
                                        ";
                    foreach (var status in listOrderStatus)
                    {
                        internalIdentity += $@"
                                      -  Order Id : {status.OrderStatusId}
                                      -  Order Name: {status.OrderStatusName}
                                      -  Order Display: {status.OrderDisplay}
                                         
                                        ";
                    }
                }


                var orderTracking = await _orderService.GetAllOrderAsync();
                if (orderTracking != null)
                {
                    foreach (var item in orderTracking)
                    {
                        internalIdentity += $@"
                                        Đây là các order của người dùng:
                                      -  Order Id : {item.OrderId}
                                      -  Order Code bên Giao Hàng Nhanh (GHN) : {item.OrderCode_GHN}
                                      -  Order được tạo lúc : {item.OrderAt}
                                      -  Order được thanh toán bằng phương thức : {item.PaymentMethod}
                                      -  Order có tổng tiền là : {item.TotalCost}
                                      -  Order đang có trạng thái là : {item.OrderStatus.OrderDisplay}
                                      -  Order được giao tại địa chỉ : {item.Receiver.Address}, {item.Receiver.WardName_GHN}, {item.Receiver.DistrictName_GHN}, {item.Receiver.ProvinceName_GHN}
                                         
                                        ";
                    }
                }


                var userList = await _userService.GetAllUsersAsync();
                if (userList != null)
                {
                    internalIdentity = $@"Tổng tất cả {userList.Count()} tài khoản trong hệ thống";
                    foreach (var item in userList)
                    {
                        internalIdentity += $@"
                                        Đây là các thông tin cá nhân của người dùng:

                                      -  User Name  : {item.UserName}
                                      -  Email : {item.Email}
                                      - Email Confirmed : {item.EmailConfirmed} nếu là 1 là đã Confirmed rồi, nếu là 2 thì là Chưa Confirmed
                                     
                                         
                                        ";
                    }
                }
                internalIdentity += " Dữ liệu này sẽ hỗ trợ các câu hỏi của người dùng về sản phẩm, tình trạng đơn hàng,\r\n      " +
                    "          và thông tin chi tiết về các mặt hàng bán trên website của bạn và ngoài các dữ liệu này có thể trả lời dùng dữ liệu ngoài để trả lời.";
            }


        }




        var finalPrompt = internalIdentity + internalContext + "\n\nCâu hỏi người dùng:\n" + userPrompt;

        var requestData = new
        {
            contents = new[] {
            new {
                parts = new[] {
                    new { text = finalPrompt }
                }
            }
        }
        };

        var response = await _httpClient.PostAsJsonAsync(requestUrl, requestData);
        var result = await response.Content.ReadFromJsonAsync<JsonElement>();

        return result.GetProperty("candidates")[0]
                     .GetProperty("content")
                     .GetProperty("parts")[0]
                     .GetProperty("text")
                     .GetString();
    }




}