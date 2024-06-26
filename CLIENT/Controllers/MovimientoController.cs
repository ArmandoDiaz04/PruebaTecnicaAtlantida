using CLIENT.Models.ViewModels;
using CLIENT.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace CLIENT.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly IServiceMovimientosTarjeta _serviceMovimientos;

        public MovimientoController(IServiceMovimientosTarjeta serviceMovimientos)
        {
            _serviceMovimientos = serviceMovimientos;
        }

        [HttpGet]
        public async Task<IActionResult> Listado(int id, string numeroTarjeta, int mes, int anio)
        {
            int tipo = 1;
            List<MovimientosTarjetaVM> movimientos = await _serviceMovimientos.ObtenerMovimientosTarjeta(id, numeroTarjeta, tipo, mes, anio);
            // Ordenar los movimientos por fecha de forma descendente (la más reciente primero)
            movimientos = movimientos.OrderByDescending(m => m.fechaMovimiento).ToList();
            return View(movimientos);
        }

        [HttpGet]
        public IActionResult Movimiento(int idTarjeta, int tipoMovimiento)
        {
            ViewBag.tipoMovimiento = tipoMovimiento;
            ViewBag.idTarjeta = idTarjeta;
            MovimientosTarjetaVM movimiento = new();
            movimiento.idTarjeta = idTarjeta;
            movimiento.tipoMovimiento = tipoMovimiento.ToString();
            movimiento.monto = 0;
            return View(movimiento);
        }

        [HttpPost]
        public async Task<IActionResult> Movimiento(MovimientosTarjetaVM movimiento)
        {

            if (!ModelState.IsValid)
            {
                return PartialView(movimiento);
            }
            try
            {

                if (movimiento.tipoMovimiento == "2")
                {
                    movimiento.monto = movimiento.monto * -1;
                }
                movimiento.fechaMovimiento = System.DateTime.Now;
                await _serviceMovimientos.GuardarMovimiento(movimiento);
                return Json(new { estado = "Todo bien" });
            }
            catch (Exception)
            {

                return Json(new { estado = "Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetExcelCompras(int id, string numeroTarjeta, int mes, int anio)
        {
            List<MovimientosTarjetaVM> movimientos = await _serviceMovimientos.ObtenerMovimientosTarjeta(id, numeroTarjeta, 1, mes, anio);

            using (var package = new ExcelPackage())
            {
                var worksheet1 = package.Workbook.Worksheets.Add("Compras");
                worksheet1.Cells["A1"].Value = "Fecha de Compra";
                worksheet1.Cells["B1"].Value = "Descripción";
                worksheet1.Cells["C1"].Value = "Monto";

                int row = 2;
                foreach (var movimiento in movimientos)
                {
                    worksheet1.Cells[$"A{row}"].Value = movimiento.fechaMovimiento.ToLongDateString();
                    worksheet1.Cells[$"B{row}"].Value = movimiento.descripcion;
                    worksheet1.Cells[$"C{row}"].Value = movimiento.monto.ToString("C");
                    row++;
                }
                worksheet1.Columns.AutoFit();
                worksheet1.Cells["A1:C1"].Style.Font.Bold = true;

                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Set the stream position to the beginning
                stream.Position = 0;

                // Return the stream as a FileResult
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ComprasTarjeta" + numeroTarjeta + DateTime.Now + ".xlsx");
            }
        }
    }
}
