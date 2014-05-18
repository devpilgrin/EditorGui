using UnityEngine;
using UnityEditor;

//Название не в честь Альтернатива Платформ, а
//просто как Альтернативный инспектор объектов.
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

    //Блок глобальных констант отвечающих за отображение GUI
    /// <summary>
    /// Размер вертикального разделителя
    /// </summary>
    private const float _SPACE_HEIGHT = 3;

    /// <summary>
    /// Размер поля Label для одинакового отображения во всем GUI
    /// </summary>
    private const float _LABEL_WIDTH = 80;

    #endregion

    // Add menu item to the Window menu
	[MenuItem ("Window/Inspector")]
	static void Init () {
		// Get existing open window or if none, make a new one:
        GetWindow<AlternativeInspector>(false, "GameObjectInspector");
	    
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
            _foldTitlebar = AlternativeGUILayout.TransformEditor(_foldTitlebar, _GameObject.transform, _rotationComponents, GUILayout.Width(_LABEL_WIDTH));


            foreach (var component in _GameObject.GetComponents<Component>())
            {
                //EditorGUILayout.GetControlRect();
                EditorGUIUtility.ObjectContent(component, component.GetType());
            }

            //_GameObject.GetComponents<Component>();

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

	// Called 100 times per second on all visible
	// windows.
	void Update () {}
	
	// This function is called when the object
	// is loaded.
	void OnEnable () {}
	
	// This function is called when the scriptable
	// object goes out of scope.
	void OnDisable () {
		
	}
	
}




/// <summary>
/// Alternative version of EditorGUILayout.
/// </summary>
public class AlternativeGUILayout
{
    
    #region Методы конвертации

    /// <summary>
    /// Конвертация Vector4 в Quaternion
    /// </summary>
    /// <param name="v4">Vector4</param>
    /// <returns>Quaternion</returns>
    private static Quaternion ConvertToQuaternion(Vector4 v4)
    {
        return new Quaternion(v4.x, v4.y, v4.z, v4.w);
    }

    /// <summary>
    /// Конвертация Quaternion в Vector4
    /// </summary>
    /// <param name="q">Quaternion</param>
    /// <returns>Vector4</returns>
    private static Vector4 QuaternionToVector4(Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }

    #endregion

    #region Vector4Field

    public static Vector4 Vector4Field(string label, Vector4 quaterion, GUILayoutOption options)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label,options);
        var v4 = Vector4Field(quaterion);
        EditorGUILayout.EndHorizontal();
        return v4;
    }

    public static Vector4 Vector4Field(string label, Vector4 quaterion)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label);
        var v4 = Vector4Field(quaterion);
        EditorGUILayout.EndHorizontal();
        return v4;
    }

    public static Vector4 Vector4Field(Vector4 quaterion)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("X");
        var X = EditorGUILayout.FloatField(quaterion.x);
        GUILayout.Label("Y");
        var Y = EditorGUILayout.FloatField(quaterion.y);
        GUILayout.Label("Z");
        var Z = EditorGUILayout.FloatField(quaterion.z);
        GUILayout.Label("W");
        var W = EditorGUILayout.FloatField(quaterion.w);
        EditorGUILayout.EndHorizontal();
        return new Vector4(X, Y, Z, W);
    }

    #endregion

    #region Vector3Field

    public static Vector3 Vector3Field(string label, Vector3 quaterion, GUILayoutOption options)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label, options);
        var v = Vector3Field(quaterion);
        EditorGUILayout.EndHorizontal();
        return v;
    }

    public static Vector3 Vector3Field(string label, Vector3 quaterion)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label);
        var v = Vector3Field(quaterion);
        EditorGUILayout.EndHorizontal();
        return v;
    }

    public static Vector3 Vector3Field(Vector3 quaterion)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("X");
        var X = EditorGUILayout.FloatField(quaterion.x);
        GUILayout.Label("Y");
        var Y = EditorGUILayout.FloatField(quaterion.y);
        GUILayout.Label("Z");
        var Z = EditorGUILayout.FloatField(quaterion.z);
        EditorGUILayout.EndHorizontal();
        return new Vector3(X, Y, Z);
    }

    #endregion






    /// <summary>
    /// Transform editor
    /// </summary>
    /// <param name="foldTitlebar"></param>
    /// <param name="transform"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static bool TransformEditor(bool foldTitlebar, Transform transform, GUILayoutOption options)
    {
        return TransformEditor(foldTitlebar, transform, Vector4.zero, options);
    }

    /// <summary>
    /// Transform editor
    /// </summary>
    /// <param name="foldTitlebar"></param>
    /// <param name="transform"></param>
    /// <param name="rotation"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static bool TransformEditor(bool foldTitlebar, Transform transform, Vector4 rotation, GUILayoutOption options)
    {
        foldTitlebar =  EditorGUILayout.InspectorTitlebar(foldTitlebar, transform);

        if (foldTitlebar)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            transform.position = AlternativeGUILayout.Vector3Field("Position:",
                transform.position, options);
            rotation = AlternativeGUILayout.Vector4Field("Rotation:",
                QuaternionToVector4(transform.localRotation), options);
            transform.localScale = AlternativeGUILayout.Vector3Field("Scale:",
                transform.localScale, options);
            EditorGUILayout.EndVertical();
            transform.localRotation = ConvertToQuaternion(rotation);
        }
        return foldTitlebar;
    }


}