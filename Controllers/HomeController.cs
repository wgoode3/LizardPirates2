using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LizardPirates.Models;
using Microsoft.AspNetCore.Mvc;

namespace LizardPirates.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context { get; set; }

        public HomeController (MyContext context)
        {
            _context = context;
        }

        [HttpGet ("")]
        public IActionResult Index ()
        {
            return View ();
        }

        [HttpPost ("process")]
        public IActionResult Process (Lizard newbie)
        {
            if (ModelState.IsValid)
            {
                _context.Lizards.Add (newbie);
                _context.SaveChanges ();
                return Redirect ("/pirates");
            }
            else
            {
                return View ("Index");
            }
        }

        [HttpGet ("pirates")]
        public IActionResult Pirates ()
        {
            List<Lizard> AllPirates = _context.Lizards.ToList ();
            return View (AllPirates);
        }

        [HttpPost("search")]
        public IActionResult Search(string q)
        {
            List<Lizard> SearchResults = _context.Lizards
                                            .Where(
                                                lp => lp.PirateRole.Contains(q) || 
                                                lp.LizardType.Contains(q) || 
                                                lp.Name.Contains(q)
                                            )
                                            .ToList();
            return View("Pirates", SearchResults);
        }

        [HttpGet ("pirate/{PId}")]
        public IActionResult EditPirate (int PId)
        {
            Lizard lizard = _context.Lizards.FirstOrDefault(lp => lp.LizardId == PId);
            return View(lizard);
        }

        [HttpPost ("pirate/{PId}/update")]
        public IActionResult UpdatePirate (int PId, Lizard l)
        {
            if (ModelState.IsValid)
            {
                Lizard lizard = _context.Lizards.FirstOrDefault(lp => lp.LizardId == PId);
                lizard.Name = l.Name;
                lizard.PirateRole = l.PirateRole;
                lizard.LizardType = l.LizardType;
                lizard.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return Redirect("/pirates");
            }
            else
            {
                l.LizardId = PId;
                return View("EditPirate", l);
            }
        }
    }
}