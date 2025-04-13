using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;         // ✅ Make sure your Truck.cs is in Models folder
using TruckInfoApi.Data;           // ✅ This is your TruckContext namespace
using System.Linq;

namespace TruckInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly TruckContext _context;
        private readonly IConverter _converter;

        // ✅ Inject both context and converter
        public TruckController(TruckContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        [HttpPost("download-pdf")]
        public IActionResult DownloadPdf([FromBody] string number)
        {
            // ✅ Fetch truck from database
            var truck = _context.Trucks.FirstOrDefault(t => t.Number == number);
            if (truck == null)
                return NotFound("Truck not found");

            var html = $@"
                <h1>Truck Details</h1>
                <p><strong>Truck Number:</strong> {truck.Number}</p>
                <p><strong>Owner:</strong> {truck.Owner}</p>
                <p><strong>Capacity:</strong> {truck.Capacity}</p>
                <p><strong>Route:</strong> {truck.Route}</p>
            ";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = html
                    }
                }
            };

            var pdfBytes = _converter.Convert(doc);
            return File(pdfBytes, "application/pdf", $"{truck.Number}.pdf");
        }
    }
}
