using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using FluentValidation;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Services.OrderNameBuilder;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderService
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly OrderNameBuilderService _orderNameBuilderService;
        private readonly CounterService _counterService;
        private readonly CreateOrderCreateOrderPayloadValidator _createOrderCreateOrderPayloadValidator;

        public CreateOrderCreateOrderService(
            AppDbContext appDbContext,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService,
            CreateOrderCreateOrderPayloadValidator createOrderCreateOrderPayloadValidator,
            OrderNameBuilderService orderNameBuilderService,
            CounterService counterService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
            _createOrderCreateOrderPayloadValidator = createOrderCreateOrderPayloadValidator;
            _orderNameBuilderService = orderNameBuilderService;
            _counterService = counterService;
        }

        public async Task<CreateOrderCreateOrderResponse> CreateOrder(CreateOrderCreateOrderPayload payload)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Needy, currentUser.Id);
            await _createOrderCreateOrderPayloadValidator.ValidateAndThrowAsync(payload);

            var thereIsOrderInThisLocation = await _appDbContext.Orders
                .Where(c => c.OwnerUserId == currentUser.Id && c.AddressId == _hashids.DecodeSingleLong(payload.AddressId))
                .AnyAsync();

            if (thereIsOrderInThisLocation)
                throw new ClientInputErrorException("You already added order for this location! Please finish or cancel existing order.");

            var listOfOrderProduct = payload.Products.Select(c => new OrderProduct
            {
                ProductId = _hashids.DecodeSingleLong(c.Id),
                Quantity = c.Quantity,
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            }).ToList();

            var nextOrderId = await _counterService.GetAndUpdateNextCounter(TableName.Orders);
            var orderName = _orderNameBuilderService.Build(nextOrderId);

            var newOrder = new Order
            {
                AddressId = _hashids.DecodeSingleLong(payload.AddressId),
                Name = orderName,
                Percentage = 0,
                OwnerUserId = currentUser.Id,
                OwnerUser = currentUser,
                CreationDate = DateTime.UtcNow,
                IsCanceledByUser = false,
                OrderProducts = listOfOrderProduct,
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };

            await _appDbContext.AddAsync(newOrder);
            await _appDbContext.SaveChangesAsync();

            var response = new CreateOrderCreateOrderResponse
            {
                OrderName = orderName,
            };

            return response;
        }
    }
}
