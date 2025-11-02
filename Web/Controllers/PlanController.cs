namespace Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        public IActionResult Details(int Id)
        {
            if(Id <= 0)
            {
                TempData["ErrorMessage"] = "Plan ID can Not be Zero or Negative!";

                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanById(Id);
            if(plan == null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));

            }
            return View(plan);
        }

        public IActionResult Edit(int Id)
        {
            var plan = _planService.GetPlanToUpdate(Id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Can not be Edited!";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, UpdatePlanViewModel input)
        {
            if (!ModelState.IsValid)
            {

                return View(input);
            }

            bool updatePlan = _planService.UpdatePlan(Id, input);

            if (updatePlan)
                TempData["SuccessMessage"] = "Plan Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Plan Failed To Update !";

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult Activate(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "Plan ID can Not be Zero or Negative!";

                return RedirectToAction(nameof(Index));
            }

            var activate = _planService.Activate(Id);

            if (activate)
                TempData["SuccessMessage"] = "Plan Activated Successfully!";
            else
                TempData["ErrorMessage"] = "Plan Failed To Activate !";

            return RedirectToAction(nameof(Index));
        }

    }
}
