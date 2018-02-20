using System;
using Verivox.BLL.ServiceContracts;

namespace Verivox.BLL.Services
{
    public class TariffValidator : ITariffValidator
    {
        public void Validate(double consumption)
        {
            if (consumption <= 0)
                throw new ArgumentException($"Tariff can't be calculated with kWh = {consumption}");
        }
    }
}
