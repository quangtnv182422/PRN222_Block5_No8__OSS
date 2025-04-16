using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Models.Entity;

public partial class ReceiverInformation
{
    public int ReceiverId { get; set; }

    [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Họ và tên phải có ít nhất 1 ký tự và không quá 100 ký tự.")]
    [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Họ và tên chỉ được chứa chữ cái (bao gồm cả dấu) và khoảng trắng.")]
    public string? FullName { get; set; }

    [Required]
    [StringLength(13, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 13 digits.")]
    [RegularExpression(@"^\d{10,13}$", ErrorMessage = "Phone number must only contain numbers.")]
    public string? PhoneNumber { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "Email cannot contain spaces.")]
    public string? Email { get; set; }

    public string? Address { get; set; }
    public string? ProvinceId_GHN { get; set; }
    public string? DistrictId_GHN { get; set; }
    public string? WardId_GHN { get; set; } 
    public string? ProvinceName_GHN { get; set; }
    public string? DistrictName_GHN { get; set; }
    public string? WardName_GHN { get; set; }
    public string? CustomerId { get; set; }
    public bool? IsDefault { get; set; }
    public virtual AspNetUser? Customer { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
