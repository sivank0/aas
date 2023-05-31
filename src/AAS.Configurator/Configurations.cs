using AAS.Tools.Types.IDs;

namespace AAS.Configurator;

public static class Configurations
{
    public static class BackOffice
    {
        public static String Host = "https://tec.yvgrishin.ru";
        public static ID DefaultRoleId = ID.Parse("B42E99B84F7E898A11EDFF05C930DD86");
    }

    public static class FileStorage
    {
        public static String UploadFolder = "C:/FileStorage/AAS";
        public static String Host = "http://192.168.4.95:44395/";
    }

    public static class EmailVerification
    {
        public static String VerificationUrlReplacingValue = "replaceuseremailverificationtoken";
        public static String VerificationUrlTemplate = $"{BackOffice.Host}/email_verification/{VerificationUrlReplacingValue}";
        public static String Login = "testpost808@gmail.com";
        public static String Password = "ffancqbmcrhsjabi";
    }
}
