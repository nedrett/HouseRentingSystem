using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Agent;
    using Extensions;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AgentsController : Controller
    {
        private readonly IAgentService agentService;

        public AgentsController(IAgentService _agentService)
        {
            agentService = _agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await agentService.ExistById(User.Id()))
            {
                return BadRequest();
            }

            var model = new BecomeAgentModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await agentService.ExistById(userId))
            {
                return BadRequest();
            }

            if (await agentService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exists. Enter another one.");
            }

            if (await agentService.UserHasRent(userId))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "You should have no rents to become an agent!");
            }

            await agentService.Create(userId, model.PhoneNumber);

            return RedirectToAction("All", "Houses");
        }
    }
}
