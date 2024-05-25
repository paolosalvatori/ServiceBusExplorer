// <copyright file="EventGridClient.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace EventGridExplorerLibrary
{
    using System.Collections.Generic;
    using global::Azure.ResourceManager.EventGrid.Models;

    /// <summary>
    /// Implementation of the EventGridFilterValues class
    /// </summary>
    public class EventGridFilterValues
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EventGridFilterValues"/> class.
        /// </summary>
        public EventGridFilterValues()
        {
        }

        /// <inheritdoc/>
        public void GetValueForBoolEqualsFilter(BoolEqualsFilter filter, string value)
        {
            filter.Value = bool.Parse(value);
        }

        public void GetValueForStringInFilter(StringInFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }

        public void GetValueForStringNotInFilter(StringNotInFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }

        public void GetValueForStringContainsFilter(StringContainsFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }

        public void GetValueForStringNotContainsFilter(StringNotContainsFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }
        public void GetValueForStringBeginsWithFilter(StringBeginsWithFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }
        public void GetValueForStringNotBeginsWithFilter(StringNotBeginsWithFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }
        public void GetValueForStringEndsWithFilter(StringEndsWithFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }
        public void GetValueForStringNotEndsWithFilter(StringNotEndsWithFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(filterValue);
            }
        }
        public void GetValueForNumberLessThanFilter(NumberLessThanFilter filter, string value)
        {
            filter.Value = double.Parse(value);
        }
        public void GetValueForNumberGreaterThanFilter(NumberGreaterThanFilter filter, string value)
        {
            filter.Value = double.Parse(value);
        }
        public void GetValueForNumberLessThanOrEqualsFilter(NumberLessThanOrEqualsFilter filter, string value)
        {
            filter.Value = double.Parse(value);
        }
        public void GetValueForNumberGreaterThanOrEqualsFilter(NumberGreaterThanOrEqualsFilter filter, string value)
        {
            filter.Value = double.Parse(value);
        }
        public void GetValueForNumberInFilter(NumberInFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(double.Parse(filterValue));
            }
        }
        public void GetValueForNumberNotInFilter(NumberNotInFilter filter, string value)
        {
            string[] filterValuesList = value.Split(',');
            foreach (string filterValue in filterValuesList)
            {
                filter.Values.Add(double.Parse(filterValue));
            }
        }

        public void GetValueForNumberNotInRangeFilter(NumberNotInRangeFilter filter, string value)
        {
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
        }
        public void GetValueForNumberInRangeFilter(NumberInRangeFilter filter, string value)
        {
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

        }

    }
}
