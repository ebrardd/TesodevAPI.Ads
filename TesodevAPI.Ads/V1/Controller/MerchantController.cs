using Microsoft.AspNetCore.Mvc;
using TesodevAPI.Ads.Services;
using TesodevAPI.Ads.Repositories;
using TesodevAPI.Ads.Models;
using MongoDB.Bson;
using TesodevAPI.Ads.V1.Models.RequestModels;

namespace TesodevAPI.Ads.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchantController : ControllerBase
    {
        private readonly IService _service;
        public MerchantController(IService service)
        {
            _service = service;
        }


        /*  private readonly ILogger<MerchantController> _logger;

          public MerchantController(ILogger<MerchantController> logger)
          {
              _logger = logger;
          } */
        [HttpGet(Name = "{id:int}")] //sunucudanverialmakvesorguyapmak
        public IActionResult Get([FromRoute] int id)
        {
            var merchant = _service.ToBsonDocument();

            if (merchant is null)
                return NotFound();
            return Ok(merchant);
        }
        [HttpPost] //verigüncellemeksilmekdosyayüklemek
        public IActionResult Create([FromBody]Merchant merchant)
        {
            //return CreatedAtAction(nameof(CreateMerchant), merchant);
            try
            {
                if (merchant is null)
                    return BadRequest();
               
                return StatusCode(201, merchant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //Dısardan istek geldiginde controller service gider service repositorye gider

        }
    }
}


