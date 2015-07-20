using EntityFrameworkSample.DataAccessLayer;
using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;
using EntityFrameworkSample.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace EntityFrameworkSample.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private readonly ISampleDatabase database;

        public CompanyController(ISampleDatabase database)
        {
            this.database = database;
        }

        [HttpGet]
        [Route("create/{name}/{officeid:int}")]
        public async Task<IHttpActionResult> Create(string name, int officeId)
        {

            var company = new Company(name);
            company.Office = database.Get<Office>().FirstOrDefault(office => office.Id == officeId);

            this.database.Add(company);

            return await SaveAndReturn(company);
        }
        
        [Route("get/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var company = database.Get<Company>().Where(c => c.Id == id);
            var viewModel = new CompanyViewModel();

            if (company == null)
            {
                return BadRequest();
            }

            viewModel = (company.Select(c => new CompanyViewModel()
            {
                Name = c.Name,
                SuiteNumber = c.Office.SuiteNumber
            }).SingleOrDefault());

            /*
            if (company.Office != null)
            {
                viewModel = new CompanyViewModel()
                {
                    Name = company.Name,
                    SuiteNumber = company.Office.SuiteNumber
                };
            }*/

            return Ok(viewModel);
        }

        [Route("get")]
        public IHttpActionResult Get()
        {
            var viewModels = new List<CompanyViewModel>();
            var companies = this.database.Get<Company>();

            viewModels.AddRange(companies.Select(company => new CompanyViewModel()
            {
                Name = company.Name,
                SuiteNumber = company.Office.SuiteNumber
            }));

            return Ok(viewModels);
        }
        
        // write add office
        // add/remove contact(s)
        // get all company contacts
        // add contact controller and write add contact

        [Route("getallcontacts/{id:int}")]
        public IHttpActionResult GetAllContacts(int id)
        {
            var company = database.Get<Company>();

            return Ok(((Company)company).Contacts);
        }
        
        [HttpGet]
        [Route("changeoffice/{companyId:int}/{officeId:int}")]
        public async Task<IHttpActionResult> ChangeOffice(int companyId, int officeId)
        {
            var company = database.Get<Company>().SingleOrDefault(c => c.Id == companyId);
            var officeToAdd = database.Get<Office>().SingleOrDefault(o => o.Id == officeId);

            if (company == null || officeToAdd == null)
            {
                return BadRequest();
            }

            company.Office = officeToAdd;

            return await this.SaveAndReturn(company);
        }
        
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody]Company company)
        {
            var companyInDb = database.Get<Company>().SingleOrDefault(c => c.Id == company.Id);

            if (companyInDb == null)
            {
                return BadRequest();
            }

            companyInDb.Copy(company);

            //what to update?

            return await this.SaveAndReturn(company);
        }

        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == id);

            if (company == null)
            {
                return BadRequest();
            }

            return await this.SaveAndReturn(company);
        }

        private async Task<IHttpActionResult> SaveAndReturn(IEntity entity)
        {
            var error = await this.database.CommitAsync() == 0;

            if (error)
            {
                return InternalServerError();
            }

            return Ok(entity);
        }
    }
}
