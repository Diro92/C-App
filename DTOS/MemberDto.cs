using System.Collections.Generic;


namespace Task.Api.DTOS

{

    public class MemberDto

    {

        public int ID { get; set; }

        public string  Name { get; set; }

        public byte[] PasswordHash {get;set;}
        
        public byte[] PasswordSalt {get;set;}


        public ICollection<TareaDTO> Tareas {get;set;}

        


     }

    
}