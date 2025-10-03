using FluentValidation;
using MetricsApi.Application.Commands.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Validators.Orders
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.Item).NotNull();
            RuleFor(x => x.Item.Quantity).GreaterThan(0);
            RuleFor(x => x.Item.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
