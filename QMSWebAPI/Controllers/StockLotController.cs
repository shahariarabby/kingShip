using QMSWebAPI.DAL;
using QMSWebAPI.Models.StockLot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QMSWebAPI.Controllers
{
    [RoutePrefix("api/StockLot")]
    public class StockLotController : ApiController
    {

        StockLotDAL stockLot = new StockLotDAL();


        [HttpGet]
        public IHttpActionResult GetAllProductType(string Gender)
        {

            return Json(stockLot.GetAllProductType(Gender));
        }

        [HttpGet]
        [Route("GetAllProductStyle")]
        public IHttpActionResult GetAllProductStyle(string Gender, string ProductType)
        {

            return Json(stockLot.GetAllProductStyle(Gender, ProductType));
        }




        [HttpGet]
        [Route("GetAllProduct")]
        public IHttpActionResult GetAllProduct(string ProductType,string Gender)
        {

            return Json(stockLot.GetAllProduct(ProductType,Gender));
        }

        [HttpGet]
        [Route("GetAllColorByStyleName")]
        public IHttpActionResult GetAllColorByStyleName(string styleName)
        {

            return Json(stockLot.GetAllColorByStyleName(styleName));
        }

        [HttpGet]
        [Route("GetAllSizeByStyleName")]
        public IHttpActionResult GetAllSizeByStyleName(string styleName)
        {
            return Json(stockLot.GetAllSizeByStyleName(styleName));
        }

        [HttpPost]
        [Route("SaveOrder")]
        public IHttpActionResult SaveOrder(Orders orders)
        {
            try
            {
                return Json(stockLot.SaveOrder(orders));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpGet]
        [Route("GetAllOrderByUser")]
        public IHttpActionResult GetAllOrderByUser(string userId)
        {
            return Json(stockLot.GetAllOrderByUser(userId));
        }

        [HttpGet]
        [Route("GetOrderDetailsByOrderId")]
        public IHttpActionResult GetOrderDetailsByOrderId(string OrderId)
        {
            return Json(stockLot.GetOrderDetailsByOrderId(OrderId));
        }

        [HttpGet]
        [Route("GetAllPurchaseLimit")]
        public IHttpActionResult GetAllPurchaseLimit()
        {
            return Json(stockLot.GetAllPurchaseLimit());
        }

        #region for admin 

        [HttpGet]
        [Route("GetAllOrderByUserAndStatus")]
        public IHttpActionResult GetAllOrderByUserAndStatus(string userId, string status)
        {
            return Json(stockLot.GetAllOrderByUserAndStatus(userId,status));
        }

        [HttpPost]
        [Route("UpdateOrderStatus")]
        public IHttpActionResult UpdateOrderStatus(PurchesOrderMaster purchesOrderMaster)
        {
            try
            {
                return Json(stockLot.UpdateOrderStatus(purchesOrderMaster));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }


        #endregion



    }
}
