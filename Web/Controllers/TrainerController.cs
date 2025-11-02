namespace Web.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        public IActionResult TrainerDetails(int Id)
        {
            var trainer = _trainerService.GetTrainerDetails(Id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data!!");
                return View(nameof(Create), input);
            }

            bool createTrainer = _trainerService.CreateTrainer(input);

            if (createTrainer)
                TempData["SuccessMessage"] = "Trainer Created Successfully!";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Create ,Email or Phone Number Already Exists!";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult TrainerEdit(int Id)
        {
            var trainer = _trainerService.GetTrainerToUpdate(Id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }

        [HttpPost]
        public IActionResult TrainerEdit([FromRoute] int Id, TrainerToUpdateViewModel input)
        {
            if (!ModelState.IsValid)
            {

                return View(input);
            }

            bool updateTrainer = _trainerService.UpdateTrainer(Id, input);

            if (updateTrainer)
                TempData["SuccessMessage"] = "Trainer Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Update ,Email or Phone Number Already Exists!";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "ID cannot be Null or Negative!";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(Id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = Id;
            return View();

        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = _trainerService.RemoveTrainer(id);

            if (result)
                TempData["SuccessMessage"] = "Trainer Deleted Successfully!";
            else
                TempData["ErrorMessage"] = "Trainer Cannot be Deleted!";

            return RedirectToAction(nameof(Index));

        }
    }
}
