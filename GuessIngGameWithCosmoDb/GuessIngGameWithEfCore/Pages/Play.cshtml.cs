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

        public PlayModel(IGuessRepository guessRepository, IContestRepository contestRepository)
        {
            _contestRepository = contestRepository;
            _guessRepository = guessRepository;
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            if(id == default)
            {
                return NotFound();
            }
            bool Found = await _contestRepository.ExistsAsync(id);
           
            if(!Found)
            {
                return NotFound();
            }
            contest = id;
            return Page();
        }

        [BindProperty]
        public CreateGuessRequest guess { get; set; } = default!;
        [BindProperty]
        public int contest { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || guess == null || guess.Contest  == default)
            {
                return Page();
            }
            Contest contest = await _contestRepository.GetAsync(guess.Contest);
            Guess newGuess = new Guess
            {
                Id = $"{guess.Id}",
                Number = guess.Number,
                Contestant = new Contestant
                {
                    Name = guess.Contestant.Name,
                    Email = guess.Contestant.Email,
                    Contest = contest
                }
            };
            contest.Play(newGuess);
            await _guessRepository.AddAsync(guess: newGuess);
            await _contestRepository.UpdateAsync(contest: newGuess.Contest);
            if (newGuess.Contestant.IsAWinner())
            {
                return RedirectToPage("./Won");
            }

            return RedirectToPage("./Lost");
        }
    }
}
