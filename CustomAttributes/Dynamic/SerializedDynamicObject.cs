using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace DT {
  [Serializable]
  public class SerializedDynamicObject<T> where T : ScriptableObject {
    public string implementationTypeName;
    public string serializedDynamicObject;

    public T CreateDynamicObjectFromSerialized() {
      string[] implementationTypeNames = TypeUtil.GetImplementationTypeNames(typeof(T));

      int index = Array.IndexOf(implementationTypeNames, this.implementationTypeName);
      if (index == -1) {
        Debug.LogError("Failed to get DynamicObject from SerializedDynamicObject during runtime because type name is not found!");
        return null;
      }

      Type[] implementationTypes = TypeUtil.GetImplementationTypes(typeof(T));
      Type dynamicType = implementationTypes[index];
      T dynamicObject = (T)ScriptableObject.CreateInstance(dynamicType);
      JsonUtility.FromJsonOverwrite(this.serializedDynamicObject, dynamicObject);

      return dynamicObject;
    }

    public T DynamicObject {
      get {
        if (this._cachedDynamicObject == null) {
          this._cachedDynamicObject = this.CreateDynamicObjectFromSerialized();
        }

        return this._cachedDynamicObject;
      }
    }

    public SerializedDynamicObject() {}


    // PRAGMA MARK - Internal
    [NonSerialized] private T _cachedDynamicObject;

    [OnSerializing]
    private void SaveDynamicObjectBeforeSerializing(StreamingContext context) {
      this.serializedDynamicObject = JsonUtility.ToJson(this.DynamicObject);
    }
  }
}