namespace OrderProcessing.Server.Models;

/// <summary>
/// Order details database model
/// </summary>
public class OrderLine
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Order id
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public double Price { get; set; }
}