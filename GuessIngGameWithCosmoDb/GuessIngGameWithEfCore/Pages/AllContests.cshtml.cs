using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GuessingGameWithCosmodb.Models;
using GuessingGameWithCosmodb.Repositories;

namespace GuessingGameWithCosmodb.Pages
{
    public class AllContestsModel : PageModel
    {
        private readonly IContestRepository _contestRepository;

        public AllContestsModel(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository ?? throw new ArgumentNullException(nameof(contestRepository));
        }

        public IList<Contest> Contest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Contest = await _contestRepository.GetAllAsync();
        }
    }
}
