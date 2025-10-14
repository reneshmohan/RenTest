using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace ClientTest
{
    public class Tests
    {
        private const string dateFormat = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";

        [SetUp]
        public async Task Setup()
        {
            await HelperMethods.Login();
        }

        [Test]
        public async Task ExamTestCase()
        {
            await HelperMethods.Reset();

            await HelperMethods.BuyQuantity((int)FuelType.Gas, 1);

            // Nuclear is not available at this time
            await HelperMethods.BuyQuantity((int)FuelType.Nuclear, 2);
            await HelperMethods.BuyQuantity((int)FuelType.Electric, 3);
            await HelperMethods.BuyQuantity((int)FuelType.Oil, 4);
            var orders = await HelperMethods.GetOrders();


            var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);  

            var gasRecordFound = orders.FirstOrDefault(x => x.Fuel.ToLower() == "gas" && x.Quantity == 1 && 
                        DateTime.ParseExact(x.Time, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) > today
            );

            Assert.IsNotNull(gasRecordFound);

            var elecRecordFound = orders.FirstOrDefault(x => x.Fuel.ToLower() == "elec" && x.Quantity == 3 &&
                        DateTime.ParseExact(x.Time, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) > today
            );

            Assert.IsNotNull(elecRecordFound);


            var oilRecordFound = orders.FirstOrDefault(x => x.Fuel.ToLower() == "oil" && x.Quantity == 4 &&
                        DateTime.ParseExact(x.Time, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) > today
            );

            Assert.IsNotNull(oilRecordFound);

        }


        [Test]
        public async Task ExamTestCaseNegative()
        {
            await HelperMethods.Reset();

            await HelperMethods.BuyQuantity((int)FuelType.Gas, 1);

            // Nuclear is not available at this time
            await HelperMethods.BuyQuantity((int)FuelType.Nuclear, 2);
            await HelperMethods.BuyQuantity((int)FuelType.Electric, 3);
            await HelperMethods.BuyQuantity((int)FuelType.Oil, 4);
            var orders = await HelperMethods.GetOrders();


            var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);

            var gasRecordFound = orders.FirstOrDefault(x => x.Fuel.ToLower() == "gas" && x.Quantity == 2 &&
                        DateTime.ParseExact(x.Time, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) > today
            );

            Assert.IsNull(gasRecordFound);

            var elecRecordFound = orders.FirstOrDefault(x => x.Fuel.ToLower() == "elec" && x.Quantity == 3 &&
                        DateTime.ParseExact(x.Time, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) > today
            );

            Assert.IsNotNull(elecRecordFound);


            var oilRecordFound = orders.FirstOrDefault(x => x.Fuel.ToLower() == "oil" && x.Quantity == 4 &&
                        DateTime.ParseExact(x.Time, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) > today
            );

            Assert.IsNotNull(oilRecordFound);

        }

    }
}