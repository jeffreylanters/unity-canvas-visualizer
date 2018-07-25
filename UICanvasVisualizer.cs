#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityPackages.UI {
	public class UICanvasVisualizer : MonoBehaviour {

		public Color inactiveColor = new Color (1, 1, 1, .5f);
		public Color selectionColor = new Color (1, 1, 0);
		public Color childColor = new Color (1, 0, 1);

		private void OnDrawGizmos () {
			var _selectionIsRectTransform = Selection.activeGameObject == null ?
				false : Selection.activeGameObject.GetComponent<RectTransform> () != null;
			var _selectionInstanceID = _selectionIsRectTransform == false ?
				-1 : Selection.activeGameObject.GetInstanceID ();
			var _rectTransforms = this.GetComponentsInChildren<RectTransform> ();

			foreach (var _rectTransform in _rectTransforms) {
				this.DrawRectTransform (_rectTransform, this.inactiveColor);
			}

			if (_selectionIsRectTransform == true) {
				var _selectionRectTransformChildren =
					Selection.activeGameObject.GetComponentsInChildren<RectTransform> ();
				foreach (var _rectTransform in _selectionRectTransformChildren)
					this.DrawRectTransform (_rectTransform, this.selectionColor);
				this.DrawRectTransform (
					Selection.activeGameObject.GetComponent<RectTransform> (),
					this.childColor);
			}
		}

		private void DrawRectTransform (RectTransform rectTransform, Color color) {
			var _corners = new Vector3[4];
			Gizmos.color = color;
			rectTransform.GetWorldCorners (_corners);
			for (var _i = 0; _i < 4; _i++)
				Gizmos.DrawLine (
					_corners[_i],
					_corners[_i == 3 ? 0 : _i + 1]);
		}
	}
}
#endif