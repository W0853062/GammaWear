using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GammaWear.Data;
using GammaWear.Models;
using GammaWear.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GammaWear.Controllers
{
    public class SockController : Controller
    {
        private readonly ILogger<SockController> _logger;
        private readonly GammaWearContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SockController(ILogger<SockController> logger, GammaWearContext context, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Sock
        public async Task<IActionResult> Index()
        {
            return View(await _context.Socks.ToListAsync());
            //return _context.Sock != null ?
            //            View(await _context.Sock.ToListAsync()) :
            //            Problem("Entity set 'GammaWearContext.Sock'  is null.");
        }

        // GET: Sock/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sock = await _context.Socks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sock == null)
            {
                return NotFound();
            }
            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == sock.MaterialId);
            var sockStyle = await _context.SockStyles.FirstOrDefaultAsync(s => s.Id == sock.SockStyleId);
            var outdoorSport = await _context.OutdoorSports.FirstOrDefaultAsync(o => o.Id == sock.OutdoorSportId);
            var season = await _context.Seasons.FirstOrDefaultAsync(s => s.Id == sock.SeasonId);
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == sock.BrandId);

            var viewModel = new SockDetailsViewModel
            {
                Id = sock.Id,
                MaterialName = material?.Name,
                SockStyleName = sockStyle?.Name,
                OutdoorSportName = outdoorSport?.Name,
                SockSize = sock.SockSize,
                ConsumerGroup = sock.ConsumerGroup,
                SeasonName = season?.Name,
                BrandName = brand?.Name,
                Price = sock.Price,
                Quantity = sock.Quantity,
                ImageFile = sock.ImageFile,
                Description = sock.Description
            };
            return View(viewModel);
        }

        // GET: Sock/Create
        public IActionResult Create()
        {
            _logger.LogInformation("--IActionResult Create: enter");
            PopulateDropDowns();
            return View();
        }

        // POST: Sock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialId,SockStyleId,OutdoorSportId,ConsumerGroup,SeasonId,BrandId,Price,Quantity,ImageFile,Description")] Sock sock, IFormFile imageFile)
        {
            _logger.LogInformation("--async Task<IActionResult> Create: enter");
            PopulateDropDowns();

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogWarning($"Validation Error in Create: {error.ErrorMessage}");
                    }
                }
                PopulateDropDowns(sock);
                return View(sock);
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    sock.ImageFile = fileName;
                }

                _context.Add(sock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns();
            return View(sock);
        }
        // GET: Sock/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("--- async Task<IActionResult> Edit: enter");
            if (id == null)
            {
                return NotFound();
            }

            //var sock = await _context.Socks.FindAsync(id);
            var sock = await _context.Socks
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sock == null)
            {
                return NotFound();
            }
            PopulateDropDowns(sock);
            return View(sock);
        }

        // POST: Sock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,MaterialId,SockStyleId,OutdoorSportId,ConsumerGroup,SockSize,ConsumerGroup,SeasonId,BrandId,Price,Quantity,Description,ImageFile")] Sock sock,
            IFormFile imageFile = null)
        {
            _logger.LogInformation("--- Edit with Bind: enter");
            _logger.LogInformation($"--Expect ID:{id}, Actual ID: {sock.Id}");
            if (id != sock.Id)
            {

                return NotFound();
            }

            _logger.LogInformation($"ModelState: {ModelState}");
            _logger.LogInformation($"sock: {sock}");
            _logger.LogInformation($"ModelState.IsValid: {ModelState.IsValid}");

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogWarning($"Validation Error: {error.ErrorMessage}");
                    }
                }
                PopulateDropDowns(sock);
                return View(sock);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation($"--imageFile is: {imageFile}");
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        sock.ImageFile = fileName;
                    }
                    else
                    {
                        // Keep the original image if no new image is provided
                        var originalSock = await _context.Socks.AsNoTracking().FirstOrDefaultAsync(s => s.Id == sock.Id);
                        sock.ImageFile = originalSock?.ImageFile ?? sock.ImageFile;
                    }
                    _context.Update(sock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SockExists(sock.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = sock.Id });

                //return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns(sock);
            return View(sock);
        }

        // GET: Sock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Socks == null)
            {
                return NotFound();
            }

            var sock = await _context.Socks.FirstOrDefaultAsync(m => m.Id == id);
            if (sock == null)
            {
                return NotFound();
            }


            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == sock.MaterialId);
            var sockStyle = await _context.SockStyles.FirstOrDefaultAsync(s => s.Id == sock.SockStyleId);
            var outdoorSport = await _context.OutdoorSports.FirstOrDefaultAsync(o => o.Id == sock.OutdoorSportId);
            var season = await _context.Seasons.FirstOrDefaultAsync(s => s.Id == sock.SeasonId);
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == sock.BrandId);

            var viewModel = new SockDetailsViewModel
            {
                Id = sock.Id,
                MaterialName = material?.Name,
                SockStyleName = sockStyle?.Name,
                OutdoorSportName = outdoorSport?.Name,
                SockSize = sock.SockSize,
                ConsumerGroup = sock.ConsumerGroup,
                SeasonName = season?.Name,
                BrandName = brand?.Name,
                Price = sock.Price,
                Quantity = sock.Quantity,
                ImageFile = sock.ImageFile,
                Description = sock.Description
            };
            return View(viewModel);
        }

        // POST: Sock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("--- Task<IActionResult> DeleteConfirmed: enter");
            _logger.LogInformation($"--Expect ID:{id}");
            if (_context.Socks == null)
            {
                return Problem("Entity set 'GammaWearContext.Sock' is null.");
            }
            var sock = await _context.Socks.FindAsync(id);
            if (sock != null)
            {
                _context.Socks.Remove(sock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SockExists(int id)
        {
            //return (_context.Socks?.Any(e => e.Id == id)).GetValueOrDefault();
            return _context.Socks.Any(e => e.Id == id);

        }

        private void PopulateDropDowns(Sock sock = null)
        {
            ViewBag.Materials = new SelectList(_context.Materials, "Id", "Name", sock?.MaterialId);
            ViewBag.SockStyles = new SelectList(_context.SockStyles, "Id", "Name", sock?.SockStyleId);
            ViewBag.OutdoorSports = new SelectList(_context.OutdoorSports, "Id", "Name", sock?.OutdoorSportId);
            ViewBag.Seasons = new SelectList(_context.Seasons, "Id", "Name", sock?.SeasonId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", sock?.BrandId);
        }





    }

}
