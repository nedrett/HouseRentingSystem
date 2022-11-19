using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    using Core.Models.House;
    using Extensions;
    using HouseRentingSystem.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Models;

    [Authorize]
    public class HousesController : Controller
    {
        private readonly IHouseService houseService;

        private readonly IAgentService agentService;

        public HousesController(
            IAgentService _agentService,
            IHouseService _houseService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var result = await houseService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = result.TotalHousesCount;
            query.Categories = await houseService.AllCategoriesNames();
            query.Houses = result.Houses;

            return View(query);
        }

        public async Task<IActionResult> Mine()
        {
            var model = new HousesQueryModel();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = new HouseDetailsModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await agentService.ExistById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            var model = new HouseModel
            {
                Categories = await houseService.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseModel model)
        {
            if (await agentService.ExistById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            if (await houseService.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategories();

                return View(model);
            }

            int agentId = await agentService.GetAgentId(User.Id());

            int id = await houseService.Create(model, agentId);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new HouseModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, HouseModel model)
        {
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
