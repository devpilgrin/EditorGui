//Класс переопределяющий стандартный редактор инспектора для всех GameObject
//Данный редактор будет отображаться для всех GameObject как в сцене, так и в
//деревее иерархии сцены, а также в обозревателе проекта

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject), true)]
public class GOEditor : Editor
{
    /// <summary>
    /// Целевой GameObject (в дальнейшем я не буду использовать XML коментарии, так как класс не предназначен
    /// для использования в других классах, а теги непомерно раздувают коментарии.)
    /// </summary>
    private GameObject targetGameObject;

    //Отображение расширенного редактора 
    public static bool enableAdditional;

    //Transform нашего целевого GameObject
    private Transform transform;

    //Переменная хранит состояние отображения Handles
    //Примечания:
    //Объявлена как static для того, чтобы при переходе между
    //объектами сцены состояние сохранялось для всех объектов.
    private static bool showHandles;

 
    //Отображение инструментов сцены (перемещение, масштабирование, вращение)
    Tool LastTool = Tool.None;
    private static bool _disableTransformTools;

    //Параметры оформления
    const int SpaceHeight = 2;
    const int SpaceWidth = 5;
    
    //анимация
    private Vector3 transformRotor;


    // Реализация данной процедуры позволяет переопределить стандартный вывод
    // инспектора. Она позволяет создать пользовательский редактр инспектора объектов.
    public override void OnInspectorGUI()
    {
        //В отрисовке графических элементов сцены стараюсь не использовать методы и функции
        //Так улучшается общая читаемость кода.
        //В качестве дополнительных  разделителей кода можно использовать регионы --> Продолжение далее по коду
        
        GUILayout.Space(SpaceHeight); //разделитель
        
        #region Вертикальный блок - здесь востанавливаем вид стандартного инспектора
        EditorGUILayout.BeginVertical("SelectionRect");
        EditorGUILayout.LabelField("", "Default Editor", "OL Titlemid");

        #region Горизонтальный блок
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:", GUILayout.Width(50));
        targetGameObject.name = EditorGUILayout.TextField(targetGameObject.name);
        GUILayout.Label("Static:", GUILayout.Width(50));
        targetGameObject.isStatic = EditorGUILayout.Toggle(targetGameObject.isStatic, GUILayout.Width(18));
        EditorGUILayout.EndHorizontal();
        #endregion

        GUILayout.Space(SpaceHeight);

        #region Горизонтальный блок
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Layer:", GUILayout.Width(50));
        targetGameObject.layer = EditorGUILayout.LayerField(targetGameObject.layer);
        GUILayout.Label("Tag:", GUILayout.Width(50));
        targetGameObject.tag = EditorGUILayout.TagField(targetGameObject.tag);
        EditorGUILayout.EndHorizontal();
        #endregion

        GUILayout.Space(SpaceHeight);

        #region Горизонтальный блок
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Enable additional editor");
        enableAdditional = EditorGUILayout.Toggle(enableAdditional);
        GUILayout.Label("Disable transform tools");
        _disableTransformTools = GUILayout.Toggle(_disableTransformTools, "");
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.EndVertical();
        #endregion

        GUILayout.Space(SpaceHeight); //разделитель

        //Если включен режим отображения расширенного редактора
        if (!enableAdditional) return;

        #region Вертикальный блок - реализуем расширенный редактор

        EditorGUILayout.BeginVertical("SelectionRect");
        EditorGUILayout.LabelField("", "Additional Editor", "OL Titlemid");

        #region Горизонтальный блок - блок кнопок управления трансформациями

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset angle", "minibutton")) transform.rotation = Quaternion.identity;
        if (GUILayout.Button("Step Rotate", "minibutton"))
        {
            transform.Rotate(new Vector3(10, 10, 10));
        }
        if (GUILayout.Button("Reset local position", "minibutton")) transform.localPosition = Vector3.zero;
        EditorGUILayout.EndHorizontal();

        #endregion

        showHandles = EditorGUILayout.Toggle("Show Handles", showHandles);

        #region Горизонтальный блок - заголовок таблицы вершин выделенного объекта  

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(SpaceWidth);

        EditorGUILayout.LabelField("", "Mesh", "PR Insertion");
        EditorGUILayout.EndHorizontal();

        #endregion

        #region Вертикальный блок - таблица вершин выделенного объекта

        EditorGUILayout.BeginVertical("SelectionRect");
        if (targetGameObject.GetComponent<MeshFilter>())
        {
            var _meshFilter = targetGameObject.GetComponent<MeshFilter>();
            var _meshFilter_Vertices = _meshFilter.sharedMesh.vertices;
            foreach (var vertices in _meshFilter_Vertices)
                EditorGUILayout.Vector3Field("", vertices);
        }
        EditorGUILayout.EndVertical();

        #endregion

        EditorGUILayout.EndVertical();

        #endregion


        // <-- Продолжение: Так мы добиваемся отображение структуры блоков в виде дерева
        // Достаточно легко читаемо + наглядные коментарии 

        GUILayout.Space(SpaceHeight); //разделитель

    }

    //При загрузке объекта
	void OnEnable ()
	{
        //Получаем целевой объект для редактирования и записываем его как GameObject
	    targetGameObject = target as GameObject;
        //Получаем transform объекта.
        //По идее проверка на null для GameObject в данном месте не нужна, но 
        //Редактор Unity считает иначе, поэтому оставляем
	    if (targetGameObject != null) transform = targetGameObject.transform;

        //Записываем текущий инструмент
        LastTool = Tools.current;
        if (_disableTransformTools) Tools.current = Tool.None;

	}

    //При обновлении инспектора
    public void OnInspectorUpdate()
    {
        Repaint();
    }

    //При выходе из инспектора
    void OnDisable()
    {
        //Возвращаем предыдущий (до скрытия) инструмент трансформации, дабы
        //не потерять его при смене объекта или потери фокуса окном инспектора
        Tools.current = LastTool;
    }

    //Отрисовка сцены
    //Примечание:
    //Влиять на отрисовку сцены из редактора объекта не самый лучший способ
    //Здесь используется именно такой подход, только для того, чтобы не увиличивать колличество 
    // файлов исходных кодов и для удобства оценки всего кода.
    public void OnSceneGUI()
    {

        //Если отключили отображение Tools
        //Примечание:
        //На самом деле отключать Tools имеет смысл только тогда, когда добавлены дополнительные
        //настройки позволяющие задавать блокировки объектов в сцене (от их случайного редактирования)
        //В остальных случаях Tools имеет смысл переопределять на собственнй.
        //На самом деле здесь очень большой потенциал для редактирования, так как можно настроить по мимо
        //отображения Tools, так-же и дополнительные возможности, например нечто на подобие меню объекта
        //в Maya.
        if (_disableTransformTools) Tools.current = Tool.None; 



        //Примечание:
        //showHandles - только сам функционал отрисовки Handles + масштабирование и перемещение
        //На самом деле здесь нужно еще оценивать размер самого Mesh, дабы регулировать длинну линий и размер точек при их отрисовке.
        //Но в данной демонстрации я стараюсь показать как можно больше методик работы с редактором и сценой, а не конкретные алгоритмы
        //работы с Mesh.

        //Если включили отображение собственных Handles
        if (!showHandles) return;
        
        //Если на текущем объекте висит компонент камера, тогда организуем отрисовку  положения линзы.
        //Примечание:
        //Динза виртуальная, ничего не делает, просто берет настройки о собственном размере и положении из 
        //настроек масштабирования по осям X и Z
        if (targetGameObject.GetComponent<Camera>())
        {
            Handles.color = Color.blue;
            Handles.DrawLine(transform.position, transform.position + Vector3.forward);
            Handles.CircleCap(1, transform.position + Vector3.forward*transform.localScale.z, transform.localRotation,
                transform.localScale.x);
            Handles.Label(transform.position + Vector3.up/4f, targetGameObject.name);
        }


        //Если выбранный объект содержит компонент MeshFilter, тогда визуализируем
        //вершины и нормали
        if (targetGameObject.GetComponent<MeshFilter>())
        {

            var _meshFilter = targetGameObject.GetComponent<MeshFilter>();
            //Примечание:
            //Здесь используем sharedMesh во избежание исключений редактора Unity
            var _meshFilter_Normals = _meshFilter.sharedMesh.normals;
            var _meshFilter_Vertices = _meshFilter.sharedMesh.vertices;
            
            //Рисуем все вершины
            for (var i = 0; i < _meshFilter_Vertices.Length; i++)
            {
                var vertices = _meshFilter_Vertices[i];
                Handles.color = Color.green;
                vertices.Scale(transform.lossyScale);
                Handles.SphereCap(i, vertices + (transform.position) , transform.rotation, 0.1f * (transform.lossyScale.magnitude/4));
            }

            //Рисуем нормали
            foreach (var normal in _meshFilter_Normals)
            {
                Handles.color = Color.cyan;
                normal.Scale(transform.lossyScale);
                Handles.DrawLine(transform.position + normal, transform.localPosition);
            }
        }



    }

}
