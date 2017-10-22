using CookBookApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum DifficultyLevel
    {
        [Description("Very Easy")]
        VeryEasy,
        Easy,
        Medium,
        Hard,
        [Description("Very Hard")]
        VeryHard
    }

    public class Recipe
    {
        public Recipe() { this.Ingredients = new List<Product>(); }

        public string Name { get; set; }
        public List<Product> Ingredients { get; set; }
        public int PrepTime { get; set; }
        public DifficultyLevel? DiffLevel { get; set; }
        public int Servings { get; set; }
        public string Instruction { get; set; }
        public string AdditionalNotes { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        
       
       
    }
}
