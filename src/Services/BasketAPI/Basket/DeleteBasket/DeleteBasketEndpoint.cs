
using BasketAPI.Basket.StoreBasket;

namespace BasketAPI.Basket.DeleteBasket;

//public record DeleteBasketRequest();
public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("DeleteProduct")
        .WithDescription("DeleteProduct");
    }
}
