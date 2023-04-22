using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Core.Services;
using FluentValidation;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Orders.Services.OrderNameBuilder;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderService
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly OrderNameBuilderService _orderNameBuilderService;
        private readonly CounterService _counterService;
        private readonly NotificationService _notificationService;
        private readonly CreateOrderFetchService _fetchService;

        public CreateOrderCreateOrderService(
            AppDbContext appDbContext,
            Hashids hashids,
            OrderNameBuilderService orderNameBuilderService,
            CounterService counterService,
            NotificationService notificationService,
            CreateOrderFetchService fetchService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _orderNameBuilderService = orderNameBuilderService;
            _counterService = counterService;
            _notificationService = notificationService;
            _fetchService = fetchService;
        }

        public async Task<CreateOrderCreateOrderResponse> CreateOrder(CreateOrderCreateOrderPayload payload)
        {
            var currentUser = await _fetchService.GetUser(payload);

            var thereIsOrderInThisLocation = await _appDbContext.Orders
                .Where(c => c.OwnerUserId == currentUser!.Id && !c.IsCanceledByUser && c.Percentage != 100 && c.AddressId == _hashids.DecodeSingleLong(payload.AddressId))
                .AnyAsync();

            if (thereIsOrderInThisLocation)
                throw new ClientInputErrorException("You already added order for this location! Please finish or cancel existing order.");

            var listOfOrderProduct = payload.Products.Select(c => new OrderProduct
            {
                ProductId = _hashids.DecodeSingleLong(c.Id),
                Quantity = c.Quantity,
                UpdateUserId = currentUser!.Id,
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
                OwnerUserId = currentUser!.Id,
                OwnerUser = currentUser,
                CreationDate = DateTime.UtcNow,
                IsCanceledByUser = false,
                IsFinished = false,
                OrderProducts = listOfOrderProduct,
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };

            await _appDbContext.AddAsync(newOrder);
            await _appDbContext.SaveChangesAsync();

            await _notificationService.AddAsync(currentUser.Id, $"Your order {orderName} has been created!");

            var response = new CreateOrderCreateOrderResponse
            {
                OrderName = orderName,
            };

            return response;
        }
    }
}
