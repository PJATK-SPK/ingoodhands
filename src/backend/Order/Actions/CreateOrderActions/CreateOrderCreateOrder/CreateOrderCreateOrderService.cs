using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using FluentValidation;
using HashidsNet;
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
        private readonly ILogger<CreateOrderCreateOrderService> _logger;
        private readonly OrderNameBuilderService _orderNameBuilderService;
        private readonly CounterService _counterService;
        private readonly CreateOrderCreateOrderPayloadValidator _createOrderCreateOrderPayloadValidator;

        public CreateOrderCreateOrderService(
            AppDbContext appDbContext,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            ILogger<CreateOrderCreateOrderService> logger,
            RoleService roleService,
            CreateOrderCreateOrderPayloadValidator createOrderCreateOrderPayloadValidator,
            OrderNameBuilderService orderNameBuilderService,
            CounterService counterService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _logger = logger;
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

            var listOfProducts = await _appDbContext.Products.Where(c => c.Status == DbEntityStatus.Active).FromCache().ToDynamicListAsync();
            if (!listOfProducts.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var listOfOrderProduct = payload.Products.Select(c => new OrderProduct
            {
                ProductId = _hashids.DecodeSingleLong(c.Id),
                Quantity = c.Quantity,
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            }).ToList();

            var nextOrderId = _counterService.GetAndUpdateNextCounter(TableName.Orders);
            var orderName = _orderNameBuilderService.Build(nextOrderId.Result);

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
                OrdereName = orderName,
            };

            return response;
        }
    }
}
