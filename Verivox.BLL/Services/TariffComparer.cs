using System;
using System.Collections.Generic;
using System.Linq;
using Verivox.BLL.Models;
using Verivox.BLL.ServiceContracts;

namespace Verivox.BLL.Services
{
    public class TariffComparer : ITariffComparer
    {
        private readonly ITariff _fistComparer;
        private readonly ITariff _secondTComparer;

        public TariffComparer(ITariff fistCompareTariff, ITariff secondTCompareariff)
        {
            _fistComparer = fistCompareTariff;
            _secondTComparer = secondTCompareariff;
        }

        public void Compare(double consumption)
        {
            var firstComparableProcduect = _fistComparer.Calculate(consumption);
            Display(firstComparableProcduect);

            var secondComparableProcduect = _secondTComparer.Calculate(consumption);
            Display(secondComparableProcduect);

            Console.WriteLine(new String('*', 50));
        }

        public void Compare(IEnumerable<double> consumptions)
        {
            consumptions.ToList().ForEach(Compare);
        }

        private void Display(Product product)
        {
            Console.WriteLine(new String('-', 50));
            Console.WriteLine($"Consumption = {product.Consumption}");
            Console.WriteLine($"TariffType = {product.TariffType}");
            Console.WriteLine($"AnnualCost = {product.AnnualCost}");
            Console.WriteLine(new String('-', 50));
        }
    }
}
