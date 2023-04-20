namespace Data.Model;

public class EmailSettings
{
    public string ServerName;
    public int ServerPort;
    public bool UseSsl;
    public string Login;
    public string Password;
    public bool WriteAsFile;
    public string FileLocation = @"c:\product_store_emails";
    //public EmailSettings()
    //{
    //ServerName = ConfigurationManager.AppSettings["ServerName"];
    //ServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
    //Login = ConfigurationManager.AppSettings["Login"];
    //Password = ConfigurationManager.AppSettings["Password"];
    //UseSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSsl"]);
    //WriteAsFile = Convert.ToBoolean(ConfigurationManager.AppSettings["WriteAsFile"]);
    //}
}