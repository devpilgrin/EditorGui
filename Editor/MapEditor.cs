<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> 141f49e0d30b792efb901ed517702e7fdfb2a85d
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UObject = UnityEngine.Object;

namespace Utilites
{
    public class MapEditor : EditorWindow
    {
        private string myString;
        private bool groupEnabled;
        private bool myBool;
        private float myFloat;

        #region Fields
        /// <summary>
        /// Коллекция объктов для размещения на карте
        /// </summary>
        public List<PixelToObject> _PixelToObjectsList;
        
        /// <summary>
        /// Файл изображения по которому генерируется уровень
        /// </summary>
        private Texture2D _ImageMap;

        /// <summary>
        /// Размер отображаемой картинки изображения
        /// </summary>
        private float _ImageMapWidthAndHeight;

        /// <summary>
        /// Переменная отвечающая за отображение блока подсказки
        /// </summary>
        private bool _ShowPosition;

        /// <summary>
        /// Объкт Пол, его возможно разместить в любом слое с другими объектами
        /// </summary>
        private GameObject _GameObjectfloor;

        private Color Pixel;
        private Vector2 Tile;

        # endregion

        [MenuItem("Tools/Генератор карт")]
        private static void Window()
        {
           var win = GetWindow(typeof (MapEditor));
            win.minSize = new Vector2(300,400);
        }

        /// <summary>
        /// Метод отрисовки визуальных контролов в окне настроек преобразований.
        /// </summary>
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("Button");
            GUI.color = Color.cyan;
            EditorGUILayout.HelpBox("Карта преобразований", MessageType.None);
            GUI.color = Color.white;
            _ImageMapWidthAndHeight = EditorGUIUtility.labelWidth;
            EditorGUILayout.BeginHorizontal();
            _ImageMap = EditorGUILayout.ObjectField(_ImageMap, typeof (Texture2D), false, GUILayout.Width(_ImageMapWidthAndHeight),
                    GUILayout.Height(_ImageMapWidthAndHeight)) as Texture2D;
            EditorGUILayout.BeginVertical("button");
            if (GUILayout.Button("Автонастройка\nизображения")) SetTextureParametrs(_ImageMap);
            _ShowPosition = EditorGUILayout.Foldout(_ShowPosition, "Подсказка");
            if (_ShowPosition)
            {
                GUI.color = Color.green;
                EditorGUILayout.HelpBox(
                    "Данная утилита настроит возможность считывать данные необходимые для построения уровня.",
                    MessageType.None);
                GUI.color = Color.white;
            }
            else _ShowPosition = false;
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            if (GUILayout.Button("Анализировать изображение"))
            {
                var palette = getPalette(_ImageMap);
                if (_PixelToObjectsList != null) _PixelToObjectsList.Clear();
                else _PixelToObjectsList = new List<PixelToObject>();

                foreach (var color in palette)
                {
                    var PO = new PixelToObject(color);
                    _PixelToObjectsList.Add(PO);
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
            if (_PixelToObjectsList != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Эллементов в коллекции: " + _PixelToObjectsList.Count, MessageType.None);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                for (var index = 0; index < _PixelToObjectsList.Count; index++)
                {
                    EditorGUILayout.BeginHorizontal();
                    var pixelToObject = _PixelToObjectsList[index];
                    EditorGUILayout.LabelField(index + " Пол?", GUILayout.Width(45));
                    pixelToObject.Floor = EditorGUILayout.Toggle(pixelToObject.Floor, GUILayout.Width(15));
                    pixelToObject.PixelObject = (GameObject) EditorGUILayout.ObjectField(pixelToObject.PixelObject, typeof (GameObject),true);
                    pixelToObject.PixelColor = EditorGUILayout.ColorField(pixelToObject.PixelColor, GUILayout.Width(50));
                    if (GUILayout.Button("х", EditorStyles.miniButton, GUILayout.Width(22), GUILayout.Height(17)))_PixelToObjectsList.RemoveAt(index);
                    EditorGUILayout.EndHorizontal();
                }
            }
            GUILayout.Space(5);
            _GameObjectfloor = (GameObject) EditorGUILayout.ObjectField("Объект пола", _GameObjectfloor, typeof (GameObject), true);
            if (GUILayout.Button("Генерировать уровень"))GenerateLevel();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Генератор уровня
        /// </summary>
        private void GenerateLevel()
        {
            for (var x = 0; x < _ImageMap.width; x++)
            {
                for (var y = 0; y < _ImageMap.height; y++)
                {
                    Pixel = _ImageMap.GetPixel(x, y);
                    var index = _PixelToObjectsList.FindIndex(FindPTO);
                    if (index != -1)
                    {
                        Instantiate(_PixelToObjectsList[index].PixelObject, new Vector3(x*Tile.x, 0, y*Tile.y), new Quaternion());
                        if (_PixelToObjectsList[index].Floor) Instantiate(_GameObjectfloor, new Vector3(x*Tile.x, 0, y*Tile.y), new Quaternion());
                    }

                }
            }
            Instantiate(_GameObjectfloor, new Vector3(0, 0, 0), new Quaternion());
        }


        private bool FindPTO(PixelToObject obj)
        {
            return obj.PixelColor.ToString() == Pixel.ToString();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }
        
        private void Start()
        {
            Tile = new Vector2(2, 2);
        }

        private IEnumerable<Color> getPalette(Texture2D texture)
        {
            var t = texture.GetPixels().Distinct().ToArray();
            return t.Length > 0 ? t : null;
        }

        /// <summary>
        /// Настройка параметров текстуры в соответствии с требованиями необходимыми для попиксельного чтения изображения
        /// </summary>
        /// <param name="texture">Принимает параметр текстуры, которую требуется настроить</param>
        private static void SetTextureParametrs(Texture2D texture)
        {
            var textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture)) as TextureImporter;
            if (textureImporter != null)
            {
                textureImporter.textureType = TextureImporterType.Advanced;
                textureImporter.isReadable = true;
                textureImporter.filterMode = FilterMode.Point;
                textureImporter.textureFormat = TextureImporterFormat.RGB24;
            }
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(texture));
        }
    }

    /// <summary>
    /// Класс настройки сопоставлений преобразований изображения в игровой уровень.
    /// </summary>
    [Serializable]
    public class PixelToObject
    {
        /// <summary>
        /// Цвет соответствующий данному экземпляру преобразования
        /// </summary>
        public Color PixelColor;
        /// <summary>
        /// Объект размещаемый в соответствии с цветом преобразования
        /// </summary>
        public GameObject PixelObject;
        /// <summary>
        /// Размещать ли в данной ячейке объект пола
        /// </summary>
        public bool Floor;

        /// <summary>
        /// Конструктор объекта преобразования.
        /// </summary>
        /// <param name="color">Цвет соответствующий преобразованию</param>
        public PixelToObject(Color color)
        {
            PixelColor = color;
        }

    }


<<<<<<< HEAD
}
=======
}
>>>>>>> 141f49e0d30b792efb901ed517702e7fdfb2a85d
