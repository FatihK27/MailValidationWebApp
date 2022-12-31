using CsvHelper;
using Huawei.WebUIMailValidate.Models;
using Huawei.WebUIMailValidate.Services;
using Huawei.WebUIMailValidate.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Huawei.WebUIMailValidate.Controllers
{
    [Authorize]
    public class MailValidationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private IWebHostEnvironment Environment;
        public MailValidationController(IWebHostEnvironment _environment, AppDbContext context, UserManager<User> userManager, RabbitMQPublisher rabbitMQPublisher)
        {
            Environment = _environment;
            _context = context;
            _userManager = userManager;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        public async Task<IActionResult> ValidationJobs()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(await _context.Validation.Where(x => x.userID == user.Id).OrderByDescending(x => x.RequestDate).ToListAsync());
        }

        public IActionResult UploadCsv()
        {
            return View();
        }

        public IActionResult SingleValidation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSingleValidation(string mailAddress)
        {
            try
            {
                TrueMailService validator = new TrueMailService(mailAddress);
                string result = "";
                bool status = false;
                var vresult = validator.GetValidationResult();
                if (vresult.description != null && vresult.description != "")
                {
                    result = "<strong>" + vresult.mailAddress + "</strong> Mail adresi doğrulaması tamamlandı.<br><strong>Sonuç:</strong> <span class='text-danger'>" + vresult.result + "</span><br>Açıklama: <span class='text-danger'>" + vresult.description + "</span>";
                }
                else
                {
                    result = "<strong>" + vresult.mailAddress + "</strong> Mail adresi doğrulaması tamamlandı.<br><strong>Sonuç:</strong> <span class='text-success'>" + vresult.result + "</span>";
                }
                return Json(new { Message = result, IsSuccessful = true, Status = vresult.status });
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, IsSuccessful = false });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadCsvFile(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    if (file is not { Length: > 0 }) return BadRequest();
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);

                    //using (StreamReader csvReader = new StreamReader(file.InputStream))
                    //{
                    //    while (!csvReader.EndOfStream)
                    //    {
                    //        var line = csvReader.ReadLine();
                    //        var values = line.Split(';');
                    //    }
                    //}



                    //string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                    //if (!Directory.Exists(path))
                    //{
                    //    Directory.CreateDirectory(path);
                    //}
                    Guid batchId = Guid.NewGuid();
                    //string fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + batchId.ToString() + ".csv";
                    //string filePath = Path.Combine(path, fileName);
                    //using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    file.CopyTo(stream);
                    //}
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<CsvRecord>().ToList();
                        foreach (var record in records)
                        {
                            _rabbitMQPublisher.Publish(new Validation()
                            {
                                userID = user.Id,
                                mailAddress = record.mailAddress,
                                BatchId = batchId,
                                RequestDate = DateTime.Now.ToUniversalTime(),
                            });
                        }
                    }
                }
                TempData["FileUpload"] = true;
            }
            catch (Exception ex)
            {

                //Console.WriteLine(ex.ToString());
            }


            //return View("Index");
            return View("UploadCsv");
        }
    }
}
