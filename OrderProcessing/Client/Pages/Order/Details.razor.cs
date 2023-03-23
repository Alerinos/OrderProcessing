using Microsoft.AspNetCore.Components;
using OrderProcessing.Shared.DTO;
using System.Net.Http.Json;

namespace OrderProcessing.Client.Pages.Order;

[Route("/order/{id:guid}")]
public partial class Details : ComponentBase
{
    [Inject]
    HttpClient Http { get; set; } = null!;

    [Parameter]
    public Guid Id { get; set; }

    OrderProcessing.Shared.DTO.Order? order { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        order = await Http.GetFromJsonAsync<OrderProcessing.Shared.DTO.Order>($"order/{Id}");
    }
}