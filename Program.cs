using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace databases
{
    class Program
    {
        static void Main(string[] args)
        {
            unitTests u = new unitTests();
        
           //u.add();
           //u.update();
        //   u.delete();
        }

    }

    public abstract class user
    {
        [Key]
        public string userName { get; set; }
        public string mail { get; set; }
        public string tel { get; set; }
        public string strongPassword { get; set; }
        public bool connected { get; set; }
        public int age { get; set; }
        public virtual List<alert> alert { get; set; }
    }

    public class businessOwner : user
    {

        public virtual List<couponRequest> couponRequest { get; set; }
        public virtual List<business> business { get; set; }
    }

    public class manager : user
    {
        public virtual List<coupon> coupons { get; set; }
        public virtual List<couponRequest> couponRequest { get; set; }
    }

    public class couponRequest
    {
        [Key]
        public string description { get; set; }
        public virtual coupon coupons { get; set; }
        public virtual customer customer { get; set; }
        public virtual manager manager { get; set; }
    }

    public class coupon
    {
        [Key]
        public string name { get; set; }

        //[ForeignKey("business")]
        //public string businessName { get; set; }
        public string description { get; set; }

        public double originalPrice { get; set; }
        public double discountPrice { get; set; }
        public double rate { get; set; }
        public DateTime lastDate { get; set; }
        public virtual List<couponRequest> couponRequest { get; set; }
        public virtual manager manager { get; set; }
        public virtual List<order> order { get; set; }
        public virtual List<category> category { get; set; }
        public string businessName { get; set; }
        public virtual business business { get; set; }


    }

    public class customer : user
    {
        public virtual List<couponRequest> couponRequest { get; set; }
        public virtual List<category> category { get; set; }

        public virtual List<order> order { get; set; }
    }

    public class business
    {
        [Key]
        public string businessName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string description { get; set; }
        public virtual businessOwner businessOwner { get; set; }
        public virtual category category { get; set; }
        public virtual List<order> order { get; set; }
        public virtual List<coupon> coupons { get; set; }
        public virtual List<buisCouponReport> buisCouponReport { get; set; }
        public virtual List<buisRateReport> buisRateReport { get; set; }
    }

    public class category
    {
        [Key]
        public string categoryName { get; set; }
        public virtual List<customer> customer { get; set; }
        public virtual List<business> business { get; set; }
    }

    public class order
    {
        [Key]
        public string serialKey { get; set; }
        public string paymentDetails { get; set; }
        public DateTime dateTime { get; set; }
        public bool used { get; set; }
        public virtual customer customer { get; set; }
        public virtual userCouponReport userCouponReport { get; set; }
        public virtual expiration expirationAlert { get; set; }
        public virtual business business { get; set; }
        public virtual List<coupon> coupon { get; set; } 
        //[Key, ForeignKey("coupon"), Column(Order = 0)]
        public int name { get; set; }
        //[Key, ForeignKey("coupon"), Column(Order = 0)]
        public int date { get; set; }

    }

    public abstract class report
    {
        [Key]
        [Column(Order = 1)]
        public string subject { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime date { get; set; }

    }

    public class userCouponReport : report
    {
        public virtual List<order> order { get; set; }
    }
    public class buisCouponReport : report
    {
        public virtual List<business> business { get; set; }
    }

    public class buisRateReport : report
    {
        public virtual List<business> business { get; set; }
    }

    public class rateCoupon
    {
        //[Key, ForeignKey("order"), Column(Order = 0)]

        //[Key, ForeignKey("customer"), Column(Order = 1)]
        [Key]
        public int rate { get; set; }




        //[Key, ForeignKey("customer"), Column(Order = 1)]
        //public int userName { get; set; }
        public virtual customer customer { get; set; }
        public string serialKey { get; set; }
        public virtual List<order> order { get; set; }
    }

    public abstract class alert
    {
        [Key]
        [Column(Order = 1)]
        public string subject { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime date { get; set; }
        public virtual List<user> user { get; set; }

    }

    public class location : alert
    {
    }

    public class expiration : alert
    {
        public virtual List<order> order { get; set; }
    }

    public class categoryAlert : alert
    {
        public virtual List<coupon> coupon { get; set; }
    }
    public class CouponsDatabase : DbContext
    {
        public DbSet<businessOwner> businessOwner { get; set; }
        public DbSet<couponRequest> couponRequest { get; set; }
        public DbSet<coupon> coupon { get; set; }
        public DbSet<manager> manager { get; set; }
        public DbSet<customer> customer { get; set; }
        public DbSet<category> category { get; set; }
        public DbSet<business> business { get; set; }
        public DbSet<order> order { get; set; }
        public DbSet<userCouponReport> userCouponReport { get; set; }
        public DbSet<buisCouponReport> buisCouponReport { get; set; }
        public DbSet<buisRateReport> buisRateReport { get; set; }
        public DbSet<rateCoupon> rateCoupon { get; set; }
        public DbSet<location> locationAlert { get; set; }
        public DbSet<expiration> expirationAlert { get; set; }
        public DbSet<categoryAlert> categoryAlert { get; set; }
    }
}

