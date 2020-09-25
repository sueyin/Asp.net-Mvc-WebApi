using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Gateway.Services;
using WebApiLayer.Models;
using AutoMapper;
using System.Net.Http;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace WebApiLayer.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://127.0.0.1:8079", headers: "*", methods: "*")]
    public class EmployeeController: ApiController
    {
        EmployeeRepository _employeeRepository;
        IMapper _iMapper;

        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
            });
            _iMapper = config.CreateMapper();

        }


        public IHttpActionResult Get([FromUri]PagingParameterModel pagingParameterModel)
        {
            //get Employees in range
            var employees = _employeeRepository.GetEmployeeByPage((pagingParameterModel.pageNumber - 1) * pagingParameterModel.pageSize, pagingParameterModel.pageSize);

            //get total num of employees
            var totalCount = _employeeRepository.GetEmployeeCount();

            var pageSize = pagingParameterModel.pageSize;
            var currentPage = pagingParameterModel.pageNumber;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pagingParameterModel.pageSize);

            var model = new PageModel { TotalPages = totalPages, CurrentPage = currentPage, PageSize = pageSize };
            // set response header
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(model));

            var employeesDTO = _iMapper.Map<List<Employee>, List<EmployeeDTO>>(employees);

            return Ok(employeesDTO); 
        }

        public IHttpActionResult Get(int Id)
        {
            return Ok(_iMapper.Map<Employee, EmployeeDTO>(_employeeRepository.Get(Id)));
        }

        public bool Post(EmployeeDTO employeeDTO)
        {
            Employee employee = _iMapper.Map<EmployeeDTO, Employee>(employeeDTO);
            bool status = _employeeRepository.Create(employee);

            return status;
        }

        public HttpResponseMessage Delete(int Id)
        {
            if (_employeeRepository.Get(Id) != null) {
                _employeeRepository.Delete(Id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Employee Id");
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int Id, EmployeeDTO employeeDTO)
        {
            var employee = _employeeRepository.Get(Id);

            if (employee != null)
            {
                if(_employeeRepository.Edit(_iMapper.Map<EmployeeDTO, Employee>(employeeDTO)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }
        }
    }
}
