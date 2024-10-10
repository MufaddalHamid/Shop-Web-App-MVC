using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Tool_Shop_Web_App.Models;
using Tool_Shop_Web_App.UserMode;

namespace Tool_Shop_Web_App.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItemListing()
        {
            DataTable Items = new DataTable();
            using (UserServices objUserServices = new UserServices())
            {
                Items = objUserServices.FetchItems();
            }
            return View(Items);
        }

        public ActionResult ItemForm(string id)
        {
            ItemDM objItemDM = new ItemDM();
            try
            {
                
                DataTable dtcategories = new DataTable();
                using (UserServices objuserServices = new UserServices())
                {
                    dtcategories = objuserServices.GetCategories();
                }
                List<DropDown> dropDowns = new List<DropDown>();
                foreach (DataRow dr in dtcategories.Rows)
                {
                    dropDowns.Add(new DropDown
                    {
                        Id = Guid.Parse(dr[0].ToString()), // Assuming the first column is the Id
                        Code = dr[1].ToString(),           // Assuming the second column is the Code
                        Name = dr[2].ToString()            // Assuming the third column is the Name
                    });
                }
                objItemDM.Categories = dropDowns;

                if (id != null || Guid.Parse(id) != Guid.Empty)
                {
                    DataTable dtItem = new UserServices().FetchRecord("ITEMS", id);
                    if (dtItem.Rows.Count == 1)
                    {
                        DataRow drItem = dtItem.Rows[0];
                        objItemDM.Id = Guid.Parse(drItem["Id"].ToString());
                        objItemDM.Code = drItem["Code"].ToString();
                        objItemDM.Name = drItem["Name"].ToString(); 
                        objItemDM.Price = decimal.TryParse(drItem["Price"].ToString(), out var price) ? price : 0;
                        objItemDM.Description = drItem["Description"].ToString();
                        objItemDM.IsFreezed = bool.TryParse(drItem["IsFreezed"].ToString(), out var isFreezed) && isFreezed;
                        objItemDM.Category = drItem["Category"].ToString();

                    }
                }
            }
            catch (Exception ex) { }
            return View(objItemDM);
        }

        [HttpPost]
        public ActionResult SaveItem(ItemDM save)
        {
            try
            {
                DataTable result = new DataTable();
                using (UserServices objuserServices = new UserServices())
                {
                    if (objuserServices.ItemExists(save.Code) && save.Id == Guid.Empty)
                    {
                        return Json(new { success = true, status = false, message = "Item not Saved as Item with code already Exists" });
                    }
                    result = objuserServices.SaveItem(save);
                }
                if (result.Rows.Count > 0)
                {
                    return Json(new { success = true, status = true, message = "Item Saved Succesfully!!", id = result.Rows[0][0].ToString() });
                }
                return Json(new { success = true, status = false, message = "Item not Saved" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, status = false, message = "Error Saving item " + ex.Message });
            }
        }

        public ActionResult DeleteItem(string id)
        {
            bool status = false;
            try
            {
                using (UserServices objuserServices = new UserServices())
                {
                    status = objuserServices.DeleteItem(id);
                    if (status)
                    {
                        return Json(new { success = true, status = true, message = "Item Deleted Succesfully!!" });
                    }
                    else
                    {
                        return Json(new { success = true, status = true, message = "Item Not Deleted!!" });
                    }
                }


            }
            catch (Exception ex)
            {
                return Json(new { success = false, status = false, message = "item was Not Deleted:" + ex.Message });
            }

        }

    }
}