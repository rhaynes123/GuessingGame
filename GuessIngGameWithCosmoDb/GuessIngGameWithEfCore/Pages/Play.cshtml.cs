using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GuessingGameWithCosmodb.Models;
using GuessingGameWithCosmodb.Repositories;
using GuessingGameWithCosmodb.DTOs;

namespace GuessingGameWithCosmodb.Pages
{
    public class PlayModel : PageModel
    {
        private readonly IGuessRepository _guessRepository;
        private readonly IContestRepository _contestRepository;
        private readonly ILogger<PlayModel> _logger;

        public PlayModel(IGuessRepository guessRepository
            , IContestRepository contestRepository
            , ILogger<PlayModel> logger)
        {
            _contestRepository = contestRepository;
            _guessRepository = guessRepository;
            _logger = logger;
        }


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            
            if(id == default)
            {
                return NotFound();
            }
            (bool Found, Contest FoundContest, string ErrorMessage) = await _contestRepository.TryGetAsync(id);
            if(!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                _logger.LogError(ErrorMessage);
                ModelState.AddModelError("Error",ErrorMessage);
                return Page();
            }
           
            if(!Found)
            {
                return NotFound();
            }
            contest = FoundContest;
            contestId = id;
            if (FoundContest.Prizes.All(p => p.IsWon))
            {
                ModelState.AddModelError("Error", "This contest has no more active prizes");
                return Page();
            }
            
            return Page();
        }

        [BindProperty]
        public CreateGuessRequest guess { get; set; } = default!;
        public Contest contest { get; set; }
        [BindProperty]
        public Guid contestId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || guess == null || contestId == default)
            {
                return Page();
            }
            (bool found, Contest contest, string message) = await _contestRepository.TryGetAsync(contestId);
            if (!string.IsNullOrWhiteSpace(message))
            {
                _logger.LogError(message);
                ModelState.AddModelError("Error", message);
                return Page();
            }
            if (!found)
            {
                return NotFound();
            }
            Guess newGuess = new (guess:guess);

            //await _guessRepository.AddAsync(guess: newGuess);
            bool won = contest.Play(newGuess);
            (bool Found, string ErrorMessage) = await _contestRepository.TryUpdateAsync(contest: contest);
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                _logger.LogError(ErrorMessage);
                ModelState.AddModelError("Error", ErrorMessage);
                return Page();
            }
            if (!Found)
            {
                return NotFound();
            }
            if (won)
            {
                return RedirectToPage("./Won");
            }

            return RedirectToPage("./Lost");
        }
    }
}
