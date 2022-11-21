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
            IEnumerable<HouseServiceModel> myHouses = null;

            var userId = User.Id();

            if (await agentService.ExistById(userId))
            {
                var currentAgentId = await agentService.GetAgentId(userId);

                myHouses = await houseService.AllHousesByAgentId(currentAgentId);
            }
            else
            {
                myHouses = await houseService.AllHousesByUserId(userId);
            }

            return View(myHouses);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var houseModel = await houseService.HouseDetailsById(id);

            return View(houseModel);
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
            if (await houseService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (await houseService.HasAgentWithId(id, User.Id()) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HouseDetailsById(id);
            var houseCategoryId = await houseService.GetHouseCategoryId(house.Id);

            var model = new HouseModel()
            {
                Id = house.Id,
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = houseCategoryId,
                Categories = await houseService.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseModel model)
        {
            if (await houseService.Exists(model.Id) == false)
            {
                ModelState.AddModelError("", "House does not exist.");

                model.Categories = await houseService.AllCategories();
                
                return View(model);
            }

            if (await houseService.HasAgentWithId(model.Id, User.Id()) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await houseService.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
                model.Categories = await houseService.AllCategories();

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategories();

                return View(model);
            }

            await houseService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (await houseService.HasAgentWithId(id, User.Id()) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HouseDetailsById(id);
            var model = new HouseDetailsViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, HouseDetailsViewModel model)
        {
            if (await houseService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (await houseService.HasAgentWithId(id, User.Id()) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await houseService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (await agentService.ExistById(User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await houseService.IsRented(id))
            {
                return RedirectToAction(nameof(All));
            }

            await houseService.Rent(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await houseService.Exists(id) == false || await houseService.IsRented(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (await houseService.IsRentedByUserWithId(id, User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await houseService.Leave(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
