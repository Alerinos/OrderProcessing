using System.ComponentModel.DataAnnotations;

namespace OrderProcessing.Shared.DTO;

public class Order
{
    /// <summary>
    /// Unique id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the customer, in this case it is his name
    /// </summary>
    [Display(Name = "Client name")]
    public string ClientName { get; set; } = string.Empty;

    /// <summary>
    /// Order amount
    /// </summary>
    [Display(Name = "Price")]
    public double Price => Lines?.Sum(x => x.Price) ?? 0;

    /// <summary>
    /// Order lines count
    /// </summary>
    [Display(Name = "Count lines")]
    public int Count => Lines?.Count ?? 0;

    /// <summary>
    /// Date of order creation
    /// </summary>
    public DateTime Created { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Additional information
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Order lines, order details
    /// </summary>
    public IList<OrderLine> Lines { get; set; } = null!;

    /// <summary>
    /// Status varable
    /// </summary>
    public class StatusType
    {
        public static string New => "New";
        public static string Confirm => "Confirm";
        public static string Deliver => "Deliver";
        public static string Cancel => "Cancel";
    }
}
