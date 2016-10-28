namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public interface IResourceAttribute
    {
        #region Public Properties
        string BaseName
        {
            get;
            set;
        }

        string KeyPrefix
        {
            get;
            set;
        }

        string AssemblyFullName
        {
            get;
            set;
        } 
        #endregion
    }
}