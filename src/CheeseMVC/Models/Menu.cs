using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Menu
    {
        public string Name { get; set; }




        public int ID { get; set; }

        public IList<CheeseMenu> CheeseMenus;

        //public Menu()
        //{
        //    ID = 0;
        //    Name = "";
            //CheeseMenus = new IList<CheeseMenu>();
       // }

        public string Description()
        {
            return "Description"; //"ID: " + ID.ToString() + "# of CheeseMenus: " + CheeseMenus.Count.ToString();
        }
    }
    
 
}
