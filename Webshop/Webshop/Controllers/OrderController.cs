﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Context;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly WebshopContext context;      
        
        public OrderViewModel orderviewmodel = new OrderViewModel();
        private UserManager<User> UserMgr { get; }
       private DatabaseCRUD databaseCRUD { get; }

        public OrderController(WebshopContext context, UserManager<User> userManager)
        {
            this.context = context;          
            this.UserMgr = userManager;
            this.databaseCRUD = new DatabaseCRUD(context);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                if (HttpContext.Session.GetString("CustomerCartSessionId") != null)
                {
                    var cartid = Guid.Parse(HttpContext.Session.GetString("CustomerCartSessionId"));
                    orderviewmodel.shoppinglist = context.ShoppingCart.Where(x => x.CartId == cartid).ToList();
                 
                    orderviewmodel.paymentMethodlist = context.PaymentMethods.ToList();

                    //dont use this here
                    User user = await UserMgr.GetUserAsync(HttpContext.User);

                    if(user.StreetAddress==null)
                    {
                        TempData["Address Null"] = "You haven't updated your Address book";
                    }
                    


                    // get the user id from login??
                    //get User address from database
                }
                else
                {
                    //shoppingcart is empty
                }

            }
            else
            {
                TempData["LoginNeeded"] = "You need to Login,before continue shopping...";
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View(orderviewmodel);
        }


        [HttpPost]
        public async Task<IActionResult> Index([Bind]OrderViewModel model)
        {
            User user = await UserMgr.GetUserAsync(HttpContext.User);
           

            //user id is not in Order table
            Order order = new Order()
            {
                UserId=user.Id,
                PaymentMethodId=model.PaymentMethodId,
                StatusId=1
                
            };

            var result = await databaseCRUD.InsertAsync<Order>(order);
            if(result>0)
            {
                TempData["OrderCreated"] = "Your order successfully created";
            }
            return RedirectToAction("AllProducts","Product");
        }

    }
}