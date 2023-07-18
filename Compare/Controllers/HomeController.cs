using Compare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Compare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string leftTextarea, string rightTextarea)
        {
			if (string.IsNullOrEmpty(leftTextarea))
			{
				TempData["msg"] = "No text to compare makes my life easy! Cheers ;-)";
			}
			else
			{
				StringWriter leftwriter = new StringWriter();
				StringWriter rightWriter = new StringWriter();
				HttpUtility.HtmlDecode(leftTextarea, leftwriter);
				HttpUtility.HtmlDecode(rightTextarea, rightWriter);
				leftTextarea = leftwriter.ToString();
				rightTextarea = rightWriter.ToString();
				var leftArr = leftTextarea.Split(" ");
				var rightArr = rightTextarea.Split(" ");

				int count = leftArr.Length > rightArr.Length ? leftArr.Length : rightArr.Length;
				string finalLeft = string.Empty;
				string finalRight = string.Empty;
				for (int i = 0; i < count; i++)
				{
					if (leftArr[i] != null)
					{
						finalLeft += CompareWord(leftArr[i], rightArr[i], true);
					}
					if (rightArr[i] != null)
					{
						finalRight += CompareWord(leftArr[i], rightArr[i], false);
					}
				}
				SectionModel sm = new SectionModel();
				sm.leftSection = finalLeft; sm.rightSection = finalRight;

				return View("Index", sm);
			}			
			return View("Index");
        }
		
		public string CompareWord(string leftArr, string rightArr,bool forLeft)
		{
			var final = string.Empty;
			if (string.Compare(leftArr, rightArr, StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (forLeft)
				{
					final += leftArr + " ";
				}
				else
				{
					final += rightArr + " ";
				}
			}
			else
			{
				if (forLeft)
				{
					final += "<font style=\"background-color:#FFA50096\">" + leftArr + "</font> ";
				}
				else
				{
					final += "<font style=\"background-color:#FFA50096\">" + rightArr + "</font> ";
				}
			}
			return final;
		}
    }
}