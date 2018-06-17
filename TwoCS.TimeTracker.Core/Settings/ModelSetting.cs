namespace TwoCS.TimeTracker.Core.Settings
{
    public static class ModelSetting
    {
        public static class User
        {
            public const int UserNameMinLength = 2;

            public const int UserNameMaxLength = 30;

            public const int PasswordMinLength = 6;

            public const int PasswordMaxLength = 30;
        }

        public static class TimeRecord
        {

            public const int NameMaxLength = 200;

            public const int DescriptionMaxLength = 1000;
        }
    }
}
