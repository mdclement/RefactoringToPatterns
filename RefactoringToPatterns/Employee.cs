using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RefactoringToPatterns
{
    [TestFixture]
    public class EmployeeTests
    {
        [TestCase(EmployeeType.Minion, 10000)]
        [TestCase(EmployeeType.CoinOperated, 15000)]
        [TestCase(EmployeeType.Overlord, 30000)]
        public void Paycheck(EmployeeType employeeType, decimal expectedPaycheck)
        {
            const decimal baseSalary = 10000m;
            const decimal commission = 5000m;
            const decimal bonus = 20000m;
            const decimal healthPremium = 0m;
            var employee = new Employee(employeeType, baseSalary, commission, bonus, healthPremium);
            decimal paycheck = employee.GetGrossPaycheck();
            Assert.That(paycheck, Is.EqualTo(expectedPaycheck));
        }

        [TestCase(EmployeeType.Minion, 1000)]
        [TestCase(EmployeeType.CoinOperated, 500)]
        [TestCase(EmployeeType.Overlord, 500)]
        public void BenefitDeductions(EmployeeType employeeType, decimal expectedDeduction)
        {
            const decimal baseSalary = 0m;
            const decimal commission = 0m;
            const decimal bonus = 0m;
            const decimal healthPremium = 1000m;
            var employee = new Employee(employeeType, baseSalary, commission, bonus, healthPremium);
            decimal deduction = employee.GetDeductionTotal();
            Assert.That(deduction, Is.EqualTo(expectedDeduction));
        }
    }

    public enum EmployeeType
    {
        Minion,
        CoinOperated,
        Overlord
    }

    public class Employee
    {
        public EmployeeType EmployeeType { get; private set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Commission { get; set; }

        public decimal HealthPremium { get; set; }

        public Employee(EmployeeType employeeType,
                        decimal baseSalary,
                        decimal commission,
                        decimal bonus,
                        decimal healthPremium)
        {
            EmployeeType = employeeType;
            BaseSalary = baseSalary;
            Commission = commission;
            Bonus = bonus;
            HealthPremium = healthPremium;
        }

        public decimal GetGrossPaycheck()
        {
            switch (EmployeeType)
            {
                case EmployeeType.Minion:
                    return BaseSalary;
                case EmployeeType.CoinOperated:
                    return BaseSalary + Commission;
                case EmployeeType.Overlord:
                    return BaseSalary + Bonus;
                default:
                    return 0;
            }
        }

        public decimal GetDeductionTotal()
        {
            switch (EmployeeType)
            {
                case EmployeeType.Minion:
                    return HealthPremium;
                case EmployeeType.CoinOperated:
                    return HealthPremium*.5m;
                case EmployeeType.Overlord:
                    return HealthPremium*.5m;
                default:
                    return 0;
            }
        }
    }
}
