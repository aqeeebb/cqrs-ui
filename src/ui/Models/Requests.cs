using System;
using System.ComponentModel.DataAnnotations;
public class AesRequest {
    [Required]
    public string Uri { get; set; }

    [Required]
    public string SubscriptionKey  { get; set; }

    [Range(1, 1500, ErrorMessage = "Total number invalid (1-1500).")]
    public int TotalKeys { get; set; } = 50;

    public string KeyId { get; set; }
}
