using BidBoss.Data;
using BidBoss.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BidBoss.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BuyerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ✅ Fix IsBuyer() to check roles properly
        private async Task<bool> IsBuyer()
        {
            var user = await _userManager.GetUserAsync(User);
            return user != null && await _userManager.IsInRoleAsync(user, "Buyer");
        }

        // ✅ Fix `BrowseAuctions` query
        public async Task<IActionResult> BrowseAuctions()
        {
            if (!await IsBuyer())
                return Unauthorized("Access denied. Only buyers can view auctions.");

            var auctions = await _context.Auctions
                .Where(a => a.IsActive)
                .ToListAsync();  // ✅ Fix: `.ToListAsync()` is only available with `Microsoft.EntityFrameworkCore`

            return View(auctions);
        }

        // ✅ Fix `AuctionDetails` query
        public async Task<IActionResult> AuctionDetails(int id)
        {
            if (!await IsBuyer())
                return Unauthorized("Access denied. Only buyers can view auction details.");

            var auction = await _context.Auctions
                .FirstOrDefaultAsync(a => a.Id == id);  // ✅ Fix: Use `FirstOrDefaultAsync()` instead of `Find()`

            if (auction == null)
                return NotFound("Auction not found.");

            return View(auction);
        }

        // ✅ Fix `PlaceBid` to use async
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBid(int auctionId, decimal bidAmount)
        {
            if (!await IsBuyer())
                return Unauthorized("Access denied. Only buyers can place bids.");

            var auction = await _context.Auctions.FirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null)
                return NotFound("Auction not found.");

            var userId = _userManager.GetUserId(User);
            var newBid = new Bid
            {
                AuctionId = auctionId,
                BidderId = userId,
                Amount = bidAmount,
                BidTime = DateTime.Now
            };

            await _context.Bids.AddAsync(newBid);
            await _context.SaveChangesAsync();

            return RedirectToAction("AuctionDetails", new { id = auctionId });  // ✅ Ensure return statement
        }
    }
}
