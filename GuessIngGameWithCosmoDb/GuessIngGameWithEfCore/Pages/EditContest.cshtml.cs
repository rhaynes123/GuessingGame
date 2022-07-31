using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGameWithCosmodb.DTOs;
using GuessingGameWithCosmodb.Models;
using GuessingGameWithCosmodb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessIngGameWithCosmodb.Pages
{
    public class EditContestModel : PageModel
    {
        [BindProperty]
        public CreateOrUpdateContestRequest Contest { get; set; } = default!;
        private readonly IContestRepository _contestRepository;
        private readonly ILogger<EditContestModel> _logger;
        public EditContestModel(IContestRepository contestRepository
            , ILogger<EditContestModel> logger)
        {
            _contestRepository = contestRepository ?? throw new ArgumentNullException(nameof(contestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Contest contest = await _contestRepository.GetAsync(id);
            IEnumerable<CreateOrUpdatePrizeRequest>? prizes = contest.Prizes.Select(pr => new CreateOrUpdatePrizeRequest(0, pr.Description, (int)pr.Place, pr.IsWon, 0));
            Contest = new(0, Guid.Parse(contest.Id), contest.Name, contest.WinningNumber, contest.Active,prizes.ToList()) ;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Contest == null || Contest.Prizes is null || !Contest.Prizes.Any())
            {
                return Page();
            }

            (bool saved, string failureMessage) = await _contestRepository.TryUpdateAsync(new Contest(contest: Contest));

            if (!saved)
            {
                _logger.LogError(failureMessage);
                ModelState.AddModelError("Contest", failureMessage);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
