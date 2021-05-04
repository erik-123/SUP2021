using System;
using System.Collections.Generic;
using System.Text;

namespace SUP2021.Models
{
    public class CustomerSupport
    {
        public string country { get; set; }
        public string customerSupportPhoneNo { get; set; }
    }

    public class AvailableForPickupStandard
    {
        public object earliestTime { get; set; }
        public string day { get; set; }
    }

    public class TimeSlots
    {
        public List<AvailableForPickupStandard> availableForPickupStandard { get; set; }
        public List<object> availableForPickupEarlyCollect { get; set; }
    }

    public class Product
    {
        public string name { get; set; }
        public TimeSlots timeSlots { get; set; }
    }

    public class Pickup
    {
        public object cashOnDelivery { get; set; }
        public List<Product> products { get; set; }
        public List<object> heavyGoodsProducts { get; set; }
    }

    public class VisitingAddress
    {
        public string countryCode { get; set; }
        public string city { get; set; }
        public string streetName { get; set; }
        public string streetNumber { get; set; }
        public string postalCode { get; set; }
        public object additionalDescription { get; set; }
    }

    public class DeliveryAddress
    {
        public string countryCode { get; set; }
        public string city { get; set; }
        public string streetName { get; set; }
        public string streetNumber { get; set; }
        public string postalCode { get; set; }
        public object additionalDescription { get; set; }
    }

    public class NotificationArea
    {
        public List<string> postalCodes { get; set; }
    }

    public class Coordinate
    {
        public string countryCode { get; set; }
        public double northing { get; set; }
        public double easting { get; set; }
        public string srId { get; set; }
    }

    public class PostalService
    {
        public string openTime { get; set; }
        public string openDay { get; set; }
        public string closeTime { get; set; }
        public string closeDay { get; set; }
    }

    public class OpeningHours
    {
        public List<object> specialDates { get; set; }
        public List<PostalService> postalServices { get; set; }
    }

    public class Type
    {
        public int groupTypeId { get; set; }
        public string groupTypeName { get; set; }
    }

    public class ServicePoint
    {
        public string name { get; set; }
        public string servicePointId { get; set; }
        public string phoneNoToCashRegister { get; set; }
        public object routingCode { get; set; }
        public object handlingOffice { get; set; }
        public object locationDetail { get; set; }
        public int routeDistance { get; set; }
        public Pickup pickup { get; set; }
        public VisitingAddress visitingAddress { get; set; }
        public DeliveryAddress deliveryAddress { get; set; }
        public NotificationArea notificationArea { get; set; }
        public List<Coordinate> coordinates { get; set; }
        public OpeningHours openingHours { get; set; }
        public Type type { get; set; }
    }

    public class ServicePointNames
    {
        public string name { get; set; }

    }

    public class ServicePointInformationResponse
    {
        public List<CustomerSupport> customerSupports { get; set; }
        public List<ServicePoint> servicePoints { get; set; }
        public List<ServicePointNames> servicePointNames { get; set; }
    }

    public class Root
    {
        public ServicePointInformationResponse servicePointInformationResponse { get; set; }
    }

}
