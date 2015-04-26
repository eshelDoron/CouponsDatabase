using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using databases;

namespace databases
{
    class unitTests
    {

        public void add()
        {
            using (var db1 = new CouponsDatabase())
            {


                try
                {
          
                    //customer
                    string temp = "";
                    var cu = new customer { userName = "yuval", mail = "haran@bgu.ac.il", tel = "0547123123", strongPassword = "123123", connected = true, age = 25 };
                    db1.customer.Add(cu);
                    db1.SaveChanges();
                    var query = from c in db1.customer
                                orderby c.userName
                                select c;

                    foreach (var item in query)
                    {
                        if (item.userName == "yuval")
                        {
                            temp = item.userName;
                        }
                   }
                    Assert.AreEqual("yuval", temp);
                    //buisnessOwner
                    var bo = new businessOwner { userName = "eshel", mail = "beshel@bgu.ac.il", tel = "0547111111", strongPassword = "123123", connected = true, age = 25 };
                    db1.businessOwner.Add(bo);
                    db1.SaveChanges();
                    var query1 = from b in db1.businessOwner
                                orderby b.userName
                                select b;

                    foreach (var item in query1)
                    {
                        if (item.userName == "eshel")
                        {
                            temp = item.userName;
                        }
                    }
                    Assert.AreEqual("eshel", temp);

                    //Manager
                    var ma = new manager { userName = "raz", mail = "raz@bgu.ac.il", tel = "0547111222", strongPassword = "123123", connected = true, age = 25 };
                    db1.manager.Add(ma);
                    db1.SaveChanges();

                    var query2 = from b in db1.manager
                                orderby b.userName
                                select b;

                    foreach (var item in query2)
                    {
                        if (item.userName == "raz")
                        {
                            temp = item.userName;
                        }
                    }
                    Assert.AreEqual("raz", temp);
                    //locationAlert
                    DateTime date1 = new DateTime(2015, 3, 1, 7, 0, 0);
                    var la = new location { subject = "it is near", date = date1 };
                    la.user = new List<user>();
                    la.user.Add(cu);
                    db1.locationAlert.Add(la);
                    db1.SaveChanges();
                    var query3 = from b in db1.locationAlert
                                orderby b.subject
                                select b;

                    foreach (var item in query3)
                    {
                        if (item.subject == "it is near")
                        {
                            temp = item.subject;
                        }
                    }
                    Assert.AreEqual("it is near", temp);

                    var ex = new expiration { subject = "expires soon", date = date1 };
                    ex.user = new List<user>();
                    ex.user.Add(cu);
                    db1.expirationAlert.Add(ex);
                    db1.SaveChanges();

                    var query4 = from b in db1.expirationAlert
                                orderby b.subject
                                select b;

                    foreach (var item in query4)
                    {
                        if (item.subject == "expires soon")
                        {
                            temp = item.subject;
                        }
                    }
                    Assert.AreEqual("espires soon", temp);
                    Assert.IsNotNull(ex.user.First());


                    var ca = new categoryAlert { subject = "a sports coupon", date = date1 };
                    ca.user = new List<user>();
                    ca.user.Add(cu);
                    db1.categoryAlert.Add(ca);
                    db1.SaveChanges();

                    var query5 = from b in db1.categoryAlert
                                orderby b.subject
                                select b;

                    foreach (var item in query5)
                    {
                        if (item.subject == "a sports coupon")
                        {
                            temp = item.subject;
                        }
                    }
                    Assert.AreEqual("a sports coupon", temp);
                    
                    Assert.IsNotNull(ca.user.First());

                    var ur = new userCouponReport { subject = "user used coupon once", date = date1 };
                    db1.userCouponReport.Add(ur);
                    db1.SaveChanges();

                    var query6 = from b in db1.userCouponReport
                                orderby b.subject
                                select b;

                    foreach (var item in query6)
                    {
                        if (item.subject == "user used coupon once")
                        {
                            temp = item.subject;
                        }
                    }
                    Assert.AreEqual("user used coupon once", temp);


                    var bu = new buisCouponReport { subject = "business issued coupon", date = date1 };
                    db1.buisCouponReport.Add(bu);
                    db1.SaveChanges();

                    var query7 = from b in db1.buisCouponReport
                                orderby b.subject
                                select b;

                    foreach (var item in query7)
                    {
                        if (item.subject == "business issued coupon")
                        {
                            temp = item.subject;
                        }
                    }
                    Assert.AreEqual("business issued coupon", temp);


                    var br = new buisRateReport { subject = "business rate is good", date = date1 };
                    db1.buisRateReport.Add(br);
                    db1.SaveChanges();

                    var query8 = from b in db1.buisRateReport
                                orderby b.subject
                                select b;

                    foreach (var item in query8)
                    {
                        if (item.subject == "business rate is good")
                        {
                            temp = item.subject;
                        }
                    }
                    Assert.AreEqual("business rate is good", temp);

                    var bs = new business { businessName = "caldo", address = "metsada 40", city = "beer sheva", description = "pizza" };
                    bs.buisCouponReport = new List<buisCouponReport>();
                    bs.buisCouponReport.Add(bu);
                    bs.buisRateReport = new List<buisRateReport>();
                    bs.buisRateReport.Add(br);
                    bs.businessOwner = bo;
                    db1.business.Add(bs);
                    db1.SaveChanges();

                    var query9 = from b in db1.business
                                orderby b.businessName
                                select b;

                    foreach (var item in query9)
                    {

                        if (item.businessName == "caldo")
                        {
                            temp = item.businessName;
                        }
                    }
                    Assert.AreEqual("caldo", temp); Assert.IsNotNull(bs.buisCouponReport.First());
                    Assert.IsNotNull(bs.buisRateReport.First());
                    Assert.IsNotNull(bs.businessOwner);

                    var cat = new category { categoryName = "food" };
                    cat.business = new List<business>();
                    cat.business.Add(bs);
                    cat.customer = new List<customer>();
                    cat.customer.Add(cu);
                    bs.category = cat;
                    db1.category.Add(cat);
                    db1.SaveChanges();

                    var query10 = from b in db1.category
                                orderby b.categoryName
                                select b;

                    foreach (var item in query10)
                    {

                        if (item.categoryName == "food")
                        {
                            temp = item.categoryName;
                        }
                    }
                    Assert.AreEqual("food", temp); Assert.IsNotNull(cat.business.First());
                    Assert.IsNotNull(cat.customer.First());
                    Assert.IsNotNull(bs.category);

                    var ord = new order { serialKey = "123", paymentDetails = "cash", dateTime = date1, used = false };
                    ord.business = bs;
                    ord.customer = cu;
                    ord.expirationAlert = ex;
                    ord.userCouponReport = ur;
                    db1.order.Add(ord);
                    db1.SaveChanges();

                    var query11 = from b in db1.order
                                orderby b.serialKey
                                select b;

                    foreach (var item in query11)
                    {

                        if (item.serialKey == "123")
                        {
                            temp = item.serialKey;
                        }
                    }
                    Assert.AreEqual("123", temp); Assert.IsNotNull(ord.business);
                    Assert.IsNotNull(ord.customer);
                    Assert.IsNotNull(ord.expirationAlert);
                    Assert.IsNotNull(ord.userCouponReport);

                    var cou = new coupon { name = "free pizza", description = "free pizza", originalPrice = 70.0, discountPrice = 0, rate = 10, lastDate = date1 };
                    cou.manager = ma;
                    cou.business = bs;
                    cou.businessName = bs.businessName;
                    cou.category = new List<category>();
                    cou.category.Add(cat);
                    cou.order = new List<order>();
                    cou.order.Add(ord);
                    db1.coupon.Add(cou);
                    db1.SaveChanges();

                    var query12 = from b in db1.coupon
                                orderby b.name
                                select b;

                    foreach (var item in query12)
                    {

                        if (item.name == "free pizza")
                        {
                            temp = item.name;
                        }
                    }
                    Assert.AreEqual("free pizza", temp); Assert.IsNotNull(cou.manager);
                    Assert.IsNotNull(cou.business);
                    Assert.IsNotNull(cou.category);
                    Assert.IsNotNull(cou.order);

                    var couR = new couponRequest { description = "new" };
                    couR.coupons = cou;
                    couR.customer = cu;
                    couR.manager = ma;
                    db1.couponRequest.Add(couR);
                    db1.SaveChanges();

                    var query13 = from b in db1.couponRequest
                                orderby b.description
                                select b;

                    foreach (var item in query13)
                    {

                        if (item.description == "new")
                        {
                            temp = item.description;
                        }
                    }
                    Assert.AreEqual("new", temp); Assert.IsNotNull(couR.manager);
                    Assert.IsNotNull(couR.coupons);
                    Assert.IsNotNull(couR.customer);
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                    Console.ReadLine();
                }

            }
        }

        public void delete()
        {
            using (var db1 = new CouponsDatabase())
            {
                try
                {
                    
                    int count = 0;
                    int count1 = 0;
                    
                   
                    
                    count = 0;
                    var query = from c in db1.customer
                                orderby c.userName
                                select c;

                    foreach (var item in query)
                    {
                        count++;
                    }
                    var cu = db1.customer.First();
                    db1.customer.Remove(cu);
                    db1.SaveChanges();
                    var querys = from c in db1.customer
                                orderby c.userName
                                select c;

                    foreach (var item in querys)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);
                    //buisnessOwner
                    count1 = 0;
                    count = 0;
                    var query1 = from c in db1.businessOwner
                                 orderby c.userName
                                 select c;

                    foreach (var item in query1)
                    {
                        count++;
                    }
                    var bo = db1.businessOwner.First();
                    db1.businessOwner.Remove(bo);
                    db1.SaveChanges();

                    var query1s = from c in db1.businessOwner
                                orderby c.userName
                                select c;

                    foreach (var item in query1s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);
                    //Manager
                    count1 = 0;
                    count = 0;
                    var query2 = from c in db1.manager
                                  orderby c.userName
                                  select c;

                    foreach (var item in query2)
                    {
                        count++;
                    }
                    var ma = db1.manager.First();
                    db1.manager.Remove(ma);
                    db1.SaveChanges();

                    
                    var query2s = from c in db1.manager
                                orderby c.userName
                                select c;

                    foreach (var item in query2s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);
                    //locationAlert
                    count1 = 0;
                    count = 0;
                    var query3 = from c in db1.locationAlert
                                 orderby c.subject
                                 select c;

                    foreach (var item in query3)
                    {
                        count++;
                    }
                    var la = db1.locationAlert.First();
                    db1.locationAlert.Remove(la);
                    db1.SaveChanges();

                    
                    var query3s = from c in db1.locationAlert
                                orderby c.subject
                                select c;

                    foreach (var item in query3s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);


                    count = 0;
                    count1 = 0;
                    var query4 = from c in db1.expirationAlert
                                 orderby c.subject
                                 select c;

                    foreach (var item in query4)
                    {
                        count++;
                    }
                    var ex = db1.expirationAlert.First();
                    db1.expirationAlert.Remove(ex);
                    db1.SaveChanges();

                    var query4s = from c in db1.expirationAlert
                                orderby c.subject
                                select c;

                    foreach (var item in query4s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;

                    var query5 = from c in db1.categoryAlert
                                 orderby c.subject
                                 select c;

                    foreach (var item in query5)
                    {
                        count++;
                    }
                    var ca = db1.categoryAlert.First();
                    db1.categoryAlert.Remove(ca);
                    db1.SaveChanges();

                   
                    var query5s = from c in db1.categoryAlert
                                orderby c.subject
                                select c;

                    foreach (var item in query5s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query6 = from c in db1.userCouponReport
                                 orderby c.subject
                                 select c;

                    foreach (var item in query6)
                    {
                        count++;
                    }
                    var ur = db1.userCouponReport.First();
                    db1.userCouponReport.Remove(ur);
                    db1.SaveChanges();

                    
                    var query6s = from c in db1.userCouponReport
                                orderby c.subject
                                select c;

                    foreach (var item in query6s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query7 = from c in db1.buisCouponReport
                                 orderby c.subject
                                 select c;

                    foreach (var item in query7)
                    {
                        count++;
                    }
                    var bu = db1.buisCouponReport.First();
                    db1.buisCouponReport.Remove(bu);
                    db1.SaveChanges();

                    var query7s = from c in db1.buisCouponReport
                                orderby c.subject
                                select c;

                    foreach (var item in query7s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query8 = from c in db1.buisRateReport
                                 orderby c.subject
                                 select c;

                    foreach (var item in query8)
                    {
                        count++;
                    }
                    var br = db1.buisRateReport.First();
                    db1.buisRateReport.Remove(br);
                    db1.SaveChanges();

                    
                    var query8s = from c in db1.buisRateReport
                                orderby c.subject
                                select c;

                    foreach (var item in query8s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query9 = from c in db1.business
                                 orderby c.businessName
                                 select c;

                    foreach (var item in query9)
                    {
                        count++;
                    }
                    var bs = db1.business.First();
                    db1.business.Remove(bs);
                    db1.SaveChanges();

                    
                    var query9s = from c in db1.business
                                orderby c.businessName
                                select c;

                    foreach (var item in query9s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(0, count-count1);

                    count = 0;
                    count1 = 0;
                    var query10 = from c in db1.category
                                  orderby c.categoryName
                                  select c;

                    foreach (var item in query10)
                    {
                        count++;
                    }
                    var cat = db1.category.First();
                    db1.category.Remove(cat);
                    db1.SaveChanges();

                    
                    var query10s = from c in db1.category
                                orderby c.categoryName
                                select c;

                    foreach (var item in query10s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query11 = from c in db1.order
                                  orderby c.serialKey
                                  select c;

                    foreach (var item in query11)
                    {
                        count++;
                    }
                    var ord = db1.order.First();
                    db1.order.Remove(ord);
                    db1.SaveChanges();

                
                    var query11s = from c in db1.order
                                orderby c.serialKey
                                select c;

                    foreach (var item in query11s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query12 = from c in db1.coupon
                                  orderby c.name
                                  select c;

                    foreach (var item in query12)
                    {
                        count++;
                    }
                    var cou = db1.coupon.First();
                    db1.coupon.Remove(cou);
                    db1.SaveChanges();

                    
                    var query12s = from c in db1.coupon
                                orderby c.name
                                select c;

                    foreach (var item in query12s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count-count1);

                    count = 0;
                    count1 = 0;
                    var query13 = from c in db1.couponRequest
                                  orderby c.description
                                  select c;

                    foreach (var item in query13)
                    {
                        count++;
                    }
                    var couR = db1.couponRequest.First();
                    db1.couponRequest.Remove(couR);
                    db1.SaveChanges();

                    
                    var query13s = from c in db1.couponRequest
                                orderby c.description
                                select c;

                    foreach (var item in query13s)
                    {
                        count1++;
                    }
                    Assert.AreEqual(1, count - count1);
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                    Console.ReadLine();
                }

            }
        }

        public void update()
        {
            using (var db1 = new CouponsDatabase())
            {
                try
                {

                    
                    //customer
                    db1.customer.First().userName = "dani";
                    Assert.AreEqual("dani", db1.customer.First().userName);
                 
                    db1.businessOwner.First().userName = "rami";
                    Assert.AreEqual("rami", db1.businessOwner.First().userName);

                    //Manager
                    db1.manager.First().userName = "bat";
                    Assert.AreEqual("bat", db1.manager.First().userName);

                    //locationAlert
                    db1.locationAlert.First().subject = "update";
                    Assert.AreEqual("update", db1.locationAlert.First().subject);

                    db1.expirationAlert.First().subject = "update";
                    Assert.AreEqual("update", db1.expirationAlert.First().subject);


                    db1.categoryAlert.First().subject = "update";
                    Assert.AreEqual("update", db1.categoryAlert.First().subject);

                    db1.userCouponReport.First().subject = "update";
                    Assert.AreEqual("update",db1.userCouponReport.First().subject);

                    db1.buisCouponReport.First().subject = "update";
                    Assert.AreEqual("update", db1.buisCouponReport.First().subject);

                    db1.buisRateReport.First().subject = "update";
                    Assert.AreEqual("update", db1.buisRateReport.First().subject);

                    db1.business.First().businessName = "update";
                    Assert.AreEqual("update", db1.business.First().businessName);


                    db1.category.First().categoryName = "update";
                    Assert.AreEqual("update", db1.category.First().categoryName);

                    db1.order.First().serialKey = "update";
                    Assert.AreEqual("update", db1.order.First().serialKey);

                    db1.coupon.First().name = "update";
                    Assert.AreEqual("update", db1.coupon.First().name);

                    db1.couponRequest.First().description = "update";
                    Assert.AreEqual("update", db1.couponRequest.First().description);
                  
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                    Console.ReadLine();
                }
            }
        }
    }

  
}
