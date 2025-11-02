using Core.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult MemberDetails(int Id)
        {
            var member = _memberService.GetMemberDetails(Id);
            if(member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult HealthRecordDetails(int Id)
        {
            var healthRecord = _memberService.GetMemberHealthRecord(Id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "Health Record is Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel input)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data!!");
                return View(nameof(Create),input);
            }

            bool createMember = _memberService.CreateMember(input);

            if(createMember)
                TempData["SuccessMessage"] = "Member Created Successfully!";
            else
                TempData["ErrorMessage"] = "Member Failed To Create ,Email or Phone Number Already Exists!";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult MemberEdit(int Id)
        {
            var member = _memberService.GetMemberToUpdate(Id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute]int Id,MemberToUpdateViewModel input)
        {
            if (!ModelState.IsValid)
            {
               
                return View(input);
            }
          
            bool updateMember = _memberService.UpdateMember(Id,input);

            if (updateMember)
                TempData["SuccessMessage"] = "Member Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Member Failed To Update ,Email or Phone Number Already Exists!";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int Id)
        {
            if(Id <= 0)
            {
               TempData["ErrorMessage"] = "ID cannot be Null or Negative!";
               return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(Id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.MemberId = Id;
            return View();

        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm]int id)
        {
            var result = _memberService.RemoveMember(id);

            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully!";
            else
                TempData["ErrorMessage"] = "Member Cannot be Deleted!";

            return RedirectToAction(nameof(Index));

        }
    }
}
