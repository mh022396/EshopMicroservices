namespace BasketAPI.Basket.GetBasket;

public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new(new("test"));
    }
}
