using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    public class ToDoController : Controller
    {
        private readonly AppDbContext _context;

        public ToDoController(AppDbContext context)
        {
            _context = context;
        }

        // Lista todas as tarefas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDoItems.ToListAsync());
        }

        // Formulário para criar nova tarefa
        public IActionResult Create()
        {
            return View();
        }

        // Criação de uma nova tarefa (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ToDoItem toDoItem)
        {
            
            _context.Add(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            // return Ok(new {message="debug", item=toDoItem});
        }

        // Formulário para edição
        public async Task<IActionResult> Edit(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        // Edita uma tarefa (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            _context.Update(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // Exclui uma tarefa
        public async Task<IActionResult> Delete(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem != null)
            {
                _context.ToDoItems.Remove(toDoItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
