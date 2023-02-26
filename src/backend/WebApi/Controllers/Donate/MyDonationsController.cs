using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Actions.MyDonations.GetDetails;
using Donate.Actions.MyDonations.GetList;
using Donate.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;

namespace WebApi.Controllers.Donate;

[EnableCors]
[ApiController]
[Authorize]
[Route("my-donations")]
public class MyDonationsController : ControllerBase
{
    private readonly GetListMyDonationAction _getListMyDonationAction;
    private readonly GetMyDonationDetailsAction _getMyDonationDetailsAction;

    public MyDonationsController(GetListMyDonationAction getListMyDonationAction, GetMyDonationDetailsAction getMyDonationDetailsAction)
    {
        _getListMyDonationAction = getListMyDonationAction;
        _getMyDonationDetailsAction = getMyDonationDetailsAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize) => await _getListMyDonationAction.Execute();

    [HttpGet("not-delivered-count")]
    public async Task<ActionResult> GetCountOfNotDelivered()
        => await Task.FromResult(Ok(new { Count = 3 }));

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetails(string id) => await _getMyDonationDetailsAction.Execute(id);

    //Task.FromResult(Ok(new MyDonationDetailsResponse
    //{
    //    Id = "b743E4G",
    //    Name = "DNT000123",
    //    CreationDate = DateTime.Now.AddDays(-5),
    //    ExpireDate = ExpireDateService.GetExpiredDate4Donation(DateTime.Now.AddDays(-5)),
    //    IsDelivered = RandomNumberGenerator.GetInt32(0, 2) != 0,
    //    IsExpired = RandomNumberGenerator.GetInt32(0, 2) != 0,
    //    Warehouse = new MyDonationDetailsWarehouseResponse
    //    {
    //        CountryName = "Poland",
    //        Id = "asj65n87",
    //        Name = "PL001",
    //        GpsLatitude = 52.403324,
    //        GpsLongitude = 16.917781,
    //        City = "Kwidzyn",
    //        Street = "Jaworowa 3/5",
    //        PostalCode = "82-500"
    //    },
    //    Products = new List<MyDonationDetailsProductResponse>
    //    {
    //         new MyDonationDetailsProductResponse
    //         {
    //              Name = "Rice",
    //              Quantity = 1,
    //              Unit = "kg",
    //         },
    //         new MyDonationDetailsProductResponse
    //         {
    //              Name = "Milk",
    //              Quantity = 15,
    //              Unit = "l",
    //         }
    //    }
    //}));

    [HttpGet("score")]
    public async Task<ActionResult> GetScore()
        => await Task.FromResult(Ok(new { Score = 1734 }));
}
