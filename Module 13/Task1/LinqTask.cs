using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result =
                from c in customers
                join s in suppliers
                on new { c.Country, c.City } equals new { s.Country, s.City }
                into suppliersGroup
                select (customer: c, suppliers: suppliersGroup);

            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.GroupJoin(
                suppliers,
                c => new { c.Country, c.City },
                s => new { s.Country, s.City },
                (c, supplierGroup) => (
                    customer: c,
                    suppliers: supplierGroup
                ));

            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            Func<Order, bool> hasOrderAboveLimit = order => order.Total > limit;
            return customers.Where(c => c.Orders.Any(hasOrderAboveLimit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var result = customers
                .Where(c => c.Orders.Any())
                .Select(c => (customer: c, dateOfEntry: c.Orders.Min(o => o.OrderDate)));

            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = customers
                .Where(c => c.Orders.Any())
                .Select(c => (customer: c, dateOfEntry: c.Orders.Min(o => o.OrderDate)))
                .OrderBy(i => i.dateOfEntry.Year)
                .ThenBy(i => i.dateOfEntry.Month)
                .ThenByDescending(i => i.customer.Orders.Sum(o => o.Total))
                .ThenBy(i => i.customer.CompanyName);

            return result;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            var result = customers
                .Where(c => 
                    !c.PostalCode.All(char.IsDigit) || 
                    c.Region is null ||
                    !Regex.IsMatch(c.Phone, @"\(\d+\)"));

            return result;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            var result = 
                from p in products
                group p by p.Category into categoryGroup
                select new Linq7CategoryGroup
                {
                    Category = categoryGroup.Key,
                    UnitsInStockGroup = 
                        from productInCategory in categoryGroup
                        group productInCategory by productInCategory.UnitsInStock into stockGroup
                        select new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = stockGroup.Key,
                            Prices = stockGroup.Select(p => p.UnitPrice).OrderBy(price => price).ToArray()
                        }
                };

            return result;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            decimal CalculateCategory(decimal price)
            {
                if (price > 0 && price <= cheap) return cheap;
                if (price > cheap && price <= middle) return middle;
                if (price > middle && price <= expensive) return expensive;
                return 0;
            }

            var result =
                from p in products
                group p by CalculateCategory(p.UnitPrice)
                into priceGroup
                select (category: priceGroup.Key, products: priceGroup.AsEnumerable());

            return result;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            var result = customers
                .GroupBy(c => c.City)
                .Select(g => 
                (
                    city: g.Key,
                    averageIncome: (int)Math.Round(g.Average(c => c.Orders.Sum(o => o.Total))),
                    averageIntensity: (int)g.Average(c => c.Orders.Count())
                ));

            return result;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var countries = suppliers
                .Select(s => s.Country)
                .Distinct()
                .OrderBy(c => c.Length)
                .ThenBy(c => c);
            

            return string.Join("", countries);
        }
    }
}