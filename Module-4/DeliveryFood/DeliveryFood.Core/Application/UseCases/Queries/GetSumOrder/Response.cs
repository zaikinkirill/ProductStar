namespace DeliveryFood.Core.Application.UseCases.Queries.GetSumOrder;

public class Response
{
    public decimal Sum { get; set; }

    public Response(decimal sum)
    {
        Sum = sum;
    }
}