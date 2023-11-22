using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Attributes;
using MyServices.Tools.EnumHelper;
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

        [HttpGet("by-extension")]
        public IActionResult GetCategoriesExtension()
        {
            var hasDescription = Category.Personnel.HasDescription();
            var dataDescription = Category.Personnel.ToDescription();
            Console.WriteLine($"hasDescription: {hasDescription}");
            Console.WriteLine($"dataDescription: {dataDescription}");


            var dataText = Category.Personnel.ToString();
            Console.WriteLine($"dataText: {dataText}");

            var dataValue = Category.Personnel.ToValue();
            Console.WriteLine($"dataValue: {dataValue}");


            var hasDIsplayName = Category.Personnel.HasDisplayName();
            var dataDiaplayName = Category.Personnel.ToDisplayName();
            Console.WriteLine($"hasDIsplayName: {hasDIsplayName}");
            Console.WriteLine($"dataDiaplayName: {dataDiaplayName}");


            var listText = Enum.GetValues(typeof(Category)).Cast<Category>().Select(d => d.ToString()).ToList();
            var listValues = Enum.GetValues(typeof(Category)).Cast<int>().ToList();
            var dictionary_with_value_and_description = Enum.GetValues(typeof(Category))
                            .Cast<Category>()
                            .ToDictionary(data => data.ToValue(), data => data.ToDescription());

            var dictionary_with_value_and_display_name = Enum.GetValues(typeof(Category))
                            .Cast<Category>()
                            .ToDictionary(data => data.ToValue(), data => data.ToDisplayName());


            return Ok(new
            {
                dataText,
                dataValue,
                hasDIsplayName,
                dataDiaplayName,
                listText,
                listValues,
                dictionary_with_value_and_description,
                dictionary_with_value_and_display_name
            });
        }
    }
}
