using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UObject = UnityEngine.Object;

public class MapEditor : EditorWindow
{
    private string myString;
    private bool groupEnabled;
    private bool myBool;
    private float myFloat;

    public List<PixelToObject> PTObjects;
    private Texture2D image;
    private float utilwidth;
    private bool showPosition;
    private UObject gameObjectfloor;

    private Color Pixel;
    private Vector2 Tile;

    [MenuItem("Tools/MapEditor")]
    static void Window()
    {
        EditorWindow editorWindow = GetWindow(typeof(MapEditor));
        editorWindow.autoRepaintOnSceneChange = true;
        editorWindow.Show();
    }


    void OnGUI()
    {
        
        EditorGUILayout.BeginVertical("Button");

        GUI.color = Color.cyan;
        EditorGUILayout.HelpBox("Карта преобразований", MessageType.None);
        GUI.color = Color.white;

        utilwidth = EditorGUIUtility.labelWidth;
        EditorGUILayout.BeginHorizontal();
        image = EditorGUILayout.ObjectField(image, typeof(Texture2D), false, GUILayout.Width(utilwidth), GUILayout.Height(utilwidth)) as Texture2D;

        EditorGUILayout.BeginVertical("button");
        if(GUILayout.Button("Автонастройка изображения")) SetTextureParametrs(image);
        
        showPosition = EditorGUILayout.Foldout(showPosition, "Подсказка");
        if (showPosition)
        {
            GUI.color = Color.green;
            EditorGUILayout.HelpBox("Данная утилита настроит возможность считывать данные необходимые для построения уровня.", MessageType.Info);
            GUI.color = Color.white;
        }
        else showPosition = false;

        EditorGUILayout.EndVertical();
        
        
        EditorGUILayout.EndHorizontal();


        
        GUILayout.Space(5);



        if (GUILayout.Button("Анализировать изображение"))
        {

            var palette = getPalette(image);
            if(PTObjects!=null)PTObjects.Clear();
            else PTObjects = new List<PixelToObject>();
            
            foreach (var color in palette)
            {
                var PO = new PixelToObject(color);
                PO.PixelObject = new UObject();
                PTObjects.Add(PO);
            }

        }
        EditorGUILayout.EndVertical();


        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("Button");
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUI.color = Color.cyan;
        EditorGUILayout.HelpBox("Настройка преобразований", MessageType.None);
        GUI.color = Color.white;




        EditorGUILayout.EndHorizontal();

        Tile = EditorGUILayout.Vector2Field("Размер тайла", Tile);



        if (PTObjects != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Эллементов в коллекции: " + PTObjects.Count, MessageType.None);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            for (var index = 0; index < PTObjects.Count; index++)
            {
                EditorGUILayout.BeginHorizontal();
                var pixelToObject = PTObjects[index];
                EditorGUILayout.LabelField(index + " Пол?", GUILayout.Width(45));
                pixelToObject.Floor = EditorGUILayout.Toggle(pixelToObject.Floor, GUILayout.Width(15));
                pixelToObject.PixelObject = EditorGUILayout.ObjectField(pixelToObject.PixelObject, typeof(UObject), true);
                pixelToObject.PixelColor = EditorGUILayout.ColorField(pixelToObject.PixelColor, GUILayout.Width(50));
                if (GUILayout.Button("х", EditorStyles.miniButton, GUILayout.Width(22), GUILayout.Height(17)))
                    PTObjects.RemoveAt(index);
                EditorGUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(5);

        gameObjectfloor = EditorGUILayout.ObjectField("Объект пола", gameObjectfloor, typeof(UObject), true);


        if (GUILayout.Button("Генерировать уровень"))
        {
            GenerateLevel();
        }

        EditorGUILayout.EndVertical();        
    }


    private void GenerateLevel()
    {
        for (int x = 0; x < image.width; x++)
        {
            for (int y = 0; y < image.height; y++)
            {
                Pixel = image.GetPixel(x, y);
                var index = PTObjects.FindIndex(FindPTO);
                if (index != -1)
                {
                    
                    Instantiate(PTObjects[index].PixelObject, new Vector3(x * Tile.x, 0, y * Tile.y), new Quaternion());
                    if (PTObjects[index].Floor) Instantiate(gameObjectfloor, new Vector3(x * Tile.x, 0, y * Tile.y), new Quaternion());
                    
                }

            }
        }

        Instantiate(gameObjectfloor, new Vector3(0, 0, 0), new Quaternion());
        
    }

    private bool FindPTO(PixelToObject obj)
    {
        return obj.PixelColor.ToString() == Pixel.ToString();
    }


    void OnInspectorUpdate()
    {
        Repaint();
    }

	// Use this for initialization
	void Start ()
	{
        Tile = new Vector2(2,2);
	}

    Color[] getPalette(Texture2D texture)
    {
        var t =  texture.GetPixels().Distinct().ToArray();
        return t.Length > 0 ? t : null;
    }

    void SetTextureParametrs(Texture2D texture)
    {
        var textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture)) as TextureImporter;
        textureImporter.textureType = TextureImporterType.Advanced;
        textureImporter.isReadable = true;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureFormat = TextureImporterFormat.RGB24;
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(texture));

    }
}

[Serializable]
public class PixelToObject
{
    public Color PixelColor;
    public UObject PixelObject;
    public bool Floor;
    
    public PixelToObject(Color color)
    {
        PixelColor = color;
    }

}


