namespace OrderProcessing.Shared.DTO;

/// <summary>
/// Order details database model
/// </summary>
public class OrderLine
{
    /// <summary>
    /// Product name
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public double Price { get; set; }
}
