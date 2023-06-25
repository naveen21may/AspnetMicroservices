﻿using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(_discountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon =  await _discountRepository.GetDiscount(request.ProductName);
            if (coupon ==null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name = {coupon.ProductName} is not found."));
            }

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.CouponModel);

            var couponModel = await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation($"Discount is Successfully Created. ProductName :{coupon.ProductName} ");
            return _mapper.Map<CouponModel>(couponModel);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.CouponModel);
            var couponModel = await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount is Successfully updated. ProductName :{coupon.ProductName} ");
            return _mapper.Map<CouponModel>(couponModel);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse()
            {
                Success = deleted
            };
            return response;
        }
    }
}
