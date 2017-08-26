using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {


        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Menu> menus = context.Menus.ToList();

            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();
            Menu newMenu = context.Menus.Single(m => m.ID == id);
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(newMenu, cheeses);
            return View(addMenuItemViewModel);
        }
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            int cheeseID = addMenuItemViewModel.cheeseID;
            int menuID = addMenuItemViewModel.menuID;
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
            .Where(cm => cm.CheeseID == cheeseID)
            .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.ToList<CheeseMenu>().Count == 0)
                {

                    existingItems = context.CheeseMenus.Where(cm => cm.CheeseID > 0).ToList();

                    int ListSize = existingItems.Count();

                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        MenuID = addMenuItemViewModel.menuID,
                        CheeseID = ListSize + 1

                        //Type = addCheeseViewModel.Type
                    };


                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();

                    return Redirect("/Menu/ViewMenu/" + menuID);
                }

               

            }

            return Redirect("/Menu");

        }
        [HttpGet]
        public IActionResult ViewMenu(int ID)
        {
            int id = ID;

            Menu newMenu = context.Menus.Single(m => m.ID == id);

            List<CheeseMenu> Items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel((IList<CheeseMenu>)Items);
            viewMenuViewModel.Menu = newMenu;
            return View(viewMenuViewModel);
        }


        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                IList<Menu> menus = context.Menus.ToList();

                Menu newMenus = new Menu
                {
                    Name = addMenuViewModel.Name,



                    //Type = addCheeseViewModel.Type
                };


                context.Menus.Add(newMenus);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenus.ID);

            
        }
            return View(addMenuViewModel);
    }

    }


}
