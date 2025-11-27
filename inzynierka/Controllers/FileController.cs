using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO.Compression;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace inzynierka.Controllers
{
    public class FileController : Controller
    {

        public IActionResult File()
        {
            using CloudContext cloudContext = new CloudContext();

            //show file
            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");
            
            if (sessionIdUser == null)
            {
                TempData["InformMessage"] = "Zaloguj się ponownie";
                return RedirectToAction("Login", "Account");
            }
            var files = cloudContext.Files.Where(f => f.UserId.ToString() == sessionIdUser).ToList();

            return View(files);
        }

        public IActionResult FileAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFileAdd(FileViewModel files)
        {
            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");
            

            if (sessionIdUser == null)
            {
                TempData["InformMessage"] = "Zaloguj się ponownie";
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await files.formFile.CopyToAsync(memoryStream);
                    var filesize = files.formFile.Length;
                    //upload
                    var currentDateTime = DateTime.Now;
                    var formattedDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                    if (memoryStream.Length < 2000000)
                    {
                        int sessionIdUserToInt = int.Parse(sessionIdUser);

                        var filemodel = new FileModel
                        {
                            FileName = files.Name,
                            Date_add = formattedDateTime,
                            Size = filesize,
                            FileData = memoryStream.ToArray(),
                            UserFileNumber = GetFileNumber(sessionIdUserToInt),
                            UserId = sessionIdUserToInt
                        };

                        using CloudContext cloudContext = new CloudContext();

                        try
                        {
                            cloudContext.Add(filemodel);
                            cloudContext.SaveChanges();
                            TempData["SuccessMessage"] = "Poprawnie wyslano plik!";
                            return RedirectToAction("GoToFile", "Home");
                        }
                        catch (Exception ex)
                        {
                            TempData["DangerMessage"] = "Cos poszlo nie tak";
                            return View("FileAdd");
                        }
                    }
                    else
                    {
                        TempData["DangerMessage"] = "Plik moze miec maksymalnie 2mb";
                        return View("FileAdd");
                    }
                }
            }
            else
            {
                return View("FileAdd");
            }


        }

        // GET: /Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("GoToFile", "Home");
            }

            using CloudContext cloudContext = new CloudContext();

            var existingfile = cloudContext.Files.FirstOrDefault(u => u.FileID == id);


            return View(existingfile);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using CloudContext cloudContext = new CloudContext();
            if (cloudContext.Files == null)
            {
                return Problem("Entity set 'CloudContext.Files'  is null.");
            }

            var fileModel = await cloudContext.Files.FindAsync(id);
            if (fileModel != null)
            {
                cloudContext.Files.Remove(fileModel);
            }

            await cloudContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Usunieto plik!";
            return RedirectToAction("GoToFile", "Home");
        }


        public async Task<IActionResult> Download(int? id)
        {
            using CloudContext cloudContext = new CloudContext();


            var fileToRetrieve = cloudContext.Files.FirstOrDefault(f => f.FileID == id);
            var file = fileToRetrieve.FileData;

            return File(file, "text/plain", fileToRetrieve.FileName);

        }




        public int GetFileNumber(int userId)
        {
            using CloudContext cloudContext = new CloudContext();

            return cloudContext.Files.Count(f => f.UserId == userId) + 1;


        }

    }
}
