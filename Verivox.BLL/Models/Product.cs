using Verivox.BLL.Models.Enums;

namespace Verivox.BLL.Models
{
    public class Product
    {
        public double Consumption { get; set; }

        public TariffType TariffType { get; set; }

        public decimal AnnualCost { get; set; }
    }
}
