using System;
using System.Collections.Generic;
using System.Text;

namespace SUP2021.Services
{
    public class Service
    {
        public string basicServiceCode { get; set; }
        public List<string> additionalServiceCode { get; set; }


        public Service()
        {

            //this.basicServiceCode = "19";
            //this.additionalServiceCode = new List<string>
            //{
            //    "A1"
            //};




        }
    }
    public class Service2
    { 
        public Service service { get; set; }
    }

    public class Shipment2
    {
        public Service2 shipment { get; set; }
    }

    public class Shipment
    {
       // public Service service { get; set; }
        public List<long> items { get; set; }

        public Shipment() {

            //this.items = new List<long>
            //{
            //     373500489530470000
            //};

        }    
    
    }

    public class Location
    {
        public string place { get; set; }
        public string streetName { get; set; }
        public string streetNumber { get; set; }
        public int postalCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }

        //public Location()
        //{
        //    this.place = "Company name or information about pickup place e.g.garage"; 
        //    this.streetName = "James Norris";
        //    this.streetNumber = "117"; 
        //    this.postalCode = 19162;
        //    this.city = "Sollentuna";
        //    this.countryCode = "SE";
        //}

    }

    public class Location2
    {
        public Location location{ get; set; }
    }

    public class Order
    {
        public string customerNumber { get; set; }
        public string orderReference { get; set; }
        public string contactName { get; set; }
        public string contactEmail { get; set; }
        public string phoneNumber { get; set; }
        public string smsNumber { get; set; }
        public string entryCode { get; set; }

        public Order()
        {
            //this.customerNumber = "1234567891";
            //this.orderReference = "Ref-1212122A";
            //this.contactName = "Nils Andersson";
            //this.contactEmail = "Nils.Andersson@postnord.com";
            //this.phoneNumber = "+4670788888";
            //this.smsNumber = "+4670788888";
            //this.entryCode = "8216";        
        }

    }

    public class Order2
    {
        public Order order { get; set; }
    }
    public class PickupService
    {
        public string typeOfItem { get; set; }
        public int noUnits { get; set; }

        //public PickupService() {

        //    this.typeOfItem = "parcel";
        //    this.noUnits = 1;
        //}
    }

    public class PickUp2
    {
        public List<PickupService> pickups { get; set; }
       // public PickupService pickup { get; set; }
    }

    public class DateAndTimes
    {
        public DateTime readyPickupDate { get; set; }
        public DateTime latestPickupDate { get; set; }
        public DateTime earliestPickupDate { get; set; }

    //    public DateAndTimes()
    //    {
    //        this.readyPickupDate = new DateTime(2021, 5, 13);
    //        this.latestPickupDate = new DateTime(2021, 5, 13);
    //        this.earliestPickupDate = new DateTime(2021, 5, 13);

    //}
    }
    public class DateAndTimes2
    { 
        public DateAndTimes DateAndTimes { get; set; } 
    }

    public class Root
    {
        public Shipment shipment { get; set; }
        public Location location { get; set; }
        public Order order { get; set; }
        public List<PickupService> pickup { get; set; }
        public DateAndTimes dateAndTimes { get; set; }
    }
}
