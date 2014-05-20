using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Alternative version of EditorGUILayout.
/// </summary>
public class AlternativeGUILayout
{
    #region ������ �����������

    /// <summary>
    /// ����������� Vector4 � Quaternion
    /// </summary>
    /// <param name="v4">Vector4</param>
    /// <returns>Quaternion</returns>
    private static Quaternion ConvertToQuaternion(Vector4 v4)
    {
        return new Quaternion(v4.x, v4.y, v4.z, v4.w);
    }

    /// <summary>
    /// ����������� Quaternion � Vector4
    /// </summary>
    /// <param name="q">Quaternion</param>
    /// <returns>Vector4</returns>
    private static Vector4 QuaternionToVector4(Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }

    #endregion

    #region Vector4Field - ���������� ������� ��� �������������� ���������� ����: Vector4

    /// <summary>
    ///  ���������� ������� ��� �������������� ���������� ����: Vector4 
    /// </summary>
    /// <param name="label">������ �������� ����</param>
    /// <param name="quaterion">Quaterion ��� ����������� ��������</param>
    /// <param name="options">��������� GUILayout ��� �������������� ��������� �����</param>
    /// <returns>���������� ���� Vector4</returns>
    /// <example>
    /// <code>
    /// rotation = Vector4Field("Rotation:", QuaternionToVector4(transform.localRotation), options);
    /// </code>
    /// </example>
    public static Vector4 Vector4Field(string label, Vector4 quaterion, GUILayoutOption options)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label,options);
        var v4 = Vector4Field(quaterion);
        EditorGUILayout.EndHorizontal();
        return v4;
    }

    /// <summary>
    ///  ���������� ������� ��� �������������� ���������� ����: Vector4 
    /// </summary>
    /// <param name="label">������ �������� ����</param>
    /// <param name="quaterion">Quaterion ��� ����������� ��������</param>
    /// <returns>���������� ���� Vector4</returns>
    /// <example>
    /// <code>
    /// rotation = Vector4Field("Rotation:", QuaternionToVector4(transform.localRotation), options);
    /// </code>
    /// </example>
    public static Vector4 Vector4Field(string label, Vector4 quaterion)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label);
        var v4 = Vector4Field(quaterion);
        EditorGUILayout.EndHorizontal();
        return v4;
    }

    /// <summary>
    ///  ���������� ������� ��� �������������� ���������� ����: Vector4 
    /// </summary>
    /// <param name="quaterion">Quaterion ��� ����������� ��������</param>
    /// <returns>���������� ���� Vector4</returns>
    /// <example>
    /// <code>
    /// rotation = Vector4Field("Rotation:", QuaternionToVector4(transform.localRotation), options);
    /// </code>
    /// </example>
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

    #region Vector3Field - ���������� ������� ��� �������������� ���������� ����: Vector3

    /// <summary>
    /// ���������� ������� ��� �������������� ���������� ����: Vector3
    /// </summary>
    /// <param name="label">�������� ����: ��� string</param>
    /// <param name="vector">�������� ��� ���������� �����: ��� Vector3</param>
    /// <param name="options">��������� GUILayout ��� �������������� ��������� �����</param>
    /// <returns></returns>
    public static Vector3 Vector3Field(string label, Vector3 vector, GUILayoutOption options)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label, options);
        var v = Vector3Field(vector);
        EditorGUILayout.EndHorizontal();
        return v;
    }

    /// <summary>
    /// ���������� ������� ��� �������������� ���������� ����: Vector3
    /// </summary>
    /// <param name="label">�������� ����: ��� string</param>
    /// <param name="vector">�������� ��� ���������� �����: ��� Vector3</param>
    /// <returns></returns>
    public static Vector3 Vector3Field(string label, Vector3 vector)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label);
        var v = Vector3Field(vector);
        EditorGUILayout.EndHorizontal();
        return v;
    }

    /// <summary>
    /// ���������� ������� ��� �������������� ���������� ����: Vector3
    /// </summary>
    /// <param name="vector">�������� ��� ���������� �����: ��� Vector3</param>
    /// <returns></returns>
    public static Vector3 Vector3Field(Vector3 vector)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("X");
        var X = EditorGUILayout.FloatField(vector.x);
        GUILayout.Label("Y");
        var Y = EditorGUILayout.FloatField(vector.y);
        GUILayout.Label("Z");
        var Z = EditorGUILayout.FloatField(vector.z);
        EditorGUILayout.EndHorizontal();
        return new Vector3(X, Y, Z);
    }

    #endregion

    #region �������� ������������ ���� Transform editor �� ���������� ����������� �������������� Vector4 � Quaternion

    /// <summary>
    /// �������� ������������ ���� Transform editor �� ����������
    /// ����������� �������������� Vector4 � Quaternion
    /// </summary>
    /// <param name="foldTitlebar">���������� ���������� ��������� � InspectorTitlebar (��������/����������)</param>
    /// <param name="transform">������������� Transform</param>
    /// <param name="rotation">�������� ��� ���� Rotation</param>
    /// <returns>���������� ����������� ��������� InspectorTitlebar (��������/����������)</returns>
    public static bool TransformEditor(bool foldTitlebar, Transform transform, Vector4 rotation)
    {
        foldTitlebar = EditorGUILayout.InspectorTitlebar(foldTitlebar, transform);

        if (foldTitlebar)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            transform.position = Vector3Field("Position:",
                transform.position);
            rotation = Vector4Field("Rotation:",
                QuaternionToVector4(transform.localRotation));
            transform.localScale = Vector3Field("Scale:",
                transform.localScale);
            EditorGUILayout.EndVertical();
            transform.localRotation = ConvertToQuaternion(rotation);
        }
        return foldTitlebar;
    }

    /// <summary>
    /// �������� ������������ ���� Transform editor �� ����������
    /// ����������� �������������� Vector4 � Quaternion
    /// </summary>
    /// <param name="foldTitlebar">���������� ���������� ��������� � InspectorTitlebar (��������/����������)</param>
    /// <param name="transform">������������� Transform</param>
    /// <param name="rotation">�������� ��� ���� Rotation</param>
    /// <param name="options">��������� GUILayout ��� �������������� ��������� �����</param>
    /// <returns>���������� ����������� ��������� InspectorTitlebar (��������/����������)</returns>
    public static bool TransformEditor(bool foldTitlebar, Transform transform, Vector4 rotation, GUILayoutOption options)
    {
        foldTitlebar = EditorGUILayout.InspectorTitlebar(foldTitlebar, transform);

        if (foldTitlebar)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            transform.position = Vector3Field("Position:",
                transform.position, options);
            rotation = Vector4Field("Rotation:",
                QuaternionToVector4(transform.localRotation), options);
            transform.localScale = Vector3Field("Scale:",
                transform.localScale, options);
            EditorGUILayout.EndVertical();
            transform.localRotation = ConvertToQuaternion(rotation);
        }
        return foldTitlebar;
    }

    #endregion


    //���������� ������� InspectorTitlebar � ������������ ��������� ����������� �������� � �������� ������� ��������
    //��� � BeginVertical / EndVertical

    public static bool CustomTitlebar(string str, bool show, Type type, Color color, GUIStyle style, GUILayoutOption options)
    {
        var Texture = Resources.Load<Texture>("icons/help_icon");

        EditorGUILayout.BeginHorizontal("flow navbar back");
        var ControlRect = EditorGUILayout.GetControlRect();
        if (show) if (GUI.Button(new Rect(1, ControlRect.y, 20, 20), "", "Foldout")) show = false;
        if (!show) if (GUI.Button(new Rect(1, ControlRect.y, 20, 20), "", "Foldout")) show = true;

        GUI.Label(new Rect(ControlRect.x+11, ControlRect.y, 20, 20),EditorGUIUtility.ObjectContent(null, type).image);

        GUI.color = color;
        GUI.Label(new Rect(ControlRect.x + 44, ControlRect.y, ControlRect.width - 50, 20), str, "BoldLabel");
        GUI.color = Color.white;

        if (GUILayout.Button(new GUIContent(Texture),"label", GUILayout.Width(20), GUILayout.Height(20)))
        {
            Application.OpenURL("http://unity3d.com/search?gq=" + str);
        }

        EditorGUILayout.EndHorizontal();
        
        return show;
    }



    #region CameraEditor

    public static bool CameraEditor(Camera camera)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Clear Flags");
        camera.clearFlags = (CameraClearFlags) EditorGUILayout.EnumPopup(camera.clearFlags);
        EditorGUILayout.EndHorizontal();


        if (camera.clearFlags == CameraClearFlags.Color)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Background Color");
            camera.backgroundColor = EditorGUILayout.ColorField(camera.backgroundColor);
            EditorGUILayout.EndHorizontal();
        }
        if (camera.clearFlags == CameraClearFlags.Skybox)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Background Color");
            camera.backgroundColor = EditorGUILayout.ColorField(camera.backgroundColor);
            EditorGUILayout.EndHorizontal();
        }
        RenderTexture renderTexture = new RenderTexture(100, 100, 100, RenderTextureFormat.Default,
            RenderTextureReadWrite.Default);

        camera.targetTexture = renderTexture;

        return true;
    }

    #endregion

    

    /// <summary>
    /// ������� �������� �����
    /// </summary>
    /// <param name="label">������ �������� ����</param>
    /// <param name="buttonLabel">������ �������� ������</param>
    /// <param name="labelMaxWidth">������������ ������ label</param>
    /// <param name="path">������ ��� �������� ���������� �� ���������</param>
    /// <param name="extension">���������� ������ � �����</param>
    /// <returns></returns>
    public static string FileField(string label, string buttonLabel, float labelMaxWidth, string path, string extension)
    {
        //����� ���� ������� ��� ��������� � �������� ������ ������� ����������...
        //��� ������� ������ label = 7f

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label(label, GUILayout.MaxWidth(labelMaxWidth));

        var fileLabel = EditorGUILayout.TextField(path);

        if (GUILayout.Button(buttonLabel, "minibuttonright"))
        {
            fileLabel = EditorUtility.OpenFilePanel(label, path, extension);
        }

        EditorGUILayout.EndHorizontal();
        return fileLabel;
    }

}
