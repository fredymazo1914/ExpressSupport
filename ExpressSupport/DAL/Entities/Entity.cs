namespace ExpressSupport.DAL.Entities
{
    public class Entity
    {

        public Guid Id { get; set; }//PK

        public DateTime? CreateDate { get; set; }//? = campo Nulleable, es decir que se puden guardar nulls

        public DateTime? ModifiedDate { get; set; }//? = campo Nulleable, es decir que se puden guardar nulls
    }
}
