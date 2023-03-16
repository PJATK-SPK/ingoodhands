using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.OrdersActions.OrdersCancel
{
    public class OrdersCancelService
    {
        private readonly Hashids _hashids;
        private readonly RoleService _roleService;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<OrdersCancelService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public OrdersCancelService(
            Hashids hashids,
            RoleService roleService,
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            ILogger<OrdersCancelService> logger)
        {
            _hashids = hashids;
            _roleService = roleService;
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _logger = logger;
        }

        public async Task CancelOrderById(string id)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = _getCurrentUserService.Execute(auth0UserInfo).Result;
            await _roleService.ThrowIfNoRole(RoleName.Needy, currentUser.Id);

            var decodedOrderId = _hashids.DecodeSingleLong(id);

            var dbOrderResult = await _appDbContext.Orders
                .Include(c => c.OrderProducts)!
                .SingleOrDefaultAsync(c => c.OwnerUserId == currentUser.Id && c.Id == decodedOrderId);

            if (dbOrderResult == null)
            {
                _logger.LogError("Couldn't find Order with id:{decodedOrderId} in database", decodedOrderId);
                throw new ItemNotFoundException("Sorry we couldn't find that order in database");
            }

            dbOrderResult.IsCanceledByUser = true;
            dbOrderResult.Status = DbEntityStatus.Inactive;

            if (!dbOrderResult.OrderProducts!.Any())
            {
                _logger.LogError("Couldn't find OrderProducts for Order with id:{decodedOrderId} in database", decodedOrderId);
                throw new ItemNotFoundException("Sorry we couldn't find related Products with your order in database");
            }

            foreach (var orderProduct in dbOrderResult.OrderProducts!)
            {
                orderProduct.Status = DbEntityStatus.Inactive;
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
