using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CollegeSystemMVC.Data;
using CollegeSystemMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CollegeSystemMVC.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public StaffManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StaffManager

        public IActionResult SearchUserIndex()
        {
            return View();
        }

        public async Task<IActionResult> SearchUserResults(SearchTerm nameToSearch)
        {
            if (ModelState.IsValid)
            {

                var matchingUsers = from m in _context.Users
                                    select m;

                if (!String.IsNullOrEmpty(nameToSearch.SearchName))
                {
                    matchingUsers = matchingUsers.Where(s => s.FullName.Contains(nameToSearch.SearchName));
                }

                return View(await matchingUsers.ToListAsync());
            }
            return RedirectToAction(nameof(SearchUserIndex));
        }
        //public async Task<IActionResult> SearchUserIndex()
        //{
        //    return View(await _context.User.ToListAsync());
        //}

        // GET: StaffManager/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: StaffManager/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: StaffManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FullName,Role")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.Id = Guid.NewGuid();
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        // GET: StaffManager/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userModel = new User { Id = user.Id, FullName = user.FullName, Email = user.Email, PhoneNumber = user.PhoneNumber, SecurityStamp = user.SecurityStamp};
            return View(userModel);
        }

        // POST: StaffManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FullName,Email,PhoneNumber,SecurityStamp")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = new ApplicationUser { Id = user.Id, FullName = user.FullName, Email = user.Email, PhoneNumber = user.PhoneNumber, SecurityStamp = user.SecurityStamp};
                    await _userManager.UpdateAsync(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SearchUserIndex));
            }
            return View(user);
        }

        // GET: StaffManager/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = new User { Id = user.Id, FullName = user.FullName};

            return View(userViewModel);
        }

        // POST: StaffManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
           await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SearchUserIndex));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
