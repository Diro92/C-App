
using System.Collections.Generic;

namespace Task.Api.models

{

    public class User
    
    {   
        public int ID { get; set; }

        public string  Name { get; set; }

        public byte[] PasswordHash {get;set;}
        
        public byte[] PasswordSalt {get;set;}

        public ICollection<Tarea> Tareas {get;set;}
      
    }
}