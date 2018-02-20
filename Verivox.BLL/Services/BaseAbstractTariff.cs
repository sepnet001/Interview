using System.Collections.Generic;
using Verivox.BLL.Models;
using Verivox.BLL.ServiceContracts;

namespace Verivox.BLL.Services
{
    public abstract class BaseAbstractTariff : ITariff
    {
        public abstract Product Calculate(double consumption);

        public abstract IEnumerable<Product> Calculate(IEnumerable<double> consumptions);

        protected ITariffValidator TariffValidator { get; set; }

        protected BaseAbstractTariff(ITariffValidator tariffValidator)
        {
            TariffValidator = tariffValidator;
        }
    }
}
