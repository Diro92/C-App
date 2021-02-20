using System.Collections.Generic;


namespace Task.Api.DTOS

{

    public class MemberDto

    {

        public int ID { get; set; }

        public string  Name { get; set; }

  


        public ICollection<TareaDTO> Tareas {get;set;}

        


     }

    
}