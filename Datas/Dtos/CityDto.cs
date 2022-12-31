using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Datas.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Name is mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage ="Only numerics are not allowed")]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}


//At present we are Exposing database entities to the client, but that is not always a good idea. Sometimes you want to change the shape of the data that you send to client. For example, you might want to:

//-Remove circular references(see previous section).
//-Hide particular properties that clients are not supposed to view.
//- Omit some properties in order to reduce payload size.
//- Flatten object graphs that contain nested objects, to make them more convenient for clients.
//- Avoid "over-posting" vulnerabilities. (See Model Validation for a discussion of over-posting.)
//-Decouple your service layer from your database layer.