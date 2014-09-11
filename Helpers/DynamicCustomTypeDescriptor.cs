#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Resources; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class DynamicCustomTypeDescriptor : CustomTypeDescriptor
    {
        #region Private Fields
        private readonly PropertyDescriptorList propertyDescriptorList = new PropertyDescriptorList();
        private readonly object instance;
        private readonly Hashtable hashRM = new Hashtable();
        private CustomSortOrder propertySortOrder = CustomSortOrder.AscendingById;
        private CustomSortOrder categorySortOrder = CustomSortOrder.AscendingById;
        private ISite site = null;
        #endregion

        #region Public Properties
        public DynamicCustomTypeDescriptor(ICustomTypeDescriptor ctd, object instance)
            : base(ctd)
        {
            this.instance = instance;
            GetProperties();
        } 
        #endregion

        #region Public Properties
        public CustomSortOrder PropertySortOrder
        {
            get
            {
                return propertySortOrder;
            }
            set
            {
                propertySortOrder = value;
            }
        }

        public CustomSortOrder CategorySortOrder
        {
            get
            {
                return categorySortOrder;
            }
            set
            {
                categorySortOrder = value;
            }
        }
        #endregion

        #region Public Methods
        public CustomPropertyDescriptor GetProperty(string propertyName)
        {
            var cpd = propertyDescriptorList.FirstOrDefault(a => String.Compare(a.Name, propertyName, true) == 0);
            return cpd;
        }

        public CustomPropertyDescriptor CreateProperty(string name, Type type, object value, int index, params Attribute[] attributes)
        {
            var cpd = new CustomPropertyDescriptor(instance, name, type, value, attributes);
            if (index == -1)
            {
                propertyDescriptorList.Add(cpd);
            }
            else
            {
                propertyDescriptorList.Insert(index, cpd);
            }
            TypeDescriptor.Refresh(instance);
            return cpd;
        }

        public bool RemoveProperty(string propertyName)
        {
            var cpd = propertyDescriptorList.FirstOrDefault(a => String.Compare(a.Name, propertyName, true) == 0);
            var bReturn = propertyDescriptorList.Remove(cpd);
            TypeDescriptor.Refresh(instance);
            return bReturn;
        }

        public void ResetProperties()
        {
            propertyDescriptorList.Clear();
            GetProperties();
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            var pdl = propertyDescriptorList.FindAll(pd => pd.Attributes.Contains(attributes));
            PreProcess(pdl);
            var pdcReturn = new PropertyDescriptorCollection(pdl.ToArray());
            return pdcReturn;
        }

        public override sealed PropertyDescriptorCollection GetProperties()
        {
            if (propertyDescriptorList.Count == 0)
            {
                var pdc = base.GetProperties();  // this gives us a readonly collection, no good    
                foreach (PropertyDescriptor pd in pdc)
                {
                    if (!(pd is CustomPropertyDescriptor))
                    {
                        var cpd = new CustomPropertyDescriptor(base.GetPropertyOwner(pd), pd);
                        propertyDescriptorList.Add(cpd);
                    }
                }
            }

            var pdl = propertyDescriptorList.FindAll(pd => pd != null);

            PreProcess(pdl);
            var pdcReturn = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());

            return pdcReturn;
        } 
        #endregion

        #region Private Methods
        private void PreProcess(List<CustomPropertyDescriptor> pdl)
        {
            if (propertySortOrder != CustomSortOrder.None && pdl.Count > 0)
            {
                var propSorter = new PropertySorter();
                propSorter.SortOrder = propertySortOrder;
                pdl.Sort(propSorter);
            }
            UpdateCategoryTabAppendCount();
            UpdateResourceManager();
        } 

        private void UpdateResourceManager()
        {
            foreach (var cpd in propertyDescriptorList)
            {
                IResourceAttribute attr = (PropertyResourceAttribute)cpd.AllAttributes.FirstOrDefault(a => a is PropertyResourceAttribute);
                if (attr == null)
                {
                    var ac = GetAttributes();
                    var al = new AttributeList(ac);
                    attr = (ClassResourceAttribute)al.FirstOrDefault(a => a is ClassResourceAttribute);
                }
                if (attr == null)
                {
                    cpd.ResourceManager = null;
                    continue;
                }
                cpd.KeyPrefix = attr.KeyPrefix;
                var rm = hashRM[attr] as ResourceManager;
                if (rm != null)
                {
                    cpd.ResourceManager = rm;
                    continue;
                }
                try
                {
                    if (String.IsNullOrWhiteSpace(attr.AssemblyFullName) == false)
                    {
                        rm = new ResourceManager(attr.BaseName, Assembly.ReflectionOnlyLoad(attr.AssemblyFullName));
                    }
                    else
                    {
                        rm = new ResourceManager(attr.BaseName, base.GetPropertyOwner(cpd).GetType().Assembly);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                hashRM.Add(attr, rm);
                cpd.ResourceManager = rm;
            }
        }

        private void UpdateCategoryTabAppendCount()
        {
            // get a copy of the list as we do not want to sort around the actual list
            var pdl = propertyDescriptorList.FindAll(pd => pd != null);
            if (pdl.Count == 0)
            {
                return;
            }
            var propSorter = new CategorySorter();

            var nTabCount = 0;

            switch (categorySortOrder)
            {
                case CustomSortOrder.AscendingById:
                    propSorter.SortOrder = CustomSortOrder.DescendingById;
                    pdl.Sort(propSorter);
                    nTabCount = 0;
                    var sortIndex = pdl[0].CategoryId;
                    foreach (var cpd in pdl)
                    {
                        if (cpd.CategoryId == sortIndex)
                        {
                            cpd.TabAppendCount = nTabCount;
                        }
                        else
                        {
                            sortIndex = cpd.CategoryId;
                            nTabCount++;
                            cpd.TabAppendCount = nTabCount;
                        }
                    }
                    break;
                case CustomSortOrder.None:
                case CustomSortOrder.AscendingByName:  // by default, property grid sorts the category ascendingly
                    foreach (var cpd in propertyDescriptorList)
                    {
                        cpd.TabAppendCount = 0;
                    }
                    break;
                case CustomSortOrder.DescendingById:
                    propSorter.SortOrder = CustomSortOrder.AscendingById;
                    pdl.Sort(propSorter);
                    nTabCount = 0;
                    int nCategorySortIndex = pdl[0].CategoryId;
                    foreach (var cpd in pdl)
                    {
                        if (nCategorySortIndex == cpd.CategoryId)
                        {
                            cpd.TabAppendCount = nTabCount;
                        }
                        else
                        {
                            nCategorySortIndex = cpd.CategoryId;
                            nTabCount++;
                            cpd.TabAppendCount = nTabCount;
                        }
                    }
                    break;
                case CustomSortOrder.DescendingByName:
                    propSorter.SortOrder = CustomSortOrder.AscendingByName;
                    pdl.Sort(propSorter);
                    nTabCount = 0;
                    pdl[0].TabAppendCount = 0;
                    var sCat = pdl[0].Category;
                    foreach (var cpd in pdl)
                    {
                        cpd.TabAppendCount = 0;
                        if (String.Compare(sCat, cpd.Category) == 0)
                        {
                            cpd.TabAppendCount = nTabCount;
                        }
                        else
                        {
                            sCat = cpd.Category;
                            nTabCount++;
                            cpd.TabAppendCount = nTabCount;
                        }
                    }
                    break;
            }
        }

        public ISite GetSite()
        {
            if (site == null)
            {
                var newSite = new SimpleSite();
                IPropertyValueUIService service = new PropertyValueUIService();
                service.AddPropertyValueUIHandler(new PropertyValueUIHandler(GenericPropertyValueUIHandler));
                newSite.AddService(service);
                site = newSite;
            }
            return site;
        }

        private void GenericPropertyValueUIHandler(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList itemList)
        {
            var cpd = propDesc as CustomPropertyDescriptor;
            if (cpd != null &&
                cpd.StateItems != null)
            {
                itemList.AddRange(cpd.StateItems as ICollection);
            }
        }
        #endregion
    }
}