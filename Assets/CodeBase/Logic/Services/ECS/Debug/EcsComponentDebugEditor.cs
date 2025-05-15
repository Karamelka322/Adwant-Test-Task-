#if UNITY_EDITOR

using System.Collections.Generic;
using System.Reflection;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using Sisus.ComponentNames;
using UniRx;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Services.ECS.Debug
{
    /// <summary>
    /// Для отрисовки ECS структуры в компоненьте EcsComponentDebug
    /// </summary>
    [CustomEditor(typeof(EcsComponentDebug))]
    public class EcsComponentDebugEditor : Editor
    {
        private EcsComponentDebug _debug;
        
        private PropertyInfo[] _properties;
        private FieldInfo[] _fields;
        
        private object Component => _debug.Pool.GetRaw(_debug.Entity);
        
        private void Awake()
        {
            _debug = target as EcsComponentDebug;
            
            if(_debug.Pool.Has(_debug.Entity))
            {
                _properties = Component.GetType().GetProperties();
                _fields = Component.GetType().GetFields();
            }
        }
        
        public override void OnInspectorGUI()
        {
            if (_debug.Pool.Has(_debug.Entity) == false)
            {
                return;
            }

            _debug.SetName(_debug.Pool.GetComponentType().Name, false);

            foreach (var field in _fields)
            {
                GUILayout.BeginHorizontal();

                DrawFiled(field.Name, field.GetValue(Component));
                
                GUILayout.EndHorizontal();  
            }

            foreach (var property in _properties)
            {
                GUILayout.BeginHorizontal();

                DrawFiled(property.Name, property.GetValue(Component));
                
                GUILayout.EndHorizontal();   
            }
        }

        private void DrawFiled(string label, object value)
        {
            switch (value)
            {
                case Object: DrawObjectField(label, value); break;
                case bool: DrawBoolField(label, value); break;
                case float: DrawFloatField(label, value); break;
                case int: DrawIntField(label, value); break;
                case string: DrawStringField(label, value); break;
                case Vector2: DrawVector2Field(label, value); break;
                case Vector3: DrawVector3Field(label, value); break;
                case Quaternion: DrawQuaternionField(label, value); break;
                case Color: DrawColorField(label, value); break;
                case Gradient: DrawGradientField(label, value); break;
                case Rect: DrawRectField(label, value); break;
                case AnimationCurve: DrawAnimationCurveField(label, value); break;
                
                case Object[]: DrawArray<Object>(label, value); break;
                case bool[]: DrawArray<bool>(label, value); break;
                case float[]: DrawArray<float>(label, value); break;
                case int[]: DrawArray<int>(label, value); break;
                case string[]: DrawArray<string>(label, value); break;
                case Vector2[]: DrawArray<Vector2>(label, value); break;
                case Vector3[]: DrawArray<Vector3>(label, value); break;
                case Quaternion[]: DrawArray<Quaternion>(label, value); break;
                case Color[]: DrawArray<Color>(label, value); break;
                case Gradient[]: DrawArray<Gradient>(label, value); break;
                case Rect[]: DrawArray<Rect>(label, value); break;
                
                case List<Object>: DrawList<Object>(label, value); break;
                case List<bool>: DrawList<bool>(label, value); break;
                case List<float>: DrawList<float>(label, value); break;
                case List<int>: DrawList<int>(label, value); break;
                case List<string>: DrawList<string>(label, value); break;
                case List<Vector2>: DrawList<Vector2>(label, value); break;
                case List<Vector3>: DrawList<Vector3>(label, value); break;
                case List<Quaternion>: DrawList<Quaternion>(label, value); break;
                case List<Color>: DrawList<Color>(label, value); break;
                case List<Gradient>: DrawList<Gradient>(label, value); break;
                case List<Rect>: DrawList<Rect>(label, value); break;
                
                case LinkedList<Object>: DrawLinkedList<Object>(label, value); break;
                case LinkedList<bool>: DrawLinkedList<bool>(label, value); break;
                case LinkedList<float>: DrawLinkedList<float>(label, value); break;
                case LinkedList<int>: DrawLinkedList<int>(label, value); break;
                case LinkedList<string>: DrawLinkedList<string>(label, value); break;
                case LinkedList<Vector2>: DrawLinkedList<Vector2>(label, value); break;
                case LinkedList<Vector3>: DrawLinkedList<Vector3>(label, value); break;
                case LinkedList<Quaternion>: DrawLinkedList<Quaternion>(label, value); break;
                case LinkedList<Color>: DrawLinkedList<Color>(label, value); break;
                case LinkedList<Gradient>: DrawLinkedList<Gradient>(label, value); break;
                case LinkedList<Rect>: DrawLinkedList<Rect>(label, value); break;
                
                case IntReactiveProperty: DrawIntReactivePropertyField(label, value); break;
                case FloatReactiveProperty: DrawFloatReactivePropertyField(label, value); break;
            }
        }
        
        private void DrawArray<T>(string label, object value)
        {
            if (value is T[] array)
            {
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent($"{label} (Length - {array.Length})"), GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();
                    
                for (int i = 0; i < array.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                        
                    DrawFiled($"Index {i}", array[i]);
                        
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
            }
        }
        
        private void DrawList<T>(string label, object value)
        {
            if (value is List<T> list)
            {
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent($"{label} (Count - {list.Count})"), GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();
                    
                for (int i = 0; i < list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                        
                    DrawFiled($"Index {i}", list[i]);
                        
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
            }
        }
        
        private void DrawLinkedList<T>(string label, object value)
        {
            if (value is LinkedList<T> linkedList)
            {
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent($"{label} (Count - {linkedList.Count})"), GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();

                var node = linkedList.First;
                var count = 0;
                
                while (node != null)
                {
                    EditorGUILayout.BeginHorizontal();
                        
                    DrawFiled($"Element {count}", node.Value);
                        
                    EditorGUILayout.EndHorizontal();

                    node = node.Next;
                    count++;
                }
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
            }
        }

        private static void DrawObjectField(string label, object value)
        {
            if (value is Object obj)
            {
                EditorGUILayout.LabelField(label, GetLayoutOption());
                EditorGUILayout.ObjectField(obj, value.GetType(), true);
            }
        }

        private static void DrawBoolField(string label, object value)
        {
            if (value is bool boolean)
            {
                EditorGUILayout.Toggle(label, boolean);
            }
        }

        private static void DrawFloatField(string label, object value)
        {
            if (value is float floating)
            {
                EditorGUILayout.FloatField(label, floating);
            }
        }
        
        private static void DrawIntField(string label, object value)
        {
            if (value is int integer)
            {
                EditorGUILayout.IntField(label, integer);
            }
        }
        
        private static void DrawStringField(string label, object value)
        {
            if (value is string text)
            {
                EditorGUILayout.TextField(label, text);
            }
        }
        
        private static void DrawVector2Field(string label, object value)
        {
            if (value is Vector2 vector)
            {
                EditorGUILayout.Vector2Field(label, vector);
            }
        }
        
        private static void DrawVector3Field(string label, object value)
        {
            if (value is Vector3 vector)
            {
                EditorGUILayout.Vector3Field(label, vector);
            }
        }
        
        private static void DrawQuaternionField(string label, object value)
        {
            if (value is Quaternion quaternion)
            {
                EditorGUILayout.Vector3Field(label, quaternion.eulerAngles);
            }
        }
        
        private static void DrawColorField(string label, object value)
        {
            if (value is Color color)
            {
                EditorGUILayout.ColorField(label, color);
            }
        }
        
        private static void DrawGradientField(string label, object value)
        {
            if (value is Gradient gradient)
            {
                EditorGUILayout.GradientField(label, gradient);
            }
        }
        
        private static void DrawRectField(string label, object value)
        {
            if (value is Rect gradient)
            {
                EditorGUILayout.RectField(label, gradient);
            }
        }
        
        private static void DrawAnimationCurveField(string label, object value)
        {
            if (value is AnimationCurve curve)
            {
                EditorGUILayout.CurveField(label, curve);
            }
        }
        
        private static GUILayoutOption GetLayoutOption()
        {
            return GUILayout.MaxWidth (EditorGUIUtility.labelWidth - 16);
        }
        
        private static void DrawIntReactivePropertyField(string label, object value)
        {
            if (value is IntReactiveProperty intReactiveProperty)
            {
                EditorGUILayout.IntField(label, intReactiveProperty.Value);
            }
        }
        
        private static void DrawFloatReactivePropertyField(string label, object value)
        {
            if (value is FloatReactiveProperty floatReactiveProperty)
            {
                EditorGUILayout.FloatField(label, floatReactiveProperty.Value);
            }
        }
    }
}

#endif