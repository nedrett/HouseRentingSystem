namespace HouseRentingSystem.Infrastructure.Data
{
    public class DataConstants
    {
        public class Category
        {
            public const int NameMaxLength = 50;
        }

        public class House
        {
            public const int TitleMinLength = 10;

            public const int TitleMaxLength = 50;

            public const int AddressMinLength = 30;

            public const int AddressMaxLength = 150;

            public const int DescriptionMinLength = 50;

            public const int DescriptionMaxLength = 150;

            public const int PricePerMonthMinValue= 0;

            public const int PricePerMonthMaxValue = 2000;

            public const int ImageUrlMaxLength = 200;

        }

        public class Agent
        {
            public const int PhoneNumberMinLength = 7;

            public const int PhoneNumberMaxLength = 15;
        }
    }
}
