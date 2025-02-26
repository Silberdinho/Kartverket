using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Kartverket.Data;
using Microsoft.AspNetCore.Authorization;
using Kartverket.Repositiories;
using Kartverket.Models;
using Kartverket.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using Kartverket.Models.DomainModels;

namespace Kartverket.Controllers
{

    /// <summary>
    /// Controller responsible for handling homepage-related actions, including area change management,
    /// error handling and user submission history.
    /// </summary>
    public class HomeController : Controller
    {


        private readonly IAreaChangeRepository _areaChangeRepository; // Repository for managing AreaChanges

        private readonly UserManager<Users> _userManager; // from asp net core identity


        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class with required dependencies.
        /// </summary>
        /// <param name="areaChangeRepository">Repository for handling area changes</param>
        /// <param name="userManager">Manages user-related functionalities</param>
        public HomeController(IAreaChangeRepository areaChangeRepository, UserManager<Users> userManager)
        {
            _areaChangeRepository = areaChangeRepository;

            _userManager = userManager;
        }


        /// <summary>
        /// Displays the homepage of the application.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Displays a form for registering a new area change.
        /// </summary>
        [Authorize(Roles = "Caseworker, PrivateUser")]
        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }


        /// <summary>
        /// Handles submission of new area change
        /// </summary>
        /// <param name="areaChangesViewModel">View model containing area change details</param>
        /// <returns>A redirect to the appropriate view based on user role, or an error response</returns>
        [Authorize(Roles = "Caseworker, PrivateUser")]
        [HttpPost]
        public async Task<IActionResult> RegisterAreaChange(AreaChangesViewModel areaChangesViewModel)
        {

            var currentUser = await _userManager.GetUserAsync(User);


            if (areaChangesViewModel != null && currentUser != null)
            {
                var newAreaChange = new AreaChangeModel
                {

                    UserName = currentUser.UserName,
                    Email = currentUser.Email,
                    Kommunenavn = areaChangesViewModel.ViewKommunenavn,
                    Fylkenavn = areaChangesViewModel.ViewFylkenavn,
                    Description = areaChangesViewModel.ViewDescription,
                    AreaJson = areaChangesViewModel.ViewAreaJson,
                    StatusId = 4,
                    Date = areaChangesViewModel.ViewDate
                };

                await _areaChangeRepository.AddAsync(newAreaChange);

                // set success message
                TempData["ReportSuccess"] = "Your report has been submitted successfully!";


                // redirect based on user role
                if (User.IsInRole("Caseworker"))
                {
                    return RedirectToAction("AreaChangeOverview", "Home");
                }
                else
                {
                    return RedirectToAction("ReportSuccess", "Home");
                }
            }

            return BadRequest("An error occured");

        }


        /// <summary>
        /// Displays a success message after submitting an area change.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker, PrivateUser")]
        [HttpGet]
        public IActionResult ReportSuccess()
        {
            ViewBag.SuccessMessage = TempData["ReportSuccess"] as string;
            return View();
        }


        /// <summary>
        /// Displays an overview of all submitted area changes
        /// </summary>
        /// <returns>A view with a list of changes.</returns>
        [Authorize(Roles = "Caseworker")]
        [HttpGet]
        public async Task<IActionResult> AreaChangeOverview()
        {

            var getDTOChanges = await _areaChangeRepository.GetAllAsync();

            if (getDTOChanges != null && getDTOChanges.Any())
            {
                var areaChangeViewModel = getDTOChanges.Select(change => new AreaChangesViewModel
                {
                    ViewKommunenavn = change.Kommunenavn,
                    ViewFylkenavn = change.Fylkenavn,
                    ViewDescription = change.Description,
                    ViewAreaJson = change.AreaJson,
                    Email = change.Email,
                    Submitter = change.UserName,
                    Id = change.Id,
                    Status = change.SubmitStatusModel.Status
                }).ToList();  // `ToList()` should be applied after `Select`, not before

                return View(areaChangeViewModel);
            }

            return NotFound();  // Return a proper HTTP response instead of `null`
        }


        /// <summary>
        /// Retrieves and displays an area change for editing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker")]
        [HttpGet]
        public async Task<IActionResult> EditAreaChangeView(int id)
        {
            var getDTOChanges = await _areaChangeRepository.FindCaseById(id);
            if (getDTOChanges != null)
            {
                AreaChangesViewModel areaChangeViewModel = new AreaChangesViewModel
                {

                    ViewKommunenavn = getDTOChanges.Kommunenavn,
                    ViewFylkenavn = getDTOChanges.Fylkenavn,
                    ViewDescription = getDTOChanges.Description,
                    ViewAreaJson = getDTOChanges.AreaJson
                };

                return View(areaChangeViewModel);


            }

            return null;
        }


        /// <summary>
        /// Updates an existing area change.
        /// </summary>
        /// <param name="areaChangesViewModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker")]
        [HttpPost]
        public async Task<IActionResult> EditAreaChange(AreaChangesViewModel areaChangesViewModel)
        {
            var existingAreaChange = await _areaChangeRepository.FindCaseById(areaChangesViewModel.Id);

            var currentUser = await _userManager.GetUserAsync(User);

            if (existingAreaChange != null && currentUser != null)
            {

                existingAreaChange.Kommunenavn = areaChangesViewModel.ViewKommunenavn;
                existingAreaChange.AreaJson = areaChangesViewModel.ViewAreaJson;
                existingAreaChange.Fylkenavn = areaChangesViewModel.ViewFylkenavn;
                existingAreaChange.Description = areaChangesViewModel.ViewDescription;
                existingAreaChange.CaseWorker = currentUser.FirstName + " " + currentUser.LastName;


                await _areaChangeRepository.UpdateAsync(existingAreaChange);
                return RedirectToAction("AreaChangeOverview");
            }

            return null;
        }


        /// <summary>
        /// Deletes an area change
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker")]
        [HttpPost]
        public async Task<IActionResult> DeleteAreaChange(int id)
        {
            var areaChange = await _areaChangeRepository.FindCaseById(id);
            if (areaChange == null)
            {

                return NotFound($"AreaChange with ID {id} not found.");
            }

            await _areaChangeRepository.DeleteAsync(id);
            return RedirectToAction("AreaChangeOverview", "Home");
        }


        /// <summary>
        /// Marks a submitted report as completed by a caseworker
        /// </summary>
        /// <param name="areaChangesViewModel">View model containing report details</param>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker")]
        [HttpPost]
        public async Task<IActionResult> FinishReport(AreaChangesViewModel areaChangesViewModel)
        {
            var existingAreaChange = await _areaChangeRepository.FindCaseById(areaChangesViewModel.Id);
            var currentUser = await _userManager.GetUserAsync(User);

            if (existingAreaChange != null && existingAreaChange.StatusId != 2)
            {
                // oppdater status og tilordne saksbehandler
                existingAreaChange.StatusId = 2; // ferdig behandlet
                existingAreaChange.CaseWorker = currentUser.FirstName + " " + currentUser.LastName;



                //ExistingAreaChange.Date = DateTime.UtcNow; 

                await _areaChangeRepository.UpdateAsync(existingAreaChange);
                return RedirectToAction("ReportSuccess");
            }

            return BadRequest("Saken er allerede ferdigbehandlet");

        }


        /// <summary>
        /// Denies a submitted report and updates its status
        /// </summary>
        /// <param name="areaChangesViewModel">View model containing report details</param>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker")]
        [HttpPost]
        public async Task<IActionResult> DeniedReport(AreaChangesViewModel areaChangesViewModel)
        {
            var existingAreaChange = await _areaChangeRepository.FindCaseById(areaChangesViewModel.Id);
            if (existingAreaChange != null)
            {
                existingAreaChange.StatusId = 3; // avslått

            }

            await _areaChangeRepository.UpdateAsync(existingAreaChange);
            return View(existingAreaChange);


        }


        /// <summary>
        /// Retrieves and displays the submission history for the logged-in user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Caseworker, PrivateUser")]
        [HttpGet]
        public async Task<IActionResult> UserSubmitHistory()
        {

            var getSubmits = await _areaChangeRepository.GetAllAsync();
            var currentSubmitter = await _userManager.GetUserAsync(User);

            if (getSubmits != null && currentSubmitter != null)
            {
                var areaChangesViewModel = getSubmits.Where(e => e.Email == currentSubmitter.Email)
                    .Select(r => new AreaChangesViewModel
                    {
                        CaseHandler = r.CaseWorker ?? "Ingen sakbehandler",
                        ViewDescription = r.Description,
                        ViewAreaJson = r.AreaJson,
                        ViewFylkenavn = r.Fylkenavn,
                        ViewKommunenavn = r.Kommunenavn,
                        Id = r.Id,
                        Status = r.SubmitStatusModel.Status
                    }
                    );
                return View(areaChangesViewModel);


            }

            return null;

        }
    }
}