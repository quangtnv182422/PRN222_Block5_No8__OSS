namespace OSS_Main.Models.DTO.PayOS
{
    public record Response(
 int error,
 String message,
 object? data
);
}
