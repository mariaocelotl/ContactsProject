using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZContactos.Data;
using ZContactos.Models;

namespace ZContactos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        //Vista index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        [HttpGet]
        public async Task<FileResult> ExportarPersonasAExcel()
        {
            var personas = await _context.Usuario.ToListAsync();
            var nombreArchivo = $"Contactos.xlsx";
            return GenerarExcel(nombreArchivo, personas);
        }

        private FileResult GenerarExcel(string archivo, IEnumerable<Usuario> usuarios)
        {
            DataTable dataTable = new DataTable("Usuarios");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("id"),
                new DataColumn("name"),
                new DataColumn("apellidoPaterno"),
                new DataColumn("apellidoMaterno"),
                new DataColumn("telefono"),
                new DataColumn("celular"),
                new DataColumn("correo"),

            });
            foreach (var usuario in usuarios)
            {
                dataTable.Rows.Add(usuario.id,
                    usuario.nombre,
                    usuario.apellidoPaterno,
                    usuario.apellidoMaterno,
                    usuario.telefono,
                    usuario.celular,
                    usuario.correo);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using(MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        archivo);
                }
            }
                
        }

        [HttpGet]
        public IActionResult Create()
        {
        return View();
        }

        //Retorna la vista
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Aqui instanciamos el modelo Usuario
        public  async Task <IActionResult> Create(Usuario usuario)
        {
            //Comprueba si se estan ingresando los datos y validando
            if (ModelState.IsValid)
            {
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        //Peticion que busca el id y trae el registro
        public IActionResult Edit(int? id)
        {
            //Verifica si el id no es nulo
            if(id == null)
            {
                return NotFound();
            }
            var usuario = _context.Usuario.Find(id);
            if(usuario == null)
            {
                return NotFound();
            }
            //aqui regresa el registro del usuario encontrado por el id o nombre
            return View(usuario);
        }

        //Retorna la vista
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Aqui instanciamos el modelo Usuario
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            //Comprueba si se estan ingresando los datos y validando
            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //Retorna la vista del usuario por si se edito mal
            return View(usuario);
        }

        [HttpGet]
        //Peticion que busca el id y trae el registro
        public IActionResult Details(int? id)
        {
            //Verifica si el id no es nulo
            if (id == null)
            {
                return NotFound();
            }
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            //aqui regresa el registro del usuario encontrado por el id o nombre
            return View(usuario);
        }

        [HttpGet]
        //Peticion que busca el id y trae el registro
        public IActionResult Delete(int? id)
        {
            //Verifica si el id no es nulo
            if (id == null)
            {
                return NotFound();
            }
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            //aqui regresa el registro del usuario encontrado por el id o nombre
            return View(usuario);
        }


        //Retorna la vista
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        //Aqui instanciamos el modelo Usuario
        public async Task<IActionResult> DeleteRegister(int? id)
        {

            var usuario = await _context.Usuario.FindAsync(id);
            if(usuario == null)
            {
                return View();
            }

            //Elimina el usuario que esta recibiendo
                _context.Usuario.Remove(usuario);
            //Guarda cambios
                await _context.SaveChangesAsync();
            //Regresa al index
                return RedirectToAction(nameof(Index));
         
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
