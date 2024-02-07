namespace MetaITAPI.Utils.Constants
{
    public static class StatusMessages
    {
        public const string CheckDetails = "Please check the details and try again.";
        public const string AlreadyExist = $"This unique data already exists in the system! {CheckDetails}";
        public const string InvalidInputFields = $"An error occurred due to invalid input fields. {CheckDetails}";
        public const string NotExist = $"Data do not exist! {CheckDetails}";

        public const string SomethingWentWrong = "Something went wrong. Please try again later.";

        public const string StatusSuccess = "Success";

        public const string AuthorNotExist = $"This AuthorId does not exist! {CheckDetails}";
        public const string BookNotExist = $"This BookId does not exist! {CheckDetails}";
    }
}
