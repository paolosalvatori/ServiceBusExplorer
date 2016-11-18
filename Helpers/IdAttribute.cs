#region Using Directives
using System; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IdAttribute : Attribute
    {
        #region Public Constructors
        public IdAttribute()
        {
            PropertyId = 0;
            CategoryId = 0;
        }

        public IdAttribute(int propertyId, int categoryId)
        {
            PropertyId = propertyId;
            CategoryId = categoryId;
        } 
        #endregion

        #region Public Properties
        public int PropertyId { get; set; }
        public int CategoryId { get; set; } 
        #endregion
    }
}