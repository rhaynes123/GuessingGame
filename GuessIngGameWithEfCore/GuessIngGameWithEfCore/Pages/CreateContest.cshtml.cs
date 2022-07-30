using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GuessIngGameWithEfCore.Data;
using GuessIngGameWithEfCore.Models;
using GuessIngGameWithEfCore.DTOs;
using GuessIngGameWithEfCore.Repositories;

namespace GuessIngGameWithEfCore.Pages
{
    public class CreateContestModel : PageModel
    {
        private readonly IContestRepository _contestRepository;

        public CreateContestModel(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository ?? throw new ArgumentNullException(nameof(contestRepository));
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

            bool saved = await _contestRepository.AddAsync(new Contest(contest: contest));

            if(!saved)
            {
                ModelState.AddModelError(string.Empty, "Contest Was Not Saved");
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
