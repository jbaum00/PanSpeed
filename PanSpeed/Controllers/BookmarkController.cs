using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanSpeed.Data; // Passe den Namespace an, falls anders
using PanSpeed.Models; // Passe den Namespace an, falls anders
using System.Linq;
using System.Threading.Tasks;

namespace PanSpeed.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly PanSpeedContext _context;

        public BookmarkController(PanSpeedContext context)
        {
            _context = context;
        }

        // GET: Bookmark
        public async Task<IActionResult> Index()
        {
            var bookmarks = await _context.Bookmarks.ToListAsync();
            return View(bookmarks);
        }

        // GET: Bookmark/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var bookmark = await _context.Bookmarks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bookmark == null)
                return NotFound();

            return View(bookmark);
        }

        // GET: Bookmark/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookmark/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,Category,CreatedAt")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                bookmark.CreatedAt = DateTime.Now; // Setzt das Erstellungsdatum
                _context.Add(bookmark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookmark);
        }

        // GET: Bookmark/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
                return NotFound();

            return View(bookmark);
        }

        // POST: Bookmark/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,Category,CreatedAt")] Bookmark bookmark)
        {
            if (id != bookmark.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookmark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookmarkExists(bookmark.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookmark);
        }

        // GET: Bookmark/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var bookmark = await _context.Bookmarks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bookmark == null)
                return NotFound();

            return View(bookmark);
        }

        // POST: Bookmark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookmarkExists(int id)
        {
            return _context.Bookmarks.Any(e => e.Id == id);
        }
    }
}
