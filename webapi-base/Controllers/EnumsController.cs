using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Attributes;
using System.ComponentModel;
using System.Reflection;
using webapi_base.Enums;

namespace webapi_base.Controllers
{
    [Route("api/enums")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Array result = Enum.GetValues(typeof(Category));
            return Ok(result);
        }
        [HttpGet("generic")]
        public IActionResult GetGeneric()
        {
            Category[] result = Enum.GetValues<Category>();
            return Ok(result);
        }
        [HttpGet("names")]
        public IActionResult GetNames()
        {
            var categories = Enum.GetValues(typeof(Category));
            var categoryNames =new List<string>();
            foreach (var category in categories)
            {
                categoryNames.Add(Enum.GetName(category.GetType(),category)??"");
            }
            return Ok(categoryNames);
        }

        [HttpGet("dictionary")]
        public IActionResult GetDictionary()
        {
            var categories = Enum.GetValues(typeof(Category));
            var categoryNames =new Dictionary<string, int>();
            foreach (var category in categories)
            {
                //var name = category.GetType().GetMember(nameof(category)).FirstOrDefault()?.Name??"";
                categoryNames.Add(Enum.GetName(category.GetType(), category)??"NoFound", (int)category);
            }
            return Ok(categoryNames);
        }


        [HttpGet("full")]
        public IActionResult GetCategoryFullInfo()
        {
            var categories = Enum.GetValues(typeof(Category));
            var categoryFullInfo = new List<object>();
            foreach (var category in categories)
            {
                var x = category.GetType().GetMember(category.ToString() ?? "");
                //var name = category.GetType().GetMember(nameof(category)).FirstOrDefault()?.Name??"";
                categoryFullInfo.Add(
                        new
                        {
                            Name= Enum.GetName(category.GetType(), category) ?? "NoFound",
                            Value=(int)category,
                            DisplayName= category.GetType().GetMember(category.ToString()??"").FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()?.Name,
                            Description =category.GetType().GetMember(category.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description??"",
                        }
                    );
                
            }
            return Ok(categoryFullInfo);
        }
    }
}
