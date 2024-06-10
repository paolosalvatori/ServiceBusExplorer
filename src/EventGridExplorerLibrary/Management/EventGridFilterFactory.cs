// <copyright file="EventGridFactory.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace EventGridExplorerLibrary
{
    using global::Azure.ResourceManager.EventGrid.Models;
    using System.Collections.Generic;

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

        public void FilterSelection()
        {
            if (OperatorType.Equals("Boolean equals")) { AddFilterForBoolEquals(); };
            if (OperatorType.Equals("String is in")) { AddFilterForStringIn(); };
            if (OperatorType.Equals("String is not in")) { AddFilterForStringNotIn(); };
            if (OperatorType.Equals("String contains")) { AddFilterForStringContains(); };
            if (OperatorType.Equals("String does not contain")) { AddFilterForStringNotContains(); };
            if (OperatorType.Equals("String begins with")) { AddFilterForStringBeginsWith(); };
            if (OperatorType.Equals("String does not begin with")) { AddFilterForStringNotBeginsWith(); };
            if (OperatorType.Equals("String ends with")) { AddFilterForStringEndsWith(); };
            if (OperatorType.Equals("String does not end with")) { AddFilterForStringNotEndsWith(); };
            if (OperatorType.Equals("Number is less than")) { AddFilterForNumberLessThan(); };
            if (OperatorType.Equals("Number is greater than")) { AddFilterForNumberGreaterThan(); };
            if (OperatorType.Equals("Number is less than or equal to")) { AddFilterForNumberLessThanOrEquals(); };
            if (OperatorType.Equals("Number is greater than or equal to")) { AddFilterForNumberGreaterThanOrEquals(); };
            if (OperatorType.Equals("Number is in")) { AddFilterForNumberIn(); };
            if (OperatorType.Equals("Number is not in")) { AddFilterForNumberNotIn(); };
            if (OperatorType.Equals("Number is in range")) { AddFilterForNumberInRange(); };
            if (OperatorType.Equals("Number is not in range")) { AddFilterForNumberNotInRange(); };
            if (OperatorType.Equals("Is null or undefined")) { AddFilterForIsNullOrUndefined(); };
            if (OperatorType.Equals("Is not null")) { AddFilterForIsNotNull(); };
        }

        /// <inheritdoc/>
        public void AddFilterForBoolEquals()
        {
            var filter = new BoolEqualsFilter
            {
                Key = Key,
                Value = bool.Parse(Value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringIn()
        {
            var filter = new StringInFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringNotIn()
        {
            var filter = new StringNotInFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringContains()
        {
            var filter = new StringContainsFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringNotContains()
        {
            var filter = new StringNotContainsFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringBeginsWith()
        {
            var filter = new StringBeginsWithFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringNotBeginsWith()
        {
            var filter = new StringNotBeginsWithFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringEndsWith()
        {
            var filter = new StringEndsWithFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForStringNotEndsWith()
        {
            var filter = new StringNotEndsWithFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberLessThan()
        {
            var filter = new NumberLessThanFilter
            {
                Key = Key,
                Value = double.Parse(Value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberGreaterThan()
        {
            var filter = new NumberGreaterThanFilter
            {
                Key = Key,
                Value = double.Parse(Value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberLessThanOrEquals()
        {
            var filter = new NumberLessThanOrEqualsFilter
            {
                Key = Key,
                Value = double.Parse(Value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberGreaterThanOrEquals()
        {
            var filter = new NumberGreaterThanOrEqualsFilter
            {
                Key = Key,
                Value = double.Parse(Value)
            };

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberIn()
        {
            var filter = new NumberInFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(double.Parse(filterValue));
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberNotIn()
        {
            var filter = new NumberNotInFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(double.Parse(filterValue));
            }

            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForNumberNotInRange()
        {
            var filter = new NumberNotInRangeFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
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

        public void AddFilterForNumberInRange()
        {
            var filter = new NumberInRangeFilter { Key = Key };
            string[] filterValuesList = Value.Split(',');
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

        public void AddFilterForIsNullOrUndefined()
        {
            var filter = new IsNullOrUndefinedFilter { Key = Key };
            this.filtersConfiguration.Filters.Add(filter);
        }

        public void AddFilterForIsNotNull()
        {
            var filter = new IsNotNullFilter { Key = Key };
            this.filtersConfiguration.Filters.Add(filter);
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public string OperatorType { get; set; }
    }
}
