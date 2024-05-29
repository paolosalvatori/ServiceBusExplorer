// <copyright file="EventGridFactory.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace EventGridExplorerLibrary
{
    using System.Collections.Generic;
    using global::Azure.ResourceManager.EventGrid.Models;

    /// <summary>
    /// Implementation of the EventGridFilterFactory class
    /// This class contains methods that creates a filter for each available filter types and adds them to their respective filter configurations.
    /// </summary>
    public class EventGridFilterFactory
    {
        private FiltersConfiguration filtersConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventGridFilterFactory"/> class.
        /// </summary>
        public EventGridFilterFactory(FiltersConfiguration filtersConfiguration)
        {
            this.filtersConfiguration = filtersConfiguration;
        }

        /// <inheritdoc/>
        public void AddFilterForBoolEquals(string key, string value)
        {
            var filter = new BoolEqualsFilter 
            { 
                Key = key, 
                Value = bool.Parse(value) 
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringIn(string key, string value)
        {
            var filter = new StringInFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringNotIn(string key, string value)
        {
            var filter = new StringNotInFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringContains(string key, string value)
        {
            var filter = new StringContainsFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
         }

        public void AddFilterForStringNotContains(string key, string value)
        {
            var filter = new StringNotContainsFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
          }

        public void AddFilterForStringBeginsWith(string key, string value)
        {
            var filter = new StringBeginsWithFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
         }

        public void AddFilterForStringNotBeginsWith(string key, string value)
        {
            var filter = new StringNotBeginsWithFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }  

        public void AddFilterForStringEndsWith(string key, string value)
        {
            var filter = new StringEndsWithFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringNotEndsWith(string key, string value)
        {
            var filter = new StringNotEndsWithFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberLessThan(string key, string value)
        {
            var filter = new NumberLessThanFilter 
            {
                Key = key,
                Value = double.Parse(value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberGreaterThan(string key, string value)
        {
            var filter = new NumberGreaterThanFilter 
            {
                Key = key,
                Value = double.Parse(value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberLessThanOrEquals(string key, string value)
        {
            var filter = new NumberLessThanOrEqualsFilter
            {
                Key = key,
                Value = double.Parse(value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberGreaterThanOrEquals(string key, string value)
        {
            var filter = new NumberGreaterThanOrEqualsFilter 
            {
                Key = key,
                Value = double.Parse(value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberIn(string key, string value)
        {
            var filter = new NumberInFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(double.Parse(filterValue));
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberNotIn(string key, string value)
        {
            var filter = new NumberNotInFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(double.Parse(filterValue));
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberNotInRange(string key, string value)
        {
            var filter = new NumberNotInRangeFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                List<double> doubleRangeValues = new List<double>();
                var rangeValues = filterValue.Split('-');
                foreach (string rangeValue in rangeValues)
                {
                    doubleRangeValues.Add(double.Parse(rangeValue));
                }
                filter.Values.Add(doubleRangeValues);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberInRange(string key, string value)
        {
            var filter = new NumberInRangeFilter { Key = key };
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                List<double> doubleRangeValues = new List<double>();
                var rangeValues = filterValue.Split('-');
                foreach (string rangeValue in rangeValues)
                {
                    doubleRangeValues.Add(double.Parse(rangeValue));
                }
                filter.Values.Add(doubleRangeValues);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForIsNullOrUndefined(string key)
        {
            var filter = new IsNullOrUndefinedFilter { Key = key };
            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForIsNotNull(string key)
        {
            var filter = new IsNotNullFilter { Key = key };
            this.filtersConfiguration.Filters.Add(filter);
        }

    }
}
