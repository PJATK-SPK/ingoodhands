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

    [HttpGet("score")]
    public async Task<ActionResult> GetScore()
        => await Task.FromResult(Ok(new { Score = 1734 }));
}
