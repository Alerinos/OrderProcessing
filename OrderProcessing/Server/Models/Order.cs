namespace OrderProcessing.Server.Models;

/// <summary>
/// Database model of all orders
/// </summary>
public class Order
{
    /// <summary>
    /// Unique id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the customer, in this case it is his name
    /// </summary>
    public string ClientName { get; set; } = string.Empty;

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
    public StatusType Status { get; set; } = StatusType.New;

    /// <summary>
    /// Order lines, order details
    /// </summary>
    public ICollection<OrderLine> Lines { get; set; } = null!;

    /// <summary>
    /// Order status options
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// The order is new and was placed by the customer.
        /// </summary>
        New,

        /// <summary>
        /// The order has been confirmed by the system
        /// </summary>
        Confirm,

        /// <summary>
        /// The order was delivered
        /// </summary>
        Deliver,

        /// <summary>
        /// The order has been cancelled
        /// </summary>
        Cancel
    }
}