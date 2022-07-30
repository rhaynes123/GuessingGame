using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GuessingGameWithCosmodb.Models;
using GuessingGameWithCosmodb.DTOs;
using GuessingGameWithCosmodb.Repositories;

namespace GuessingGameWithCosmodb.Pages
{
    public class CreateContestModel : PageModel
    {
        private readonly IContestRepository _contestRepository;
        private readonly ILogger<CreateContestModel> _logger;

        public CreateContestModel(IContestRepository contestRepository
            , ILogger<CreateContestModel> logger)
        {
            _contestRepository = contestRepository ?? throw new ArgumentNullException(nameof(contestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateOrUpdateContestRequest Contest { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync([FromBody] CreateOrUpdateContestRequest contest)
        {
            if (!ModelState.IsValid || contest == null || contest.Prizes is null || !contest.Prizes.Any())
            {
                return Page();
            }

            (bool saved, string failureMessage) = await _contestRepository.TryAddAsync(new Contest(contest: contest));

            if(!saved)
            {
                _logger.LogError(failureMessage);
                ModelState.AddModelError("Contest", failureMessage);
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
