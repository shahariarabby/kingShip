using QMSWebAPI.Models;
using QMSWebAPI.Models.StockLot;
using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QMSWebAPI.DAL
{
    public class StockLotDAL
    {

        DataAccessManager accessManager = new DataAccessManager();

        public List<ProductType> GetAllProductType(string Gender)
        {
            try
            {

                ProductType reportObj = new ProductType();


                List<ProductType> typeList = new List<ProductType>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Gender", Gender));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllProductType", aParameters);
                while (dr.Read())
                {
                    ProductType ProductTypeModel = new ProductType();
                    // fixtureseModel.Id = (int)dr["Id"];
                    ProductTypeModel.ProductTypeName = dr["ProductType"].ToString();



                    typeList.Add(ProductTypeModel);


                }


                return typeList;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public List<Style> GetAllProductStyle(string Gender, string ProductType)
        {
            try
            {

                Style reportObj = new Style();


                List<Style> styleList = new List<Style>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Gender", Gender));
                aParameters.Add(new SqlParameter("@ProductType", ProductType));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllProductStyle", aParameters);
                while (dr.Read())
                {
                    Style ProductStyleModel = new Style();
                    // fixtureseModel.Id = (int)dr["Id"];
                    ProductStyleModel.StyleName = dr["StyleName"].ToString();



                    styleList.Add(ProductStyleModel);


                }


                return styleList;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }




        public BaseResultResponse GetAllProduct(string ProductType, string Gender)
        {
            try
            {

                List<Product> productList = new List<Product>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@ProductType", ProductType));
                aParameters.Add(new SqlParameter("@Gender", Gender));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllProduct", aParameters);
                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = (int)dr["ID"];
                    product.StyleName = dr["StyleName"].ToString();
                    product.Gender = dr["Gender"].ToString();
                    product.ProductType = dr["ProductType"].ToString();
                    //product.Color = dr["Color"].ToString();
                    //product.Size = dr["Size"].ToString();
                    product.Description = dr["Description"].ToString();
                    product.StockQuantity = (int)dr["StockQuantity"];
                    product.UnitPrice = (int)dr["UnitPrice"];
                    product.Image = dr["Image"].ToString();

                    //product.ImageUrl = dr["ImageUrl"].ToString();
                    product.ImageUrl = "http://10.12.13.163:8040/StockImages/" + product.Image;



                    productList.Add(product);
                }

                var baseResultResponse = baseResultResponseMethode(productList);

                return baseResultResponse;
                //return productList;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public BaseResultResponse GetAllColorByStyleName(string styleName)
        {
            try
            {

                List<Color> colors = new List<Color>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StyleName", styleName));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllColorByStyleName", aParameters);
                while (dr.Read())
                {
                    Color color = new Color();
                    color.ColorName = dr["Color"].ToString();

                    colors.Add(color);
                }
                var baseResultResponse = baseResultResponseMethode(colors);

                return baseResultResponse;
                //return colors;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public BaseResultResponse GetAllSizeByStyleName(string styleName)
        {
            try
            {

                List<Size> sizes = new List<Size>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StyleName", styleName));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllSizeByStyleName", aParameters);
                while (dr.Read())
                {
                    Size size = new Size();
                    size.SizeName = dr["Size"].ToString();

                    sizes.Add(size);
                }

                var baseResultResponse = baseResultResponseMethode(sizes);

                return baseResultResponse;
                //return sizes;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse SaveOrder(Orders Orders)
        {
            try
            {
                /// check PurchesLimit type and user wise
                ResultResponse result = new ResultResponse();
                var BraQuantity = 0;
                var BriefsQuantity = 0;
                var ActivewearQuantity = 0;
                var KnitwearQuantity = 0;



                foreach (var item in Orders.PurchesOrders)
                {
                    var stock = GetAllStockByStyleGenderTypeColorSize(item.StyleName, item.Gender, item.ProductType, item.Color, item.Size);

                    // vheck stock is avable or not
                    if (stock.StockQuantity == 0)
                    {
                        result.msg = stock.StyleName + "this style Stock is 0";
                        return result;
                    }

                    if ("Bra" == item.ProductType)
                    {
                        BraQuantity = GetPurchesOrderByTypeUser(item.ProductType, Orders.UserId);
                    }
                    else if ("Briefs" == item.ProductType)
                    {
                        BriefsQuantity = GetPurchesOrderByTypeUser(item.ProductType, Orders.UserId);
                    }
                    else if ("Activewear" == item.ProductType)
                    {
                        ActivewearQuantity = GetPurchesOrderByTypeUser(item.ProductType, Orders.UserId);
                    }
                    else if ("Knitwear" == item.ProductType)
                    {
                        KnitwearQuantity = GetPurchesOrderByTypeUser(item.ProductType, Orders.UserId);
                    }
                }
                //var BraQuantity = GetPurchesOrderByTypeUser(ProductTypeEnum.Bra.ToString(), Orders.UserId);
                //var BriefsQuantity = GetPurchesOrderByTypeUser(ProductTypeEnum.Briefs.ToString(),Orders.UserId);
                //var ActivewearQuantity = GetPurchesOrderByTypeUser(ProductTypeEnum.Activewear.ToString(),Orders.UserId);
                //var KnitwearQuantity = GetPurchesOrderByTypeUser(ProductTypeEnum.Knitwear.ToString(),Orders.UserId);



                if (BraQuantity == (int)ProductTypeEnum.Bra)
                {
                    result.msg = "Bra Order Quantity Limit Cross";
                    return result;
                }
                else if (BriefsQuantity == (int)ProductTypeEnum.Briefs)
                {
                    result.msg = "Briefs Order Quantity Limit Cross";
                    return result;
                }
                else if (ActivewearQuantity == (int)ProductTypeEnum.Activewear)
                {
                    result.msg = "Activewear Order Quantity Limit Cross";
                    return result;
                }
                else if (KnitwearQuantity == (int)ProductTypeEnum.Knitwear)
                {
                    result.msg = "Knitwear Order Quantity Limit Cross";
                    return result;
                }

                PurchesOrderMaster purchesOrderMaster = new PurchesOrderMaster();
                purchesOrderMaster.UserId = Orders.UserId;
                purchesOrderMaster.NetPrice = Orders.NetTotalPrice;
                var r = SavePurchesOrderMaster(purchesOrderMaster);
                //var masterOrder = GetAllOrderByUser(Orders.UserId).LastOrDefault();



                if (r.pk > 0)
                {
                    //DataValidation(purchesOrder);

                    foreach (var purchesOrder in Orders.PurchesOrders)
                    {
                        accessManager.SqlConnectionOpen(DataBase.StockLotDB);

                        List<SqlParameter> aParameters = new List<SqlParameter>();

                        aParameters.Add(new SqlParameter("@StyleName", purchesOrder.StyleName));
                        aParameters.Add(new SqlParameter("@Gender", purchesOrder.Gender));
                        aParameters.Add(new SqlParameter("@ProductType", purchesOrder.ProductType));
                        aParameters.Add(new SqlParameter("@Color", purchesOrder.Color));
                        aParameters.Add(new SqlParameter("@Size", purchesOrder.Size));
                        aParameters.Add(new SqlParameter("@Description", purchesOrder.Description));
                        aParameters.Add(new SqlParameter("@Quantity", purchesOrder.Quantity));
                        aParameters.Add(new SqlParameter("@UnitPrice", purchesOrder.UnitPrice));
                        //aParameters.Add(new SqlParameter("@OrderNumber", purchesOrder.OrderNumber));
                        aParameters.Add(new SqlParameter("@UserId", Orders.UserId));
                        //aParameters.Add(new SqlParameter("@Status", statusEnum.OrderPlaced));
                        aParameters.Add(new SqlParameter("@TotalPrice", purchesOrder.TotalPrice));
                        aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                        aParameters.Add(new SqlParameter("@PurchesOrderMasterId", r.pk));

                        result.isSuccess = accessManager.SaveData("sp_SaveOrder", aParameters);

                        accessManager.SqlConnectionClose();
                        // inventroy deduct
                        updateStock(purchesOrder);
                    }
                }

                result.msg = "Order Added Successfully";
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

        }
        public ResultResponse SavePurchesOrderMaster(PurchesOrderMaster purchesOrderMaster)
        {
            try
            {

                var u = "";
                var OrderNumber = "";
                var order = GetAllOrdersByUser(u).LastOrDefault();
                if (order == null)
                {
                    OrderNumber = "#SQ1";
                }
                else
                {
                    Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                    Match match = re.Match(order.OrderNumber);

                    string alphaPart = match.Groups[1].Value;
                    string numberPart = match.Groups[2].Value;

                    int num = Int32.Parse(numberPart);
                    var n = num + 1;
                    OrderNumber = "#SQ" + n;
                }

                accessManager.SqlConnectionOpen(DataBase.StockLotDB);
                ResultResponse result = new ResultResponse();
                //DataValidation(purchesOrder); 

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@OrderNumber", OrderNumber));
                aParameters.Add(new SqlParameter("@UserId", purchesOrderMaster.UserId));
                aParameters.Add(new SqlParameter("@Status", statusEnum.Ordered));
                aParameters.Add(new SqlParameter("@NetPrice", purchesOrderMaster.NetPrice));
                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));

                result.pk = accessManager.SaveDataReturnPrimaryKey("sp_SavePurchesOrderMaster", aParameters);

                if (result.pk > 0)
                {
                    result.msg = "Data Save Successfully";
                    result.isSuccess = true;
                }

                //result.msg = "Master Order Added Successfully";
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<PurchesOrderMasterVM> GetAllOrdersByUser(string userId)
        {
            try
            {

                List<PurchesOrderMasterVM> purchesOrderMasterVMs = new List<PurchesOrderMasterVM>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));


                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllOrderByUser", aParameters);
                while (dr.Read())
                {
                    PurchesOrderMasterVM purchesOrderMasterVM = new PurchesOrderMasterVM();
                    var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");
                    var status = dr["Status"].ToString();

                    purchesOrderMasterVM.Id = (int)dr["Id"];
                    purchesOrderMasterVM.OrderNumber = dr["OrderNumber"].ToString();
                    purchesOrderMasterVM.UserId = dr["UserId"].ToString();
                    purchesOrderMasterVM.Status = Convert.ToInt32(status);
                    purchesOrderMasterVM.Remarks = dr["Remarks"].ToString();
                    purchesOrderMasterVM.NetPrice = (decimal)dr["NetPrice"];
                    //purchesOrderMasterVM.CreateDateTime = (DateTime)dr["CreateDateTime"];

                    purchesOrderMasterVM.CreateDate = dateTime.Split(' ')[0];
                    purchesOrderMasterVM.CreateTime = dateTime.Split(' ')[1] + ' ' + dateTime.Split(' ')[2];

                    purchesOrderMasterVMs.Add(purchesOrderMasterVM);
                }                

                return purchesOrderMasterVMs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public BaseResultResponse GetAllOrderByUser(string userId)
        {
            //List < PurchesOrderMasterVM >
            try
            {

                List<PurchesOrderMasterVM> purchesOrderMasterVMs = new List<PurchesOrderMasterVM>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));


                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllOrderByUser", aParameters);
                while (dr.Read())
                {
                    PurchesOrderMasterVM purchesOrderMasterVM = new PurchesOrderMasterVM();
                    var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");
                    var status = dr["Status"].ToString();

                    purchesOrderMasterVM.Id = (int)dr["Id"];
                    purchesOrderMasterVM.OrderNumber = dr["OrderNumber"].ToString();
                    purchesOrderMasterVM.UserId = dr["UserId"].ToString();
                    purchesOrderMasterVM.Status = Convert.ToInt32(status);
                    purchesOrderMasterVM.NetPrice = (decimal)dr["NetPrice"];
                    //purchesOrderMasterVM.CreateDateTime = (DateTime)dr["CreateDateTime"];

                    purchesOrderMasterVM.CreateDate = dateTime.Split(' ')[0];
                    purchesOrderMasterVM.CreateTime = dateTime.Split(' ')[1] + ' ' + dateTime.Split(' ')[2];

                    purchesOrderMasterVMs.Add(purchesOrderMasterVM);
                }

                var baseResultResponse = baseResultResponseMethode(purchesOrderMasterVMs);

                return baseResultResponse;

                //return purchesOrderMasterVMs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public BaseResultResponse GetAllPurchaseLimit()
        {
            //List < PurchaseLimit >
            try
            {
                List<PurchaseLimit> purchaseLimits = new List<PurchaseLimit>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllPurchaseLimit", aParameters);
                while (dr.Read())
                {
                    PurchaseLimit purchaseLimit = new PurchaseLimit();
                    purchaseLimit.ProductType = dr["ProductType"].ToString();
                    purchaseLimit.UnitLimit = (int)dr["UnitLimit"];
                    purchaseLimit.UnitPrice = (int)dr["UnitPrice"];

                    purchaseLimits.Add(purchaseLimit);
                }

                var baseResultResponse = baseResultResponseMethode(purchaseLimits);

                return baseResultResponse;
                //return purchaseLimits;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse updateStock(PurchesOrder purchesOrder)
        {
            try
            {
                var stock = GetAllStockByStyleGenderTypeColorSize(purchesOrder.StyleName, purchesOrder.Gender, purchesOrder.ProductType, purchesOrder.Color, purchesOrder.Size);

                accessManager.SqlConnectionOpen(DataBase.StockLotDB);
                ResultResponse result = new ResultResponse();
                //DataValidation(product); 


                var quantity = stock.StockQuantity - purchesOrder.Quantity;

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@StyleName", purchesOrder.StyleName));
                aParameters.Add(new SqlParameter("@Gender", purchesOrder.Gender));
                aParameters.Add(new SqlParameter("@ProductType", purchesOrder.ProductType));
                aParameters.Add(new SqlParameter("@Color", purchesOrder.Color));
                aParameters.Add(new SqlParameter("@Size", purchesOrder.Size));
                aParameters.Add(new SqlParameter("@StockQuantity", quantity));
                aParameters.Add(new SqlParameter("@UpdateDate", DateTime.Now));

                result.isSuccess = accessManager.SaveData("sp_UpdateStock", aParameters);

                //if (result.pk > 0)
                //{
                //    result.msg = "Data Save Successfully";
                //    result.isSuccess = true;
                //}

                result.msg = "Data Update Successfully";
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public Product GetAllStockByStyleGenderTypeColorSize(string StyleName, string Gender, string ProductType, string Color, string Size)
        {
            try
            {
                //List<PurchaseLimit> purchaseLimits = new List<PurchaseLimit>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StyleName", StyleName));
                aParameters.Add(new SqlParameter("@Gender", Gender));
                aParameters.Add(new SqlParameter("@ProductType", ProductType));
                aParameters.Add(new SqlParameter("@Color", Color));
                aParameters.Add(new SqlParameter("@Size", Size));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllStockByStyleGenderTypeColorSize", aParameters);
                Product product = new Product();
                while (dr.Read())
                {
                    product.Id = (int)dr["ID"];
                    product.StyleName = dr["StyleName"].ToString();
                    product.Gender = dr["Gender"].ToString();
                    product.ProductType = dr["ProductType"].ToString();
                    product.Description = dr["Description"].ToString();
                    product.StockQuantity = (int)dr["StockQuantity"];
                    product.UnitPrice = (int)dr["UnitPrice"];
                }

                return product;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public int GetPurchesOrderByTypeUser(string ProductType, string userId)
        {
            try
            {
                List<PurchesOrder> purchesOrders = new List<PurchesOrder>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ProductType", ProductType));
                aParameters.Add(new SqlParameter("@UserId", userId));


                var quantity = 0;
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetPurchesOrderByTypeUser", aParameters);

                while (dr.Read())
                {
                    quantity += (int)dr["Quantity"];
                    PurchesOrder purchesOrder = new PurchesOrder();
                    //purchesOrder.Id = (int)dr["ID"];
                    //purchesOrder.StyleName = dr["StyleName"].ToString();
                    //purchesOrder.Gender = dr["Gender"].ToString();
                    //purchesOrder.ProductType = dr["ProductType"].ToString();
                    //purchesOrder.Description = dr["Description"].ToString();
                    purchesOrder.Quantity = (int)dr["Quantity"];
                    //purchesOrder.UnitPrice = (int)dr["UnitPrice"];

                    purchesOrders.Add(purchesOrder);
                }

                return quantity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public BaseResultResponse GetOrderDetailsByOrderId(string OrderId)
        {
            //List < PurchesOrderVM >
            try
            {
                List<PurchesOrderVM> purchesOrderVMs = new List<PurchesOrderVM>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@PurchesOrderMasterId", OrderId));                

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetOrderDetailsByOrderId", aParameters);

                while (dr.Read())
                {

                    PurchesOrderVM purchesOrderVM = new PurchesOrderVM();
                    var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    purchesOrderVM.Id = (int)dr["ID"];
                    purchesOrderVM.StyleName = dr["StyleName"].ToString();
                    purchesOrderVM.Gender = dr["Gender"].ToString();
                    purchesOrderVM.ProductType = dr["ProductType"].ToString();
                    purchesOrderVM.Color = dr["Color"].ToString();
                    purchesOrderVM.Size = dr["Size"].ToString();
                    purchesOrderVM.Description = dr["Description"].ToString();
                    purchesOrderVM.Quantity = (int)dr["Quantity"];
                    purchesOrderVM.UnitPrice = (int)dr["UnitPrice"];
                    purchesOrderVM.TotalPrice = (decimal)dr["TotalPrice"];
                    purchesOrderVM.OrderNumber = "";
                    purchesOrderVM.UserId = "";
                   

                    purchesOrderVM.CreateDate = dateTime.Split(' ')[0];
                    purchesOrderVM.CreateTime = dateTime.Split(' ')[1] + ' ' + dateTime.Split(' ')[2];

                    purchesOrderVMs.Add(purchesOrderVM);
                }

                var baseResultResponse = baseResultResponseMethode(purchesOrderVMs);
               
                
                return baseResultResponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        #region for admin panel 

        public BaseResultResponse GetAllOrderByUserAndStatus(string userId,string status)
        {
            //List < PurchesOrderMasterVM >
            try
            {

                List<PurchesOrderMasterVM> purchesOrderMasterVMs = new List<PurchesOrderMasterVM>();
                accessManager.SqlConnectionOpen(DataBase.StockLotDB);


                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                aParameters.Add(new SqlParameter("@Status", status));


                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllOrderByUserAndStatus", aParameters);
                while (dr.Read())
                {
                    PurchesOrderMasterVM purchesOrderMasterVM = new PurchesOrderMasterVM();
                    var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");
                    var stas = dr["Status"].ToString();

                    purchesOrderMasterVM.Id = (int)dr["Id"];
                    purchesOrderMasterVM.OrderNumber = dr["OrderNumber"].ToString();
                    purchesOrderMasterVM.UserId = dr["UserId"].ToString();
                    purchesOrderMasterVM.Status = Convert.ToInt32(stas);
                    purchesOrderMasterVM.Remarks = dr["Remarks"].ToString();
                    purchesOrderMasterVM.NetPrice = (decimal)dr["NetPrice"];
                    //purchesOrderMasterVM.CreateDateTime = (DateTime)dr["CreateDateTime"];

                    purchesOrderMasterVM.CreateDate = dateTime.Split(' ')[0];
                    purchesOrderMasterVM.CreateTime = dateTime.Split(' ')[1] + ' ' + dateTime.Split(' ')[2];

                    purchesOrderMasterVMs.Add(purchesOrderMasterVM);
                }

                var baseResultResponse = baseResultResponseMethode(purchesOrderMasterVMs);

                return baseResultResponse;

                //return purchesOrderMasterVMs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse UpdateOrderStatus(PurchesOrderMaster purchesOrderMaster)
        {
            try
            {
                

                accessManager.SqlConnectionOpen(DataBase.StockLotDB);
                ResultResponse result = new ResultResponse();
                //DataValidation(product);                 

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@OrderNumber", purchesOrderMaster.OrderNumber));
                aParameters.Add(new SqlParameter("@Status", purchesOrderMaster.Status));
                aParameters.Add(new SqlParameter("@Remarks", purchesOrderMaster.Remarks));
                aParameters.Add(new SqlParameter("@ModifiDateTime", DateTime.Now));

                result.isSuccess = accessManager.SaveData("sp_UpdateOrderStatus", aParameters);

                //if (result.pk > 0)
                //{
                //    result.msg = "Data Save Successfully";
                //    result.isSuccess = true;
                //}

                result.msg = "Data Update Successfully";
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        #endregion


        #region private

        private BaseResultResponse baseResultResponseMethode(dynamic data)
        {
            try
            {
                BaseResultResponse baseResultResponse = new BaseResultResponse();
                if (data.Count > 0)
                {
                    baseResultResponse.data = data;
                    baseResultResponse.msg = "Successful";
                    baseResultResponse.isSuccess = true;
                    baseResultResponse.status = "200";
                }
                else
                {
                    baseResultResponse.data = data;
                    baseResultResponse.msg = "No Record Found";
                    baseResultResponse.isSuccess = false;
                    baseResultResponse.status = "204";
                }

                return baseResultResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}