using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Macaner.Ecomerce.Application.DTO;
using Macaner.Ecomerce.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Macaner.Ecomerce.Services.WebApi.Controllers
{
    //[Route("api/[controller]/[action]")]
    //[Authorize]
    [ApiController]
    public class CustomersController : Controller
    {
       
        private readonly ICustomerApplication _customerApplication;

        public CustomersController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        #region "Metodos Sincronos"
        [Route("api/Customers/Insert")]
        [HttpPost]
        public IActionResult Insert([FromBody]/*opcional*/CustomerDTO customerDTO)
        {
            if (customerDTO == null) return BadRequest();

            var response = _customerApplication.Insert(customerDTO);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response.Message);
        }
        [Route("api/Customers/Update")]
        [HttpPut]
        public IActionResult Update([FromBody]/*opcional*/CustomerDTO customerDTO)
        {
            if (customerDTO == null) return BadRequest();

            var response = _customerApplication.Update(customerDTO);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response.Message);
        }

        [Route("api/Customers/Delete")]
        //[HttpDelete("{customerId}")]
        [HttpDelete]

        public IActionResult Delete([FromBody]/*opcional*/string customerId)
        {
            if (String.IsNullOrEmpty(customerId)) return BadRequest();

            var response = _customerApplication.Delete(customerId);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response.Message);
        }

        [Route("api/Customers/Get")]
        //[HttpGet("api/Customers/Get/{customerId}")]
        //[HttpGet("api/Customers/Get")]
        [HttpGet]
        public IActionResult Get([FromBody]/*opcional*/string customerId)
        {
            if (String.IsNullOrEmpty(customerId)) return BadRequest();

            var response = _customerApplication.Get(customerId);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response.Message);
        }

        [Route("api/Customers/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _customerApplication.GetAll();

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion

        //#region "Metodos Asincronos"
        //[HttpPost]
        //public async Task<IActionResult> InsertAsync([FromBody]/*opcional*/CustomerDTO customerDTO)
        //{
        //    if (customerDTO == null) return BadRequest();

        //    var response = await _customerApplication.InsertAsync(customerDTO);

        //    if (response.IsSuccess) return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync([FromBody]/*opcional*/CustomerDTO customerDTO)
        //{
        //    if (customerDTO == null) return BadRequest();

        //    var response = await _customerApplication.UpdateAsync(customerDTO);

        //    if (response.IsSuccess) return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpDelete("{customerId}")]
        //public async Task<IActionResult> DeleteAsync([FromBody]/*opcional*/string customerId)
        //{
        //    if (String.IsNullOrEmpty(customerId)) return BadRequest();

        //    var response = await _customerApplication.DeleteAsync(customerId);

        //    if (response.IsSuccess) return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpGet("{customerId}")]
        //public async Task<IActionResult> GetAsync([FromBody]/*opcional*/string customerId)
        //{
        //    if (String.IsNullOrEmpty(customerId)) return BadRequest();

        //    var response = await _customerApplication.GetAsync(customerId);

        //    if (response.IsSuccess) return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var response = await _customerApplication.GetAllAsync();

        //    if (response.IsSuccess) return Ok(response);

        //    return BadRequest(response.Message);
        //}
        //#endregion
    }
}
