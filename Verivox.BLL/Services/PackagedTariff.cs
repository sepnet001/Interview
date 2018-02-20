using System.Collections.Generic;
using System.Linq;
using Verivox.BLL.Models;
using Verivox.BLL.Models.Enums;
using Verivox.BLL.ServiceContracts;

namespace Verivox.BLL.Services
{
    public class PackagedTariff : BaseAbstractTariff
    {
        private const decimal BaseCost = 800;
        private const decimal ConsumptionCost = 0.30M;
        private const double UpperBoundConsumption = 4000;

        public PackagedTariff(ITariffValidator tariffValidator)
            : base(tariffValidator)
        {
        }

        public override Product Calculate(double consumption)
        {
            TariffValidator.Validate(consumption);

            return new Product
            {
                Consumption = consumption,
                TariffType = TariffType.PackagedTariff,
                AnnualCost = consumption <= UpperBoundConsumption ? BaseCost : BaseCost + ((decimal)(consumption - UpperBoundConsumption) * ConsumptionCost)
            };
        }

        public override IEnumerable<Product> Calculate(IEnumerable<double> consumptions)
        {
            return consumptions.Select(Calculate).ToList();
        }
    }
}
