using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Actions.MyDonations.GetDetails;
using Donate.Actions.MyDonations.GetList;
using Donate.Actions.MyDonations.GetNotDeliveredCount;
using Donate.Actions.MyDonations.GetScore;
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
    private readonly GetNotDeliveredCountAction _getNotDeliveredCountAction;
    private readonly GetScoreAction _getScoreAction;

    public MyDonationsController(GetListMyDonationAction getListMyDonationAction, GetMyDonationDetailsAction getMyDonationDetailsAction, GetNotDeliveredCountAction getNotDeliveredCountAction, GetScoreAction getScoreAction)
    {
        _getListMyDonationAction = getListMyDonationAction;
        _getMyDonationDetailsAction = getMyDonationDetailsAction;
        _getNotDeliveredCountAction = getNotDeliveredCountAction;
        _getScoreAction = getScoreAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize) => await _getListMyDonationAction.Execute();

    [HttpGet("not-delivered-count")]
    public async Task<ActionResult> GetCountOfNotDelivered() => await _getNotDeliveredCountAction.Execute();

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetails(string id) => await _getMyDonationDetailsAction.Execute(id);

    [HttpGet("score")]
    public async Task<ActionResult> GetScore() => await _getScoreAction.Execute();
}
