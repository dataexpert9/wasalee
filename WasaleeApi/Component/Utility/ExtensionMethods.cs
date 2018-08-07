using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Component.Utility
{
    public static class ExtensionMethods
    {
        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        public static string ImageValidation(this IFormFile File)
        {
            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
            string ext = Path.GetExtension(File.FileName).ToLower();

            if (!AllowedFileExtensions.Contains(ext))
                return "Please Upload image of type .jpg,.gif,.png.";
            else if (File.Length > Global.ImageMaxSize)
                return "Please Upload a file upto";
            else
                return "";
        }

        public static string SaveImage(this IFormFile file, string FolderName)
        {
            #region ImageSaving

            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var uniqueName = Guid.NewGuid().ToString() + ext;
            var ReturnUrl = "FileDirectory\\"+FolderName+"\\";
            //var currentPath = HttpContext.Current.Server.MapPath("~\\api\\ImageDirectory") + "\\" + FolderName + "\\";
            var FileDirectory = Directory.GetCurrentDirectory();

            string folderPath = Path.Combine(FileDirectory,ReturnUrl);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            folderPath = folderPath + uniqueName;
            ReturnUrl = ReturnUrl + uniqueName;
            using (var fileStream = new FileStream(folderPath, FileMode.Create))
                file.CopyToAsync(fileStream);

            return ReturnUrl;
            #endregion

        }

    }
}
