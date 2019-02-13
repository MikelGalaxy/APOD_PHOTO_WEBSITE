using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace NASA_API_PHOTOSHOTS.Controllers
{
    public class NasaPicturesController : Controller
    {
        private readonly IMapper _mapper;

        public NasaPicturesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "About site";

            return View();
        }
    }
}