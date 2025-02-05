using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BidBoss.Data;
using BidBoss.Models;

namespace BidBoss.Controllers
{
    [Authorize(Roles = "Seller")]  // ✅ Restricts all actions to "Seller" role
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ✅ 1. Create a Property Listing
        public IActionResult CreateAuction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuction(Auction auction)
        {
            if (ModelState.IsValid)
            {
                auction.SellerId = _userManager.GetUserId(User); // ✅ Assign logged-in seller
                _context.Auctions.Add(auction);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyAuctions");
            }
            return View(auction);
        }

        // ✅ 2. View Seller's Own Auctions
        public async Task<IActionResult> MyAuctions()
        {
            var sellerId = _userManager.GetUserId(User);
            var auctions = await _context.Auctions
                .Where(a => a.SellerId == sellerId)
                .ToListAsync();
            return View(auctions);
        }

        // ✅ 3. View Auction Details & Bids
        public async Task<IActionResult> AuctionDetails(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null || auction.SellerId != _userManager.GetUserId(User))
                return Unauthorized("You do not have permission to view this auction.");

            return View(auction);
        }

        // ✅ 4. Manage Auction Results
        public async Task<IActionResult> AuctionResults(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null || auction.SellerId != _userManager.GetUserId(User))
                return Unauthorized("You do not have permission to manage this auction.");

            var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            ViewBag.WinningBid = highestBid;
            return View(auction);
        }
    }
}
