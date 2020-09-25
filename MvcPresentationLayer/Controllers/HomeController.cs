using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WebApiLayer.Models;
using System.Net.Http;
using System.Threading.Tasks;
using NLog;
using Newtonsoft.Json;
using System.Text;
using MvcPresentationLayer.ViewModel;
using Newtonsoft.Json.Linq;

namespace MvcPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();


        public ActionResult Index(int? page)
        {
            return View(GetEmployee(page));
        }

        public ActionResult Get(int id)
        {
            return View("Index", GetEmployeeByID(id));
        }


        public ActionResult Edit(int Id)
        {
            return View(GetEmployeeByID(Id));
        }

        [HttpPost]
        public ActionResult Edit(int Id, string Name, long ContactNumber, string Address)
        {
            //TODO

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(string Name, long ContactNumber, string Address)
        {
            EmployeeDTO newEmployee = new EmployeeDTO { ID = 1, Name = Name, ContactNumber = ContactNumber, Address = Address };
            PostEmployee(newEmployee);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int Id)
        {
            var client = new HttpClient();
            var deleteTask = client.DeleteAsync("http://127.0.0.1:8080/api/employee/" + Id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                _logger.Error("Remove Employee " + Id.ToString() + " Unsuccessful");
            }

            return RedirectToAction("Index");
        }


        private EmployeeDTO GetEmployeeByID(int id)
        {

            var client = new HttpClient();
            var resultTask = client.GetAsync("http://127.0.0.1:8080/api/employee");
            resultTask.Wait();
            var result = resultTask.Result;

            EmployeeDTO employee = null;
            if (result.IsSuccessStatusCode)
            {
                employee = result.Content.ReadAsAsync<EmployeeDTO>().Result;
            }
            else
            {
                _logger.Error("Get Employee unsuccessful" + result.StatusCode.ToString());
            }

            return employee;
        }

        private IndexViewModel GetEmployee(int? page)
        {
            int DEFAULT_PAGE_SIZE = 5;
            PageModel pageModel = null;
            HttpClient client = new HttpClient();
            var resultTask = client.GetAsync("http://127.0.0.1:8080/api/employee/?pageNumber=" + page.ToString() + "&pageSize=" + DEFAULT_PAGE_SIZE.ToString());
            resultTask.Wait();

            var result = resultTask.Result;
            List<EmployeeDTO> employeesDTO = null;
            if (result.IsSuccessStatusCode)
            {
                employeesDTO = result.Content.ReadAsAsync<List<EmployeeDTO>>().Result;
                IEnumerable<string> headerValues;
                var PageInfo = string.Empty;

                if (result.Headers.TryGetValues("Paging-Headers", out headerValues))
                {
                    PageInfo = headerValues.FirstOrDefault();
                }
                JToken headerObject = JsonConvert.DeserializeObject<JToken>(PageInfo);
                pageModel = headerObject.ToObject<PageModel>();
            }
            else
            {
                _logger.Error("Get Employee by page unsuccessful" + result.StatusCode.ToString());
            }

            return new IndexViewModel { Employees = employeesDTO, PageModel = pageModel };
        }



        private void PostEmployee(EmployeeDTO employee)
        {
            var json = JsonConvert.SerializeObject(employee);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://127.0.0.1:8080/api/employee";
            var client = new HttpClient();

            var response = client.PostAsync(url, data);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                //TODO
            }
            else
            {
                _logger.Error("Post Employee unsuccessful" + result.StatusCode.ToString());
            }
        }

    }
}
