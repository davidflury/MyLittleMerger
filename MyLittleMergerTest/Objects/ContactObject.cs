using System;

namespace MyLittleMergerTest.Objects
{
    public static class ContactData
    {
        public static ContactObject David => new ContactObject
        {
            Id = Guid.Parse("033D96BD-E895-438A-B600-978382D49ADB"),
            FirstName = "David",
            LastName = "Miller",
            Friends = new[] { "Jennifer", "Peter" },
            Gender = GenderEnum.Male,
            RegistrationDate = new DateTime(2012, 2, 5),
            Birthday = new DateTime(1994, 9, 29),
            Addresses = new[]
            {
                new AddressObject
                {
                    Type = AddressTypeEnum.Private,
                    Street = "Main Street 1",
                    City = "New York",
                    Zip = 80000,
                    County = new CountryObject
                    {
                        Name = "United States of America",
                        Code = "USA"
                    }
                },
                new AddressObject
                {
                    Type = AddressTypeEnum.Business,
                    City = "Dubai",
                    Street = "Tower 14535A",
                    Zip = 1000,
                    County = new CountryObject
                    {
                        Name = "United Arab Emirates",
                        Code = "UAE"
                    }
                }
            },
            TwitterAccount = new SocialMediaObject
            {
                Name = "Twitter",
                Address = new Uri("https://twitter.com/david.m"),
                Active = true
            },
            FacebookAccount = new SocialMediaObject
            {
                Name = "Facebook",
                Address = new Uri("https://facebook.com/david.miller"),
                Active = false
            },
            InstagramAccount = new SocialMediaObject
            {
                Name = "Instagram",
                Address = new Uri("https://instagram.com/fancy-david"),
                Active = true
            }
        };

        public static ContactObject Jennifer => new ContactObject
        {
            Id = Guid.Parse("5BA42D94-005A-468A-A494-9B09033A0565"),
            FirstName = "Jennifer",
            LastName = "Jones",
            RegistrationDate = new DateTime(2008, 11, 23),
            Gender = GenderEnum.Female,
            Addresses = new[]
            {
                new AddressObject
                {
                    Type = AddressTypeEnum.Private,
                    Street = "Pflanzenstrasse 12",
                    City = "Zürich",
                    Zip = 8004,
                    County = new CountryObject
                    {
                        Name = "Switzerland",
                        Code = "CH"
                    }
                }
            },
            FacebookAccount = new SocialMediaObject
            {
                Name = "Facebook",
                Address = new Uri("https://facebook.com/raphaeljones2134"),
                Active = true
            }
        };

        public static ContactObject Sebastian => new ContactObject
        {
            Id = Guid.Parse("033D96BD-E895-438A-B600-978382D49ADB"),
            FirstName = "Sebastian",
            LastName = "Sanders",
            Friends = new[] { "David", "Carlos", "Jennifer" },
            Birthday = new DateTime(1967, 3, 14),
            Addresses = new[]
            {
                new AddressObject
                {
                    Type = AddressTypeEnum.Business,
                    City = "Dubai",
                    Street = "Palace 2",
                    Zip = 1001,
                    County = new CountryObject
                    {
                        Name = "United Arab Emirates",
                        Code = "UAE"
                    }
                }
            }
        };
    }

    public class ContactObject
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Friends { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? Birthday { get; set; }
        public GenderEnum? Gender { get; set; }
        public AddressObject[] Addresses { get; set; }
        public SocialMediaObject TwitterAccount { get; set; }
        public SocialMediaObject FacebookAccount { get; set; }
        public SocialMediaObject InstagramAccount { get; set; }
    }

    public enum GenderEnum { Male, Female }

    public class AddressObject
    {
        public AddressTypeEnum Type { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public CountryObject County { get; set; }
    }

    public class CountryObject
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public enum AddressTypeEnum { Private, Business }

    public class SocialMediaObject
    {
        public string Name { get; set; }
        public Uri Address { get; set; }
        public bool Active { get; set; }
    }
}
