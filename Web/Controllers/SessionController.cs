namespace Web.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        public IActionResult Create()
        {
            LoadCategoriesDropDown();
            LoadTrainersDropDown();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSessionViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data!!");
                LoadCategoriesDropDown();
                LoadTrainersDropDown();
                return View(nameof(Create), input);
            }

            bool createSession = _sessionService.CreateSession(input);

            if (createSession)
                TempData["SuccessMessage"] = "Session Created Successfully!";
            else
            {
                TempData["ErrorMessage"] = "Session Failed To Create!";
                LoadCategoriesDropDown();
                LoadTrainersDropDown();
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int Id)
        {
            var session = _sessionService.GetSessionByID(Id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session is Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public IActionResult Edit(int Id)
        {
            if(Id <= 0)
            {
                TempData["ErrorMessage"] = "ID Can not be Zero or Negative!";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionToUpdate(Id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Can not be Edited!";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainersDropDown();
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, UpdateSessionViewModel input)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainersDropDown();
                return View(input);
            }

            bool updateSession = _sessionService.UpdateSession(Id, input);

            if (updateSession)
                TempData["SuccessMessage"] = "Session Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Session Failed To Update !";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "ID cannot be Null or Negative!";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionByID(Id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "session is Not Found!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionId = Id;
            return View();

        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = _sessionService.RemoveSession(id);

            if (result)
                TempData["SuccessMessage"] = "Session Deleted Successfully!";
            else
                TempData["ErrorMessage"] = "Session Cannot be Deleted!";

            return RedirectToAction(nameof(Index));

        }
        #region Helper Methods
        private void LoadCategoriesDropDown()
        {
            var categories = _sessionService.LoadCategoriesDropDown();
            ViewBag.Categories = new SelectList(categories, "Id","Name");
        }

        private void LoadTrainersDropDown()
        {
            var trainers = _sessionService.LoadTrainersDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        #endregion
    }
}
