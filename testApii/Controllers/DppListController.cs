using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.DAL;
using testApii.Entity;
using testApii.Entity.API;

namespace TodoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DppListController : ControllerBase
{
    private readonly TestDbContext _context;
    private readonly IHelpers _helpers;
    public DppListController(TestDbContext context, IHelpers helpers)
    {
        _context = context;
        _helpers = helpers;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SantralValue>>> GetDppList(string uevcbEIC, DateTime startDate, DateTime endDate)
    {
        var injectionUnits = await _context.InjectionUnits.ToListAsync();
        var injectionUnit = injectionUnits.Where(e => e.EIC == uevcbEIC).FirstOrDefault();

        #region ParametersErrorControl
        if (injectionUnit == null)
        {
            return NotFound(new { message = $"Injection Unit doesn't find!" });
        }

        if (startDate > endDate)
        {
            return NotFound(new { message = "Start date can't be greater than end date!" });
        }

        if (endDate > DateTime.Now)
        {
            return NotFound(new { message = "End date can't be greater than today!" });
        }
        #endregion

        string parameters = $"?endDate={endDate:yyyy-MM-dd}&organizationEIC={injectionUnit.OrganizationETSOCode}&startDate={startDate:yyyy-MM-dd}&uevcbEIC={injectionUnit.EIC}";
        string dppListJSONString = _helpers.CallAPI("Kgup", parameters);
        var response = _helpers.Deserialize<Response<SantralValue>>(dppListJSONString);

        if (response.Body == null)
        {
            return NotFound();
        }
        return response.Body.dppList.ToList();
    }
}