using EntityFrameworkSample.Domain.Models;

namespace EntityFrameworkSample.ViewModels
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {

        }

        public CompanyViewModel(Company company)
        {
            this.Name = company.Name;
            this.SuiteNumber = company.Office.SuiteNumber;
        }

        public CompanyViewModel(string name, string suiteNumber)
        {

            this.Name = name;
            this.SuiteNumber = suiteNumber;
        }

        public string Name { get; set; }

        public string SuiteNumber{ get; set; }
    }
}