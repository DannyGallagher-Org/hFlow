using System;
using System.Linq;
using hFlow.CustomFlowTypes;
using hFlow.CustomFlowTypes.Debug;
using hFlow.Interfaces;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace hFlow
{
    public enum FlowInterstitialObjectTypes
    {
        DebugString,
        AsyncList,
        EmbedInterstitial,
        EnableDisableGameObject,
        Wait,
        FlowInstantiatePrefab,
        FlowCallUnityEvent,
        FlowInterstitialClean,
        FlowRemoveGameObject
    }

    [CustomEditor(typeof(FlowInterstitial))]
    public class FlowInterstitialEditor : Editor
    {
        private GUIStyle _elementStyle;
        private float _fifthSize;
        private int _index;
        private FlowInterstitial _target;

        private void OnEnable()
        {
            var defaultFont = CustomEditorStyles.DefaultFont;

            _elementStyle = new GUIStyle
            {
                font = defaultFont,
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                padding = new RectOffset(10, 10, 2, 5)
            };

            _target = (FlowInterstitial) target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Test"))
            {
                Debug.Log($"Invoked {_target.name}");
                _target.Invoke();
            }
            
            GUILayout.Space(20);
            _target.DebugFlowInterstitial = GUILayout.Toggle(_target.DebugFlowInterstitial, "Do debug on this thang?");
            _target.PlayOnAwake = GUILayout.Toggle(_target.PlayOnAwake, "Play On Awake");
            GUILayout.Space(10);

            var rect = EditorGUILayout.BeginVertical();

            if (rect.width > 0f)
                _fifthSize = rect.width * 0.2f;

            GUI.color = FlowEditorDefs.BackGroundBoxColor;

            GUI.Box(rect, GUIContent.none);

            EditorGUI.BeginChangeCheck();
            DrawAllPoints();
            if (EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
                EditorUtility.SetDirty(_target);
            }

            GUILayout.Space(20);

            DrawSelector();

            GUILayout.Space(5);

            DrawAddButton();

            GUILayout.Space(30);

            DrawGrabButton();
            
            GUILayout.Space(20);

            _target.CleanOnComplete = GUILayout.Toggle(_target.CleanOnComplete, "Clean On Complete");
            
            EditorGUILayout.EndVertical();
        }

        private void DrawGrabButton()
        {
            GUI.color = FlowEditorDefs.AddButtonColor;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(_fifthSize * 2f);

            if (GUILayout.Button("Grab Customs")) 
                GrabFlowPoints();

            GUILayout.Space(_fifthSize);
            EditorGUILayout.EndHorizontal();
        }

        private void GrabFlowPoints()
        {
            foreach (var flow in _target.GetComponents<AbstractFlowPoint>())
            {
                if (_target.FlowPoints.Contains(flow)) 
                    continue;
                
                var newElement = flow;
                Array.Resize(ref _target.FlowPoints, _target.FlowPoints.Length + 1);
                _target.FlowPoints[_target.FlowPoints.Length - 1] = newElement;
                EditorUtility.SetDirty(_target);
            }
        }

        private void DrawAddButton()
        {
            GUI.color = FlowEditorDefs.AddButtonColor;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(_fifthSize * 2f);

            if (GUILayout.Button("Add")) 
                AddFlowPoint();

            GUILayout.Space(_fifthSize);
            EditorGUILayout.EndHorizontal();
        }

        private void AddFlowPoint()
        {
            var option = (FlowInterstitialObjectTypes) _index;

            MonoBehaviour newElement;

            switch (option)
            {
                case FlowInterstitialObjectTypes.DebugString:
                    newElement = _target.gameObject.AddComponent<FlowDebugString>();
                    break;
                case FlowInterstitialObjectTypes.AsyncList:
                    newElement = _target.gameObject.AddComponent<FlowAsyncListOfPoints>();
                    break;
                case FlowInterstitialObjectTypes.EnableDisableGameObject:
                    newElement = _target.gameObject.AddComponent<FlowEnableDisableGameObject>();
                    break;
                case FlowInterstitialObjectTypes.EmbedInterstitial:
                    newElement = _target.gameObject.AddComponent<FlowEmbedInterstitial>();
                    break;
                case FlowInterstitialObjectTypes.Wait:
                    newElement = _target.gameObject.AddComponent<FlowWait>();
                    break;
                case FlowInterstitialObjectTypes.FlowInstantiatePrefab:
                    newElement = _target.gameObject.AddComponent<FlowInstantiatePrefab>();
                    break;
                case FlowInterstitialObjectTypes.FlowCallUnityEvent:
                    newElement = _target.gameObject.AddComponent<FlowCallUnityEvent>();
                    break;
                case FlowInterstitialObjectTypes.FlowInterstitialClean:
                    newElement = _target.gameObject.AddComponent<FlowInterstitialCleanUp>();
                    break;
                case FlowInterstitialObjectTypes.FlowRemoveGameObject:
                    newElement = _target.gameObject.AddComponent<FlowRemoveGameObject>();
                    break;
                default:
                    newElement = _target.gameObject.AddComponent<FlowDebugString>();
                    ((FlowDebugString) newElement).DebugString = "Broke to default somehow, ask Danny.";
                    break;
            }

            Array.Resize(ref _target.FlowPoints, _target.FlowPoints.Length + 1);
            _target.FlowPoints[_target.FlowPoints.Length - 1] = newElement;
            EditorUtility.SetDirty(_target);
        }

        private void DrawSelector()
        {
            GUI.color = Color.gray;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(_fifthSize * 2f);
            _index = EditorGUILayout.Popup(_index, Enum.GetNames(typeof(FlowInterstitialObjectTypes)));
            GUILayout.Space(_fifthSize);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawAllPoints()
        {
            var count = 0;

            foreach (var flowPoint in _target.FlowPoints)
            {
                if (flowPoint == null)
                {
                    _target.FlowPoints = _target.FlowPoints.RemoveAt(count);
                    EditorUtility.SetDirty(_target);
                    return;
                }

                var fpRect = EditorGUILayout.BeginHorizontal();

                GUI.color = FlowEditorDefs.ElementColor;
                GUI.Box(fpRect, GUIContent.none, _elementStyle);

                (flowPoint as IFlowPoint)?.DrawEditorView();

                GUI.color = FlowEditorDefs.UpDownButtonColor;

                if (count != 0)
                    if (GUILayout.Button("▲", GUILayout.Height(20), GUILayout.Width(20)))
                    {
                        _target.FlowPoints.Swap(count, count - 1);
                        EditorUtility.SetDirty(_target);
                        break;
                    }

                if (count != _target.FlowPoints.Length - 1)
                {
                    if (GUILayout.Button("▼", GUILayout.Height(20), GUILayout.Width(20)))
                    {
                        _target.FlowPoints.Swap(count, count + 1);
                        EditorUtility.SetDirty(_target);
                        break;
                    }
                }
                else
                {
                    GUILayout.Space(24);
                }

                GUILayout.Space(5);

                ((AbstractFlowPoint) flowPoint).Muted =
                    EditorGUILayout.Toggle(((AbstractFlowPoint) flowPoint).Muted, GUILayout.Width(20));

                GUILayout.Space(5);

                GUI.color = FlowEditorDefs.RemButtonColor;
                if (GUILayout.Button("Rem", GUILayout.Height(20), GUILayout.Width(50)))
                {
                    DestroyImmediate(_target.FlowPoints[count]);
                    _target.FlowPoints = _target.FlowPoints.RemoveAt(count);
                    EditorUtility.SetDirty(_target);
                    return;
                }

                EditorGUILayout.EndHorizontal();
                count++;
            }
        }
    }
}