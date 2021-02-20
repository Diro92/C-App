
namespace Task.Api.models

{

    public class Tarea
    
    {   
        public int ID { get; set; }

        public string  Name { get; set; }

        public string Tipo {get;set;} 

        public string Duracion {get;set;}
        
        public User User{get;set;}

        public int Userid {get;set;}  // Fully defining the entity 

    

    }



}