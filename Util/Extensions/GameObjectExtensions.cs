using System.Collections;
using UnityEngine;

namespace DT {
  public static class GameObjectExtensions {
    public static string FullName(this GameObject g) {
      string name = g.name;
      while (g.transform.parent != null) {
        g = g.transform.parent.gameObject;
        name = g.name + "/" + name;
      }
      return name;
    }
    
    public static bool IsInLayerMask(this GameObject g, LayerMask mask) {
      return ((mask.value & (1 << g.layer)) > 0);
    }
  }
}
