using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

//Название не в честь Альтернатива Платформ, а
//просто как Альтернативный инспектор объектов.
using UnityEngine.Internal;

public class AlternativeInspector : EditorWindow
{
    #region поля класса

    
    /// <summary>
    /// Базовый объект инспектора который мы и будем отображать и редактировать
    /// </summary>
    private GameObject _GameObject;
    
    private Vector4 _rotationComponents = Vector4.zero;

    //Бок полей класса отвечающих за взаимодействие с GUI
    private bool _foldTitlebar;
    private bool _foldTitlebarUtil;


    //Блок глобальных констант отвечающих за отображение GUI
    /// <summary>
    /// Размер вертикального разделителя
    /// </summary>
    private const float _SPACE_HEIGHT = 3;

    /// <summary>
    /// Размер поля Label для одинакового отображения во всем GUI
    /// </summary>
    private const float _LABEL_WIDTH = 80;
    
    private Editor m_LastInteractedEditor;

    #endregion

    // Add menu item to the Window menu
	[MenuItem ("Window/Inspector")]
	static void Init () {
		// Get existing open window or if none, make a new one:
        //GetWindow<AlternativeInspector>(false, "GameObjectInspector");
        EditorWindow editorWindow = GetWindow(typeof(AlternativeInspector));
        editorWindow.autoRepaintOnSceneChange = true;
        editorWindow.Show();
	    
	}

    public void Awake()
    {
        
    }

    public void Update()
    {
    }


	// Implement your own editor GUI here.
    private void OnGUI()
    {
        #region Заголовочная часть инспектора
        
        if (_GameObject)
        {
           
            #region Вертикальный блок - здесь востанавливаем вид стандартного инспектора
            EditorGUILayout.BeginVertical();

            #region Горизонтальный блок
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(EditorGUIUtility.ObjectContent(null,_GameObject.GetType()).image, GUILayout.Width(20),GUILayout.Height(20));
            GUILayout.Label("Name:", GUILayout.Width(50));
            _GameObject.name = EditorGUILayout.TextField(_GameObject.name);
            GUILayout.Label("Static:", GUILayout.Width(50));
            _GameObject.isStatic = EditorGUILayout.Toggle(_GameObject.isStatic, GUILayout.Width(18));
            EditorGUILayout.EndHorizontal();
            #endregion

            GUILayout.Space(_SPACE_HEIGHT);

            #region Горизонтальный блок
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Layer:", GUILayout.Width(50));
            _GameObject.layer = EditorGUILayout.LayerField(_GameObject.layer);
            GUILayout.Label("Tag:", GUILayout.Width(50));
            _GameObject.tag = EditorGUILayout.TagField(_GameObject.tag);
            EditorGUILayout.EndHorizontal();
            #endregion

            GUILayout.Space(_SPACE_HEIGHT);

            EditorGUILayout.EndVertical();
            #endregion
        }


        #endregion

        if (_GameObject)
        {

            foreach (var component in _GameObject.GetComponents<Component>())
            {
                if (component is Transform)
                {
                    _foldTitlebar = AlternativeGUILayout.TransformEditor(_foldTitlebar, _GameObject.transform, _rotationComponents, GUILayout.Width(_LABEL_WIDTH));
                    
                    _foldTitlebarUtil = EditorGUILayout.InspectorTitlebar(_foldTitlebarUtil, _GameObject.transform);
                    if (_foldTitlebarUtil)
                    {
                        //Утилиты для Transform
                    }
                }
                else if (component is Camera)
                {
                    EditorGUILayout.InspectorTitlebar(true, component);
                    AlternativeGUILayout.CameraEditor(component as Camera);
                }
                else EditorGUILayout.InspectorTitlebar(true, component);

                
                

            }
        }



    }

    #region События выбора объекта

    // Called whenever the selection has changed.
    private void OnSelectionChange()
    {
        _GameObject = Selection.activeGameObject;
    }

    // Called whenever the scene hierarchy
    // has changed.
    private void OnHierarchyChange()
    {
        _GameObject = Selection.activeGameObject;
    }

    // Called whenever the project has changed.
    private void OnProjectChange()
    {
        _GameObject = Selection.activeGameObject;
    }

    // OnInspectorUpdate is called at 10 frames
    // per second to give the inspector a chance
    // to update.
    private void OnInspectorUpdate()
    {
        _GameObject = Selection.activeGameObject;
        title = "ObjInspector";
        Repaint();
    }

    #endregion


	// This function is called when the object
	// is loaded.
	void OnEnable () {}
	
	// This function is called when the scriptable
	// object goes out of scope.
	void OnDisable () {
		
	}
	
}