using UnityEngine;
using UnityEngine.UIElements;

namespace CodeReverie
{
    public class BlackboardFieldPropertyViewBAK : VisualElement
    {
        public BlackboardFieldPropertyViewBAK(string propertyType)
        {
            // Add different UI elements based on the property type.
            Label propertyTypeLabel = new Label($"Type: {propertyType}");
            Add(propertyTypeLabel);

            TextField propertyNameField = new TextField("Property Name")
            {
                value = $"New {propertyType} Property"
            };
            propertyNameField.RegisterValueChangedCallback(evt => Debug.Log($"Property name changed to: {evt.newValue}"));

            // Add fields to the property view.
            Add(propertyNameField);
        }
    }
}