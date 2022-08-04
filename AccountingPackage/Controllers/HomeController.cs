using AccountingPackage.Data;
using AccountingPackage.Entities;
using AccountingPackage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingPackage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(TransRegViewModel model)
        {
            var ptransactor = _context.Transactors.FirstOrDefault(t => t.Email == model.Email);
            Transactor transactor;
            if(ptransactor is null)
            {
                transactor = new Transactor() { Email = model.Email, Name = model.Name };
                _context.Transactors.Add(transactor);
                _context.SaveChanges();
                var transaction1 = new Transaction()
                {
                    Amount = model.Amount,
                    Date = model.Date,
                    Debt = model.Credit == "yes" ? true : false,
                    Transactor = transactor,
                    TransactorId = transactor.Id
                };

                _context.Transactions.Add(transaction1);
                _context.SaveChanges();
            }
            else
            {
                var transaction = new Transaction()
                {
                    Amount = model.Amount,
                    Date = model.Date,
                    Debt = model.Credit == "yes" ? true : false,
                    Transactor = ptransactor,
                    TransactorId = ptransactor.Id
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }

            

            return RedirectToAction("Index");
        }

        public IActionResult All()
        {
            var transactions = _context.Set<Transaction>().Include(x => x.Transactor).ToList();
            return View(transactions);
        }

        public IActionResult Debtors()
        {
            var transactions = _context.Set<Transaction>().Where(x => x.Debt == true).Include(x => x.Transactor).ToList();
            return View(transactions);
        }
        [HttpPost]
        public IActionResult TransactionsByDate(TransRegViewModel model)
        {
            var transactions = _context.Transactions.Where(x => x.Date.Contains(model.Date)).Include(x => x.Transactor).ToList();
            return View(transactions);
        }
        [HttpPost]
        public IActionResult DebtorsTransactionsByDate(TransRegViewModel model)
        {
            var transactor = _context.Transactors.FirstOrDefault(x => x.Email == model.Email);
            if (transactor is null)
                return RedirectToAction("Index");

            var transactions = _context.Transactions.Where(x => x.TransactorId == transactor.Id && x.Date.Contains(model.Date)).Include(x => x.Transactor).ToList();
            return View(transactions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
