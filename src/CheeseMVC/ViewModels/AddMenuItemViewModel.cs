﻿using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int menuID { get; set; }
        public int cheeseID { get; set; }

        public Menu Menu { get; set; }

        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel()
        {


        }      


        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheese)
        {
            Menu = menu;
            menuID = menu.ID;
            cheeseID = cheese.ToList<Cheese>()[0].ID;
            Cheeses = new List<SelectListItem>();
            for (int i = 0; i < cheese.ToList<Cheese>().Count; i++)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ToList<Cheese>()[i].ID.ToString(),
                    Text = cheese.ToList<Cheese>()[i].Name.ToString()
                });
            }
        }

    }
}
