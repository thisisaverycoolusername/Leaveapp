using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;


namespace WebApplication5.Controllers
{
    public class LeaveRequestsController : Controller
    {
        private readonly LeaveRequestContext _context;

        public LeaveRequestsController(LeaveRequestContext context)
        {
            _context = context;
        }

        // GET: LeaveRequests
        public async Task<ActionResult> Index(string sortOrder, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.EmployeeIdSortParam = sortOrder == "EmployeeId" ? "employeeId_desc" : "EmployeeId";
            ViewBag.DateNowSortParam = sortOrder == "DateNow" ? "dateNow_desc" : "DateNow";

            var leaveRequests = from lr in _context.LeaveRequests
                                select lr;

            if (fromDate.HasValue)
            {
                leaveRequests = leaveRequests.Where(lr => lr.StartDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                leaveRequests = leaveRequests.Where(lr => lr.EndDate <= toDate.Value);
            }

            switch (sortOrder)
            {
                case "employeeId_desc":
                    leaveRequests = leaveRequests.OrderByDescending(lr => lr.EmployeeId);
                    break;
                case "DateNow":
                    leaveRequests = leaveRequests.OrderBy(lr => lr.DateNow);
                    break;
                case "dateNow_desc":
                    leaveRequests = leaveRequests.OrderByDescending(lr => lr.DateNow);
                    break;
                default:
                    leaveRequests = leaveRequests.OrderBy(lr => lr.EmployeeId);
                    break;
            }

            return View(await leaveRequests.ToListAsync());
        }

        //public async Task<IActionResult> Index()
        //{
        //    var leaveRequestContext = _context.LeaveRequests.Include(l => l.Employee);
        //    return View(await leaveRequestContext.ToListAsync());
        //}

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveRequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email");
            return View();
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveRequestId,EmployeeId,StartDate,EndDate,DateNow,Reason,IsApproved")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                leaveRequest.DateNow = DateTime.Now;
                _context.Add(leaveRequest);
                leaveRequest.DateNow = DateTime.Now;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveRequestId,EmployeeId,StartDate,EndDate,DateNow,Reason,IsApproved")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.LeaveRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.LeaveRequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveRequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.LeaveRequestId == id);
        }
    }
}