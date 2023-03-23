using Microsoft.EntityFrameworkCore;
using OrderProcessing.Shared;
using static OrderProcessing.Server.Models.Order;

namespace OrderProcessing.Server.Modules;

public class Order
{
    readonly Context _context;

    public Order(Context context)
        => _context = context;

    IQueryable<Models.Order> Get()
        => _context
        .Orders
        .Include(x => x.Lines);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<Shared.DTO.Order>> GetAllAsync()
        => await Get()
            .Select(x => DTOOrder(x))
            .ToListAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="line"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    public async Task<Guid> CreateAsync(string client, List<Shared.DTO.OrderLine> line, string? note = null)
    {
        var model = new Models.Order
        {
            ClientName = client,
            Note = note,
            Lines = line.Select(x => new Models.OrderLine {
                Product = x.Product,
                Price = x.Price
            }).ToList()
        };

        _context.Orders.Add(model);
        await _context.SaveChangesAsync();  

        return model.Id;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Shared.DTO.Order> GetAsync(Guid id)
        => await Get()
            .Where(x => x.Id == id)
            .Select(x => DTOOrder(x))
            .SingleOrDefaultAsync() ?? new();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<bool> UpdateClientAsync(Guid id, string client)
    {
        var model = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return false;

        model.ClientName = client;

        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    public async Task<bool> UpdateNoteAsync(Guid id, string note)
    {
        var model = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return false;

        model.Note = note;

        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<bool> UpdateStatusAsync(Guid id, string status)
    {
        var model = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);

        if (model is null)
            return false;

        if (Enum.TryParse(status, out StatusType statusType))
        {
            model.Status = statusType;
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await _context.Orders.Include(x =>x.Lines).SingleOrDefaultAsync(x => x.Id == id);

        if(model is null)
            return false;

        _context.Orders.Remove(model);

        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static Shared.DTO.Order DTOOrder(Models.Order model)
        => new() { 
            Id = model.Id,
            ClientName = model.ClientName,
            Created = model.Created,
            Note = model.Note,
            Status = model.Status.ToString(),
            Lines = model.Lines.Select(x => DTOOrderLine(x)).ToList(),
        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public static Shared.DTO.OrderLine DTOOrderLine(Models.OrderLine line)
        => new()
        {
            Product = line.Product,
            Price = line.Price
        };
}