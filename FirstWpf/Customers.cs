

namespace FirstWpf
{
    public class Customers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

     
        public string Fullinfo
        {
            get 
            {
                return $"{UserName} {Password}";
            }
           
        }

    }
}
