#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPackages.UI {
	public class UICanvasVisualizer : MonoBehaviour {

		public Color inactiveColor = new Color (1, 1, 1, .5f);
		public Color selectionColor = new Color (1, 1, 0);
		public Color childColor = new Color (1, 0, 1);
		public Color raycastColor = new Color (1, 0, 0, 1);

		private void OnDrawGizmos () {
			var _selectionIsRectTransform = Selection.activeGameObject == null ?
				false : Selection.activeGameObject.GetComponent<RectTransform> () != null;
			var _rectTransforms = this.GetComponentsInChildren<RectTransform> ();

			foreach (var _rectTransform in _rectTransforms) {
				this.DrawRectTransform (_rectTransform, this.inactiveColor);

				var _graphic = _rectTransform.GetComponent<Graphic> ();
				if (_graphic != null &&
					_graphic.raycastTarget == true) {
					var _lightRaycastColor = this.raycastColor;
					_lightRaycastColor.a = .5f;
					this.DrawRectTransform (_rectTransform, _lightRaycastColor, true);
					this.DrawRectTransform (_rectTransform, this.raycastColor);
				}
			}

			if (_selectionIsRectTransform == true) {
				var _selectionRectTransformChildren =
					Selection.activeGameObject.GetComponentsInChildren<RectTransform> ();
				foreach (var _rectTransform in _selectionRectTransformChildren)
					this.DrawRectTransform (_rectTransform, this.childColor);
				this.DrawRectTransform (
					Selection.activeGameObject.GetComponent<RectTransform> (),
					this.selectionColor);
			}
		}

		private void DrawRectTransform (RectTransform rectTransform, Color color, bool fill = false) {
			var _corners = new Vector3[4];
			Gizmos.color = color;
			rectTransform.GetWorldCorners (_corners);
			if (fill == false)
				for (var _i = 0; _i < 4; _i++)
					Gizmos.DrawLine (
						_corners[_i],
						_corners[_i == 3 ? 0 : _i + 1]);
			else
				Gizmos.DrawCube (
					new Vector3 (
						_corners[0].x + ((_corners[2].x - _corners[0].x) / 2f),
						_corners[0].y + ((_corners[1].y - _corners[0].y) / 2f)
					),
					new Vector3 (
						_corners[0].x - _corners[2].x,
						_corners[0].y - _corners[1].y
					));
		}
	}
}
#endif