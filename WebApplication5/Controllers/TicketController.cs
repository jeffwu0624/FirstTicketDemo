using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using SimpleCaptcha;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _service;
        private readonly ICaptchaValidator _captchaValidator;
        
        public TicketController(ITicketService service, ICaptchaValidator captchaValidator)
        {
            _service = service;
            _captchaValidator = captchaValidator;
        }

        // GET
        public IActionResult Index()
        {
            ViewBag.RemainingCount = _service.RemainingCount();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookTicket(string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("captcha","Captcha validation failed");

                return View(new BookResult(){ IsSuccess = false, Message = "Captcha validation failed"});
            }
            else
            {
                return RedirectToAction("Result", _service.Add(new Ticket()));
            }
        }

        public IActionResult Result(BookResult result)
        {
            return View(result);
        }
    }
}