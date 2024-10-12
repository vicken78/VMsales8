using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VMSales8.Models;

namespace VMSales8.Logic
{
    public class DataProcessor<T> where T : BaseModel // Constrain T to inherit BaseViewModel
    {


        // Helper method to get the list of properties (including primary and foreign keys) that changed between two items
        private List<PropertyInfo> GetChangedPropertiesIncludingKeys(T cleanItem, T dirtyItem, PropertyInfo[] primaryKeys)
        {
            var changedProperties = new List<PropertyInfo>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value1 = property.GetValue(cleanItem);
                var value2 = property.GetValue(dirtyItem);

                // Skip non-relevant properties (like IsSelected, etc.)
                if (property.Name.Equals("IsSelected", StringComparison.OrdinalIgnoreCase) ||
                    property.Name.Equals("new_supplier_name", StringComparison.OrdinalIgnoreCase) ||
                    property.Name.Equals("total_lot", StringComparison.OrdinalIgnoreCase) ||
                    property.Name.Equals("total_shipping", StringComparison.OrdinalIgnoreCase) ||
                    property.Name.Equals("total_sales_tax", StringComparison.OrdinalIgnoreCase) ||
                    property.Name.Equals("Action", StringComparison.OrdinalIgnoreCase) ||
                    property.Name.Equals("Column", StringComparison.OrdinalIgnoreCase))

                {
                    continue;
                }

                // Compare property values
                if (!Equals(value1, value2))
                {
                    // Always track changes in primary and foreign key properties
                    if (primaryKeys.Contains(property) || IsForeignKey(property))
                    {
                        changedProperties.Add(property); // Track key changes
                        Debug.WriteLine($"Key/Foreign key '{property.Name}' changed.");
                    }
                    else if (!primaryKeys.Contains(property))
                    {
                        changedProperties.Add(property); // Track non-key changes
                        Debug.WriteLine($"Property '{property.Name}' changed.");
                    }
                }
            }
            return changedProperties;
        }



        private bool ArePrimaryKeysEqual(T item1, T item2, PropertyInfo[] primaryKeys)
        {
            if (primaryKeys == null || primaryKeys.Length == 0)
                throw new InvalidOperationException("Primary key properties not found.");

            foreach (var primaryKey in primaryKeys)
            {
                // Find the matching primary key in item2 by name (this allows for different order)
                var matchingKey = typeof(T).GetProperty(primaryKey.Name);
                if (matchingKey == null)
                {
                    // If no matching key is found, skip the comparison for this key
                    //Debug.WriteLine($"No matching primary key found for '{primaryKey.Name}' in the second item.");
                    continue;
                }


                var key1 = primaryKey.GetValue(item1);
                var key2 = matchingKey.GetValue(item2);

                // Log the primary key names and their values for comparison
                //Debug.WriteLine($"Comparing Primary Key: {primaryKey.Name}, Value1: {key1}, Value2: {key2}");

                // If any primary key doesn't match, return false
                if (key1 == null || !key1.Equals(key2))
                {
                    Debug.WriteLine($"Primary Key '{primaryKey.Name}' does not match: Value1 = {key1}, Value2 = {key2}");
                    return false;
                }
            }

            //Debug.WriteLine("All matching primary keys compare equal.");
            return true;
        }

        private PropertyInfo[] GetPrimaryKeyProperties()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Filter properties that end with '_pk' to identify them as primary keys
            var primaryKeys = properties
                .Where(p => p.Name.EndsWith("_pk", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            // Debug log the selected primary keys
            Debug.WriteLine("Selected Primary Keys:");
            foreach (var key in primaryKeys)
            {
                Debug.WriteLine($"Primary Key found: {key.Name}");
            }

            if (primaryKeys.Length == 0)
                throw new InvalidOperationException("Primary key properties not found.");

            return primaryKeys;
        }
        private object GetPrimaryKeyValue(T item, PropertyInfo[] primaryKeys)
        {
            if (primaryKeys.Length > 0)
            {
                return primaryKeys[0].GetValue(item); // Assuming a single primary key for simplicity.
            }
            return null;
        }

        // Compare method that returns ObservableCollection<T>
        public ObservableCollection<T> Compare(
            ObservableCollection<T> observableCollectionClean,
            ObservableCollection<T> observableCollectionDirty)
        {
            var differences = new ObservableCollection<T>();

            if (observableCollectionClean == null || observableCollectionDirty == null)
                throw new ArgumentNullException("Both collections must be non-null.");

            // Get primary key properties (supporting 1 or 2 primary keys)

            var primaryKeys = GetPrimaryKeyProperties();
            var collectionListClean = observableCollectionClean.OrderBy(x => GetPrimaryKeyValue(x, primaryKeys)).ToList();
            var collectionListDirty = observableCollectionDirty.OrderBy(x => GetPrimaryKeyValue(x, primaryKeys)).ToList();

            var matchingItems = collectionListClean
      .Join(
          collectionListDirty,
          cleanItem => GetPrimaryKeyValue(cleanItem, primaryKeys),  // Get key from clean
          dirtyItem => GetPrimaryKeyValue(dirtyItem, primaryKeys),  // Get key from dirty
          (cleanItem, dirtyItem) => new { CleanItem = cleanItem, DirtyItem = dirtyItem } // Join clean and dirty
      ).ToList();

            foreach (var match in matchingItems)
            {

                var propertyDifferences = GetChangedPropertiesIncludingKeys(match.CleanItem, match.DirtyItem, primaryKeys);
                if (propertyDifferences.Any())
                {
                    // Mark the entity as updated
                    // Log or collect the changed property/entity names
                    foreach (var property in propertyDifferences)
                    {
                        Debug.WriteLine(property.Name);
                        Debug.WriteLine(property);
                        if (property.Name != null)
                            match.DirtyItem.Action = "Update";
                    }

                    // Add the dirty item to differences collection
                    differences.Add(match.DirtyItem);
                }
            }

            // Handle added items (Inserts)
            var addedItems = collectionListDirty.Except(collectionListClean, new ObjectPrimaryKeyComparer(this, primaryKeys)).ToList();

            if (addedItems.Any())
            {

                foreach (var item in addedItems)
                {
                    //item.Action = "Insert";  // Mark as Insert
                    differences.Add(item);

                }
            }

            // Handle removed items (Deletes)
            var removedItems = collectionListClean.Except(collectionListDirty, new ObjectPrimaryKeyComparer(this, primaryKeys)).ToList();
            foreach (var match in matchingItems)
            {

                var propertyDifferencesdelete = GetChangedPropertiesIncludingKeys(match.CleanItem, match.DirtyItem, primaryKeys);

                if (removedItems.Any())
                {
                    foreach (var item in removedItems)
                    {
                        //item.Action = "Delete";  // Mark as Delete
                        differences.Add(item);
                    }
                }
            }
            return differences;
        }

        // Helper method to identify foreign keys (e.g., based on naming convention or attributes)
        private bool IsForeignKey(PropertyInfo property)
        {
            // Example: Check if the property name ends with "_fk" to identify it as a foreign key.
            return property.Name.EndsWith("_fk", StringComparison.OrdinalIgnoreCase);
        }


        // Nested class for comparing primary keys (updated for multiple keys)
        private class ObjectPrimaryKeyComparer : IEqualityComparer<T>
        {
            private readonly DataProcessor<T> _dataProcessor;
            private readonly PropertyInfo[] _primaryKeys;

            public ObjectPrimaryKeyComparer(DataProcessor<T> dataProcessor, PropertyInfo[] primaryKeys)
            {
                _dataProcessor = dataProcessor;
                _primaryKeys = primaryKeys;
            }

            public bool Equals(T x, T y)
            {
                return _dataProcessor.ArePrimaryKeysEqual(x, y, _primaryKeys);
            }

            public int GetHashCode(T obj)
            {
                if (_primaryKeys == null || _primaryKeys.Length == 0)
                    throw new InvalidOperationException("Primary key properties not found.");

                int hashCode = 17;
                foreach (var primaryKey in _primaryKeys)
                {
                    var key = primaryKey.GetValue(obj);
                    hashCode = hashCode * 23 + (key != null ? key.GetHashCode() : 0);
                }
                return hashCode;
            }

        }
        private bool AreObjectsEqual(T item1, T item2)
        {
            // Check if both items are the same reference
            if (ReferenceEquals(item1, item2))
                return true;

            // Check if either item is null
            if (item1 == null || item2 == null)
                return false;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var value1 = property.GetValue(item1);
                var value2 = property.GetValue(item2);

                if (property.Name.Equals("IsSelected", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Skip this property
                }
                if (property.Name.Equals("Action", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Skip this property
                }

                // Log or Debug the property names and values
                //Debug.WriteLine($"Comparing Property: {property.Name}, Value1: {value1}, Value2: {value2}");

                if (!Equals(value1, value2))
                {
                    // for testing
                    //Debug.WriteLine($"Property '{property.Name}' differs. Objects are not equal.");
                    return false;  // Property value differs, objects are not equal
                }
            }

            // If all properties are equal
            return true;
        }
    }
}
