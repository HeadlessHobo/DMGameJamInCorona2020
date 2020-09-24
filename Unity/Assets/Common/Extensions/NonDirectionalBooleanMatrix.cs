// (C) 2018 ERAL
// Distributed under the Boost Software License, Version 1.0.
// (See copy at http://www.boost.org/LICENSE_1_0.txt)

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer(typeof(NonDirectionalBooleanMatrixAttribute))]
public class NonDirectionalBooleanMatrixPropertyDrawer : PropertyDrawer {

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		var nonDirectionalBooleanMatrixAttribute = (NonDirectionalBooleanMatrixAttribute)attribute;

		if (property.propertyType != SerializedPropertyType.Generic) {
			return base.GetPropertyHeight(property, label);
		}

		var targetObject = GetCurrent(property) as NonDirectionalBooleanMatrix;
		if (targetObject == null) {
			return base.GetPropertyHeight(property, label);
		}

		if (!s_Foldout) {
			return base.GetPropertyHeight(property, label);
		}

		var basePropertyHeight = base.GetPropertyHeight(property, label);
		var result = basePropertyHeight;

		var labelMaxWidth = 0.0f;
		if (0 < targetObject.Length) {
			labelMaxWidth = Enumerable.Range(0, targetObject.Length)
										.Max(x=>EditorStyles.label.CalcSize(GetLabelContent(x)).x);
		}
		result += Mathf.Max(labelMaxWidth, basePropertyHeight);
		result += basePropertyHeight * targetObject.Length;
		return result;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var nonDirectionalBooleanMatrixAttribute = (NonDirectionalBooleanMatrixAttribute)attribute;

		if (property.propertyType != SerializedPropertyType.Generic) {
			EditorGUI.LabelField(position, label, kNotSupportTypeMessage);
			return;
		}

		var targetObject = GetCurrent(property) as NonDirectionalBooleanMatrix;
		if (targetObject == null) {
			EditorGUI.LabelField(position, label, kNotSupportTypeMessage);
			return;
		}

		using (new EditorGUI.PropertyScope(position, label, property)) {
			//ラベル名
			var foldoutPosition = position;
			foldoutPosition.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.BeginChangeCheck();
			var foldoutResult = EditorGUI.Foldout(foldoutPosition, s_Foldout, label);
			if (EditorGUI.EndChangeCheck()) {
				s_Foldout = foldoutResult;
			}
			if (!s_Foldout) {
				return;
			}

			position.y += EditorGUIUtility.singleLineHeight;

			//サイズフィールド
			var sizePosition = position;
			sizePosition.width = EditorGUIUtility.labelWidth - EditorGUIUtility.singleLineHeight;
			sizePosition.height = EditorGUIUtility.singleLineHeight;
			++EditorGUI.indentLevel;
			var oldLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(kSizeContent).x + EditorGUI.IndentedRect(new Rect(0.0f, 0.0f, EditorStyles.label.CalcSize(kSizeContent).x, 1.0f)).x;
			EditorGUI.BeginChangeCheck();
			var sizeResult = EditorGUI.IntField(sizePosition, kSizeContent, targetObject.Length);
			if (EditorGUI.EndChangeCheck() && (0 <= sizeResult)) {
				targetObject.Length = sizeResult;
			}
			EditorGUIUtility.labelWidth = oldLabelWidth;
			--EditorGUI.indentLevel;

			//縦書きラベル
			var labelMaxWidth = 0.0f;
			if (0 < targetObject.Length) {
				labelMaxWidth = Enumerable.Range(0, targetObject.Length)
											.Max(x=>EditorStyles.label.CalcSize(GetLabelContent(x)).x);
			}
			position.y += labelMaxWidth;

			var verticalLabelPosition = position;
			verticalLabelPosition.width = EditorGUIUtility.labelWidth - kLabelToggleSpacing;
			verticalLabelPosition.height = EditorGUIUtility.singleLineHeight;
			var OldGuiMatrix = GUI.matrix;
			{
				var rotateAroundPivot = new Vector2(position.xMin + EditorGUIUtility.labelWidth + EditorGUIUtility.singleLineHeight * targetObject.Length * 0.5f
												, position.yMin + EditorGUIUtility.singleLineHeight * (targetObject.Length * 0.5f));
				GUIUtility.RotateAroundPivot(90, rotateAroundPivot);
				for (var y = 0; y < targetObject.Length; ++y) {
					EditorGUI.LabelField(verticalLabelPosition, GetLabelContent(y), kRightAlignStyle);
					verticalLabelPosition.y += EditorGUIUtility.singleLineHeight;
				}
			}
			GUI.matrix = OldGuiMatrix;

			//横書きラベルとトグル
			var prefixLabelPosition = position;
			prefixLabelPosition.width = EditorGUIUtility.labelWidth - kLabelToggleSpacing;
			prefixLabelPosition.height = EditorGUIUtility.singleLineHeight;
			for (var y = 0; y < targetObject.Length; ++y) {
				EditorGUI.LabelField(prefixLabelPosition, GetLabelContent(y), kRightAlignStyle);

				var togglePosition = prefixLabelPosition;
				togglePosition.xMin = prefixLabelPosition.xMax + EditorGUIUtility.singleLineHeight * (targetObject.Length - 1 - y) + kLabelToggleSpacing;
				togglePosition.xMax = togglePosition.xMin + EditorGUIUtility.singleLineHeight;
				for (var x = 0; x < (targetObject.Length - y); ++x) {
					EditorGUI.BeginChangeCheck();
					var toggleResult = EditorGUI.Toggle(togglePosition, targetObject[x + y, y]);
					if (EditorGUI.EndChangeCheck()) {
						targetObject[x + y, y] = toggleResult;
					}
					togglePosition.x -= EditorGUIUtility.singleLineHeight;
				}
				prefixLabelPosition.y += EditorGUIUtility.singleLineHeight;
			}
		}
	}

	public override bool CanCacheInspectorGUI(SerializedProperty property) {
		return false;
	}

	private readonly GUIContent kNotSupportTypeMessage = new GUIContent("Use NonDirectionalBooleanMatrixPropertyDrawer with NonDirectionalBooleanMatrix.");
	private readonly GUIStyle kRightAlignStyle = new GUIStyle(){alignment = TextAnchor.MiddleRight};
	private readonly GUIContent kSizeContent = new GUIContent("Size");
	private const float kLabelToggleSpacing = 2.0f;

	private static bool s_Foldout = true;

	private List<GUIContent> m_LabelsContent = new List<GUIContent>();

	private static object GetCurrent(SerializedProperty property) {
		object result = property.serializedObject.targetObject;
		var propertyNames = property.propertyPath.Replace(".Array.data", ".").Split('.');
		foreach (var propertyName in propertyNames) {
			var parent = result;
			var indexerStart = propertyName.IndexOf('[');
			if (-1 == indexerStart) {
				const System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic;
				result = parent.GetType().GetField(propertyName, bindingFlags).GetValue(parent);
			} else if (parent.GetType().IsArray) {
				var indexerEnd = propertyName.IndexOf(']');
				var indexString = propertyName.Substring(indexerStart + 1, indexerEnd - indexerStart - 1);
				var index = int.Parse(indexString);
				var array = (System.Array)parent;
				if (index < array.Length) {
					result = array.GetValue(index);
				} else {
					result = null;
					break;
				}
			} else {
				throw new System.MissingFieldException();
			}
		}
		return result;
	}

	private GUIContent GetLabelContent(int index) {
		for (var i = m_LabelsContent.Count; i <= index; ++i) {
			m_LabelsContent.Add(null);
		}
		if (m_LabelsContent[index] == null) {
			var nonDirectionalBooleanMatrixAttribute = (NonDirectionalBooleanMatrixAttribute)attribute;
			string label;
			if (index < nonDirectionalBooleanMatrixAttribute.labels.Length) {
				label = nonDirectionalBooleanMatrixAttribute.labels[index];
			} else {
				label = index.ToString();
			}
			m_LabelsContent[index] = new GUIContent(label);
		}
		return m_LabelsContent[index];
	}
}
#endif

public class NonDirectionalBooleanMatrixAttribute : PropertyAttribute {
	public readonly string[] labels;

	public NonDirectionalBooleanMatrixAttribute() : this(new string[0]) {
	}

	public NonDirectionalBooleanMatrixAttribute(string[] labels) {
		this.labels = labels;
	}

	public NonDirectionalBooleanMatrixAttribute(System.Type enumType) : this(System.Enum.GetNames(enumType)) {
	}
}

[System.Serializable]
public class NonDirectionalBooleanMatrix {
	public bool this[int x, int y] {get{
		if ((x < 0) || (m_Length <= x)) throw new System.ArgumentOutOfRangeException("x");
		if ((y < 0) || (m_Length <= y)) throw new System.ArgumentOutOfRangeException("y");

		var index = GetArrayIndex(x, y, m_Length);
		return m_Data[index];
	}set{
		if ((x < 0) || (m_Length <= x)) throw new System.ArgumentOutOfRangeException("x");
		if ((y < 0) || (m_Length <= y)) throw new System.ArgumentOutOfRangeException("y");

		var index = GetArrayIndex(x, y, m_Length);
		m_Data[index] = value;
	}}

	public int Length {get{
		return m_Length;
	}set{
		if (value < 0) throw new System.ArgumentOutOfRangeException("Length");

		if (m_Data != null) {
			Resize(value, false, true);
		} else if (0 < value) {
			var arrayLength = GetArrayLength(value);
			m_Data = new bool[arrayLength];
			m_Length = value;
		}
	}}

	public void Resize(int newLength, bool valueDefault, bool valueKeep = true) {
		if (newLength < 0) throw new System.ArgumentOutOfRangeException("newLength");

		var arrayLength = GetArrayLength(newLength);
		if (m_Data.Length != arrayLength) {
			if (!valueKeep) {
				System.Array.Resize(ref m_Data, arrayLength);
			} else {
				var valueKeepLength = Mathf.Min(m_Length, newLength);
				if (m_Length < newLength) {
					//拡大
					System.Array.Resize(ref m_Data, arrayLength);
					for (var y = newLength - 1; 0 <= y; --y) {
						for (var x = newLength - 1; y <= x; --x) {
							var newIndex = GetArrayIndex(x, y, newLength);
							if ((valueKeepLength <= y) || (valueKeepLength <= x)) {
								m_Data[newIndex] = valueDefault;
							} else {
								var oldIndex = GetArrayIndex(x, y, m_Length);
								m_Data[newIndex] = m_Data[oldIndex];
							}
						}
					}
				} else {
					//縮小
					for (var y = 0; y < valueKeepLength; ++y) {
						for (var x = y; x < valueKeepLength; ++x) {
							var oldIndex = GetArrayIndex(x, y, m_Length);
							var newIndex = GetArrayIndex(x, y, newLength);
							m_Data[newIndex] = m_Data[oldIndex];
						}
					}
					System.Array.Resize(ref m_Data, arrayLength);
				}
			}
			m_Length = newLength;
		}
	}

	public NonDirectionalBooleanMatrix() {
	}

	public NonDirectionalBooleanMatrix(int length) {
		if (length < 0) throw new System.ArgumentOutOfRangeException("length");
		Length = length;
	}

	[SerializeField]
	private int m_Length = 0;

	[SerializeField]
	private bool[] m_Data = null;
	//m_Length:3の場合の配列図
	//  | 0   1   2 
	//--+-----------
	// 0|[0] [1] [2]
	// 1|    [3] [4]
	// 2|        [5]

	private static int GetArrayIndex(int x, int y, int length) {
		int min;
		int max;
		if (x <= y) {
			min = x;
			max = y;
		} else {
			min = y;
			max = x;
		}
		//元式: (length + 1) * min - GetArrayLength(min) + (max - min)
		//辺(length + 1),minの矩形からmin分の三角形を引くとminのオフセットが求まるので、後はmax分をオフセット
		var result = length * min - GetArrayLength(min) + max;
		return result;
	}

	private static int GetArrayLength(int length) {
		return length * (length + 1) / 2;
	}
}