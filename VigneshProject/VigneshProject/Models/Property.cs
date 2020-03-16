using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VigneshProject.Models
{
    public class Property
    {
        [Key]
        public int PropID { get; set; }
        public string PropName { get; set; }
        public string Location { get; set; }
        public string PropOwnerName { get; set; }
        public string PropDescription { get; set; }
        public string PropType { get; set; }
        public long PhoneNo { get; set; }
        public string PropImage { get; set; }
        public string UserID { get; set; }

        public PropertyType propertyType { get; set; }
        

    }
    public class PropertyType
    {
        [Key]
        public int TypeID { get; set; }
        public string PropType { get; set; }

        public List<Property> properties { get; set; }
    }

}