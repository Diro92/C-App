using System.ComponentModel.DataAnnotations;

namespace Task.Api.DTOS

{

    public class Userforlogin

    {
            [Required]
            public string username {get;set;}

            
             [Required]
            public string Password {get;set;}

    }


}