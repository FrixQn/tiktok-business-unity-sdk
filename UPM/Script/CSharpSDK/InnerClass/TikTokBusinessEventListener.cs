using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using SDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TikTokBusinessEventListener : MonoBehaviour
{
    //缓存一下反射的FieldInfo，可以提高性能
    private FieldInfo _fieldInfo;
    private Transform _baseTransform;
    private float _startTime;
    private float _endTime;
    private Transform _currentClickTransform;
    private Dictionary<string, object> _clickInfos;
    
    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPointerUp())
        {
            if (_clickInfos != null)
            {
                _endTime = Time.time;
                var clickDuration = (long)((_endTime - _startTime) * 1000);
                _clickInfos.TryAdd("click_duration", clickDuration);
                _clickInfos.TryAdd("platform", "unity");
                _clickInfos.TryAdd("monitor_type", "enhanced_data_postback");
                TikTokBusinessSDK.TrackTTEvent(new TikTokBaseEvent("click", _clickInfos, ""));
                TikTokLogger.Verbose("Unity edp click");
            }
        }

        if (IsPointerDown())
        {
            if (!TikTokInnerManager.Instance().ShouldReportClickEvent())
            {
                _clickInfos = null;
                return;
            }
            clearData();
            _startTime = Time.time;

            var eventSystem = EventSystem.current;
            if (eventSystem == null)
            {
                return;
            }

            GameObject go = null;
            Vector2 pointerPos = Vector2.zero;

#if ENABLE_INPUT_SYSTEM
            if (UnityEngine.InputSystem.Pointer.current != null)
            {
                pointerPos = UnityEngine.InputSystem.Pointer.current.position.ReadValue();

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                PointerEventData eventData = new PointerEventData(eventSystem)
                {
                    position = pointerPos
                };
                eventSystem.RaycastAll(eventData, raycastResults);
                if (raycastResults.Count > 0)
                    go = raycastResults[0].gameObject;
            }
#endif

            if (go == null)
            {
                pointerPos = Input.mousePosition;

                if (_fieldInfo == null)
                {
                    _fieldInfo = typeof(StandaloneInputModule).GetField("m_InputPointerEvent", BindingFlags.NonPublic | BindingFlags.Instance);
                }

                var pointerEventData = _fieldInfo?.GetValue(eventSystem.currentInputModule) as PointerEventData;
                if (pointerEventData != null)
                {
                    go = pointerEventData.pointerEnter;
                }
                else
                {
                    List<RaycastResult> raycastResults = new List<RaycastResult>();
                    PointerEventData eventData = new PointerEventData(eventSystem)
                    {
                        position = pointerPos
                    };
                    eventSystem.RaycastAll(eventData, raycastResults);
                    if (raycastResults.Count > 0)
                        go = raycastResults[0].gameObject;
                }
            }

            if (go == null)
            {
                return;
            }

            var goTransform = go.transform;
            if (TikTokInnerManager.Instance().IsButtonInBlackList(go.transform.name))
            {
                _clickInfos = null;
                return;
            }

            var deepCount = 0;
            _baseTransform = null;
            while (goTransform != null)
            {
                deepCount += 1;
                if (goTransform.parent == null)
                {
                    _baseTransform = goTransform;
                }
                goTransform = goTransform.parent;
            }

            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("class_name", go.transform.name);
            var result = CurrentUIInfo(_baseTransform, _baseTransform.name, 1, TikTokInnerManager.Instance().PageDeepCount());

            info.Add("page_components", result);
            info.Add("current_page_name", _baseTransform.name);
            info.Add("page_deep_count", deepCount);

            info.Add("click_position_x", pointerPos.x);
            info.Add("click_position_y", (Screen.height - pointerPos.y));

            if (go != null)
            {
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;
                var currentObject = go;
                RectTransform currentObjectTransform = currentObject.GetComponent<RectTransform>();
                if (currentObjectTransform == null) return;
                if (!(currentObjectTransform.rect.width < screenWidth ||
                      currentObjectTransform.rect.height < screenHeight)) return;

                if (currentObject.GetComponent<Text>() != null || currentObject.GetComponent<Image>() != null)
                {
                    var parentObject = currentObject.transform.parent.gameObject;
                    RectTransform parentObjectTransform = parentObject.GetComponent<RectTransform>();
                    if (currentObjectTransform != null)
                    {
                        if (parentObjectTransform.rect.width < screenWidth ||
                            parentObjectTransform.rect.height < screenHeight)
                        {
                            currentObject = parentObject;
                            currentObjectTransform = parentObjectTransform;
                        }
                    }
                }
                _currentClickTransform = go.transform;
                var clickComponent = CurrentUIInfo(currentObjectTransform, currentObjectTransform.name, 1, TikTokInnerManager.Instance().PageDeepCount());
                info.Add("click_component", clickComponent);
            }

            _clickInfos = info;
        }
    }

    private bool IsPointerUp()
    {
#if ENABLE_INPUT_SYSTEM
        if (UnityEngine.InputSystem.Touchscreen.current != null)
            if (UnityEngine.InputSystem.Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)

                return true;
        if (UnityEngine.InputSystem.Mouse.current != null)
            if (UnityEngine.InputSystem.Mouse.current.leftButton.wasReleasedThisFrame)
                return true;

        return false;
#else
    return Input.GetMouseButtonUp(0);
#endif
    }

    private bool IsPointerDown()
    {
#if ENABLE_INPUT_SYSTEM
        if (UnityEngine.InputSystem.Touchscreen.current != null)
            if (UnityEngine.InputSystem.Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                return true;

        if (UnityEngine.InputSystem.Mouse.current != null)
            if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
                return true;

        return false;
#else
    return Input.GetMouseButtonDown(0);
#endif
    }

    Dictionary<string,object> CurrentUIInfo(Transform currentTransform, string parentName, long currentLevel, long targetLevel)
    {
        Dictionary<string, object> info = new Dictionary<string, object>();
        info.Add("class_name",parentName);
        if (_currentClickTransform != null)
        {
            if (_currentClickTransform.gameObject == currentTransform.gameObject)
            {
                info.Add("is_click",1);
            }
            else
            {
                info.Add("is_click",0);
            }
        }
        
        List<object> childViews = new List<object>();
        foreach (Transform child in currentTransform)
        {
            if (child.gameObject.activeSelf && currentLevel < targetLevel)
            {
                var result = CurrentUIInfo(child, child.name,currentLevel+1,targetLevel);
                childViews.Add(result);
            }
        }
        RectTransform rectTransform = currentTransform.gameObject.GetComponent<RectTransform>();
        if (rectTransform)
        {
            
            CanvasScaler canvasScaler = currentTransform.gameObject.GetComponentInParent<CanvasScaler>();
            
            if (canvasScaler != null)
            {
                // 获取对象左上角相对于Canvas的位置
                Vector3 objectTopLeft = new Vector3(rectTransform.rect.xMin, rectTransform.rect.yMax,0);

                // 使用CanvasScaler进行转换
                Vector3 screenPosition;
                screenPosition = canvasScaler.transform.TransformPoint(objectTopLeft);

                info.Add("left",screenPosition.x);
                info.Add("top",(Screen.height - screenPosition.y));
                
                Rect rect  = rectTransform.rect;
                info.Add("height",rect.height);
                info.Add("width",rect.width);
            }
   
        }
        
        Text textComponent = currentTransform.gameObject.GetComponent<Text>();
        if(textComponent != null)
        {
            string textString = TikTokInnerManager.Instance().SensigFiltering(textComponent.text);
            info.Add("text",textString);
        }
        
        Image image = currentTransform.gameObject.GetComponent<Image>();
        if (image != null && image.sprite != null)
        {
            string resourceName = image.sprite.texture.name;
            info.Add("image_name",resourceName);
        }
        
        if (childViews.Count > 0)
        {
            info.Add("child_views",childViews);
        }
        return info;
    }
    
    private void OnActiveSceneChanged(Scene previousScene, Scene newScene)
    {
        if (!TikTokInnerManager.Instance().IsUnityEDPSceneTrackEnable()) return;
        Dictionary<string, object> sceneDictionary = new Dictionary<string, object>();
        // 获取当前场景的根对象
        if (previousScene != null && previousScene.IsValid())
        {
            GameObject[] rootObjects = previousScene.GetRootGameObjects();
            if (rootObjects.Length != 0)
            {
                List<Dictionary<string, object>> dictionaryArray = new List<Dictionary<string, object>>();
                // 遍历每个根对象
                foreach(GameObject rootObject in rootObjects)
                {
                    if (rootObject.activeSelf)
                    {
                        var scene = CurrentUIInfo(rootObject.transform, rootObject.name,0,TikTokInnerManager.Instance().PageDeepCount());
                        dictionaryArray.Add(scene);
                    }
                }
                Dictionary<string, object> sceneInfo = new Dictionary<string, object>();
                sceneInfo.Add("name",newScene.name);
                sceneInfo.Add("scene_info",dictionaryArray);
                sceneDictionary.Add("previous_scene",sceneInfo);
            }
        }

        if (newScene != null && newScene.IsValid())
        {
            // 获取当前场景的根对象
            GameObject[] rootObjects = newScene.GetRootGameObjects();
            if (rootObjects.Length != 0)
            {
                List<Dictionary<string, object>> dictionaryArray = new List<Dictionary<string, object>>();
                // 遍历每个根对象
                foreach(GameObject rootObject in rootObjects)
                {
                    if (rootObject.activeSelf)
                    {
                        var scene = CurrentUIInfo(rootObject.transform, rootObject.name,0,TikTokInnerManager.Instance().PageDeepCount());
                        dictionaryArray.Add(scene);
                    }
                }
                Dictionary<string, object> sceneInfo = new Dictionary<string, object>();
                sceneInfo.Add("name",newScene.name);
                sceneInfo.Add("scene_info",dictionaryArray);
                sceneDictionary.Add("current_scene",sceneInfo);
            }
        }
        
        sceneDictionary.Add("platform","unity");
        sceneDictionary.Add("monitor_type","enhanced_data_postback");
        TikTokBusinessSDK.TrackTTEvent(new TikTokBaseEvent("scene",sceneDictionary,""));
        TikTokLogger.Verbose("Unity edp scene");
    }
    
    void clearData()
    {
        _startTime = 0;
        _endTime = 0;
        _clickInfos = null;
        _baseTransform = null;
        _currentClickTransform = null;
    }
}