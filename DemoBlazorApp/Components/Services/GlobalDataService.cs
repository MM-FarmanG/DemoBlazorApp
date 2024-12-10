namespace DemoBlazorApp.Components.Services
{
    public class GlobalDataService
    {
        public string UserName { get; set; } = "";

       
        public string GetGlobalData()
        {
            return UserName;
        }

        public void SetGlobalData(string value)
        {
            UserName = value;
        }
    }

}
