using Microsoft.AspNetCore.Components;
using OrderProcessing.Shared;
using System.Net.Http.Json;

namespace OrderProcessing.Client.Pages.Order;

[Route("/orders")]
public partial class List : ComponentBase
{
    [Inject]
    HttpClient Http { get; set; } = null!;

    private IList<OrderProcessing.Shared.DTO.Order>? orders;
    protected override async Task OnInitializedAsync()
    {
        orders = await Http.GetFromJsonAsync<IList<OrderProcessing.Shared.DTO.Order>>("order");
    }
}
