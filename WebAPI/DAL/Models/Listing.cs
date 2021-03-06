using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Listing
    {
        public ErrorCode ErrorCode { get; set; }
        public Paging Paging { get; set; }
        public List<Result> Results { get; set; }
        public List<Pin> Pins { get; set; }
        public string GroupingLevel { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ErrorCode
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
        public string Version { get; set; }
    }

    public class Paging
    {
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int MaxRecords { get; set; }
        public int TotalPages { get; set; }
        public int RecordsShowing { get; set; }
        public int Pins { get; set; }
    }

    public class Building
    {
        public string BathroomTotal { get; set; }
        public string Bedrooms { get; set; }
        public string Type { get; set; }
        public string StoriesTotal { get; set; }
        public string SizeInterior { get; set; }
        public string UnitTotal { get; set; }
    }

    public class Address
    {
        public string AddressText { get; set; }
        public object DisseminationArea { get; set; }
        public bool PermitShowAddress { get; set; }
    }

    public class Phone
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string AreaCode { get; set; }
        public string PhoneTypeId { get; set; }
    }

    public class Email
    {
        public string ContactId { get; set; }
    }

    public class Website
    {
        public string website { get; set; }
        public string WebsiteTypeId { get; set; }
    }

    public class Organization
    {
        public int OrganizationID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
        public List<Website> Websites { get; set; }
        public string OrganizationType { get; set; }
        public string Designation { get; set; }
        public bool HasEmail { get; set; }
        public bool PermitFreetextEmail { get; set; }
        public bool PermitShowListingLink { get; set; }
        public string RelativeDetailsURL { get; set; }
        public string PhotoLastupdate { get; set; }
        public string Logo { get; set; }
    }

    public class Phone2
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string AreaCode { get; set; }
        public string PhoneTypeId { get; set; }
    }

    public class Email2
    {
        public string ContactId { get; set; }
    }

    public class Website2
    {
        public string Website { get; set; }
        public string WebsiteTypeId { get; set; }
    }

    public class Individual
    {
        public int IndividualID { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public List<Phone2> Phones { get; set; }
        public List<Email2> Emails { get; set; }
        public string Position { get; set; }
        public bool PermitFreetextEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CorporationDisplayTypeId { get; set; }
        public bool PermitShowListingLink { get; set; }
        public string RelativeDetailsURL { get; set; }
        public List<Website2> Websites { get; set; }
        public string Photo { get; set; }
        public string DesignationCodes { get; set; }
        public string AgentPhotoLastUpdated { get; set; }
        public string PhotoHighRes { get; set; }
    }

    public class Address2
    {
        public string AddressText { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public object DisseminationArea { get; set; }
        public bool PermitShowAddress { get; set; }
    }

    public class Photo
    {
        public string SequenceId { get; set; }
        public string HighResPath { get; set; }
        public string MedResPath { get; set; }
        public string LowResPath { get; set; }
        public string LastUpdated { get; set; }
    }

    public class Parking
    {
        public string Name { get; set; }
    }

    public class Property
    {
        public string Price { get; set; }
        public string Type { get; set; }
        public Address2 Address { get; set; }
        public List<Photo> Photo { get; set; }
        public List<Parking> Parking { get; set; }
        public string ParkingSpaceTotal { get; set; }
        public string TypeId { get; set; }
        public string OwnershipType { get; set; }
        public string ConvertedPrice { get; set; }
        public List<int> OwnershipTypeGroupIds { get; set; }
        public string ParkingType { get; set; }
        public string PriceUnformattedValue { get; set; }
        public string AmmenitiesNearBy { get; set; }
    }

    public class Business
    {
    }

    public class Land
    {
        public string SizeTotal { get; set; }
    }

    public class AlternateURL
    {
        public string VideoLink { get; set; }
    }

    public class Result
    {
        public string Id { get; set; }
        public string MlsNumber { get; set; }
        public string PublicRemarks { get; set; }
        public Building Building { get; set; }
        public List<Individual> Individual { get; set; }
        public Listing Property { get; set; }
        public Business Business { get; set; }
        public Land Land { get; set; }
        public string PostalCode { get; set; }
        public string RelativeDetailsURL { get; set; }
        public string StatusId { get; set; }
        public string PhotoChangeDateUTC { get; set; }
        public string Distance { get; set; }
        public string RelativeURLEn { get; set; }
        public string RelativeURLFr { get; set; }
        public AlternateURL AlternateURL { get; set; }
        public string PriceChangeDateUTC { get; set; }
        public bool? HasNewImageUpdate { get; set; }
    }

    public class Pin
    {
        public string key { get; set; }
        public string propertyId { get; set; }
        public int count { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}
