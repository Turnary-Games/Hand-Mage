using UnityEngine;
using System.Collections;

namespace ExtensionMethods {
	public static class LayerMaskExtensions {
		public static bool IsInLayerMask(this LayerMask mask, int layer) {
			return ((mask.value & (1 << layer)) > 0);
		}
		
		public static bool IsInLayerMask(this LayerMask mask, GameObject obj) {
			return ((mask.value & (1 << obj.layer)) > 0);
		}
	}
}