using Microsoft.AspNetCore.Mvc;

namespace WalletBalanceService.Features.GetBalance;

public static class GetBalanceEndpoint
{   
    public static void UseGetBalance(this WebApplication app)
    {
        app.MapGet("/balances/{id}", GetBalance);
    }
    
    private static async Task<IResult> GetBalance(string id, IGetBalanceService getBalanceService, CancellationToken cancellationToken)
    {
        var response = await getBalanceService.GetBalance(id, cancellationToken);
        if (response == null) return Results.NotFound();
        
        return Results.Ok(response);
    }
}

