using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus =ResultStatus.Success
            });

        }

        [HttpGet]
        public  IActionResult Add()
        {
            return PartialView("_UserAddPartial");

        }

        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            // ~/img/user.Picture
            string wwwroot = _env.WebRootPath;
            // oguzhankutucu
            string userFileName = Path.GetFileNameWithoutExtension(userAddDto.Picture.FileName);
            // .png
            string fileExtension = Path.GetExtension(userAddDto.Picture.FileName);
            /* 
                OğuzhanKutucu_587_5_38_12_3_10_2020.png
                KaanKutucu_601_5_38_12_3_10_2022.png
            */
            DateTime dateTime = DateTime.Now;

            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}_{userFileName}{fileExtension}";

            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using(FileStream stream = new FileStream(path,FileMode.Create))
            {
                await userAddDto.Picture.CopyToAsync(stream);
            }

            return fileName;

        }


    }
}
