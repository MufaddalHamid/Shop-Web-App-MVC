using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Tool_Shop_Web_App.Models;
using Tool_Shop_Web_App.UserMode;

namespace Shop_Web_App.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult SetClientDetails (CartDM cart , CartItemDM CartItem)
        {
            string CurrentCartId = "";
            try
            {

                using (UserServices objUserService = new UserServices())
                {
                    string AddNewCartId = objUserService.AddCart(cart);
                    CurrentCartId = objUserService.AddItemToCart(CartItem.ItemId.ToString(),CartItem.Qty, CartItem.Price,CartItem.CartId.ToString());
                    if (CurrentCartId =="" || CurrentCartId == null)
                    {
                        throw new Exception();
                    }
                }

            }
            catch
            {
               return Json(new { status = "Could not add Item To Cart"});

            }
            return Json(new { status = "Added Item To Cart", CartId = CurrentCartId });
        }

        public ActionResult AddItemToCart (string ItemId,int Qty , decimal Price,string CartId)
        {
            try
            {
                using (UserServices objUserService = new UserServices())
                {
                   CartId = objUserService.AddItemToCart(ItemId, Qty , Price, CartId);
                    if (CartId == null) 
                    {
                        throw new Exception();
                    }
                }

            }
            catch
            {
                return Json(new { status = "Could Not add Item To Cart", CartId = CartId });

            }
            return Json(new { status = "Added Item To Cart" , CartId = CartId });
        }

        //[HttpPost]
        //public ActionResult PlaceOrder (string CartId)
        //{
        //    try
        //    {
        //        using (UserServices objUsrServices = new UserServices()) 
        //        { 
        //            string OrderCode = objUsrServices.SaveCartOrder(CartId);
                
        //        }
        //    }
        //    catch (Exception ex) { }


        //    return Json(new { status = "Saved SuccessFully" + "order Numer" });

        //}
    }
}
