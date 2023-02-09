using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionComponentConvertTool 
{
    [MenuItem("Assets/HM/Extension Component Convert", false, int.MinValue)]
    private static void AssetsConvert()
    {
        ConvertAssets(Selection.objects);
    }

    [MenuItem("GameObject/ExtensionText Convert", false, int.MinValue)]
    private static void GameObjectConvert()
    {
        ConvertGameObject(Selection.activeObject);
    }

    private static void ConvertGameObject(Object findTarget)
    {
        if (findTarget is GameObject == false)
        {
            return;
        }

        var parentOb = findTarget as GameObject;
        var textComponent = parentOb.GetComponent<Text>();
        var oldSerializedOb = new SerializedObject(textComponent);

        Object.DestroyImmediate(parentOb.GetComponent<Text>());

        var convertText = parentOb.AddComponent<ExtensionText>();
        var newSerializedOb = new SerializedObject(convertText);
        var iter = oldSerializedOb.GetIterator();

        while (iter.NextVisible(true))
        {
            if (iter.name.Equals("m_Script"))
            {
                continue;
            }

            var element = newSerializedOb.FindProperty(iter.name);

            if (element != null && element.propertyType == iter.propertyType)
            {
                newSerializedOb.CopyFromSerializedProperty(iter);
            }
        }

        newSerializedOb.ApplyModifiedProperties();
    }

    private static void ConvertAssets(Object[] findTarget)
    {
        var selectObjects = findTarget;

        for (int i = 0; i < selectObjects.Length; i++)
        {
            if (selectObjects[i] is GameObject == false)
            {
                continue;
            }

            ConvertComponent<Text, ExtensionText>(selectObjects[i]);
            //ConvertComponent<Image, HMExtensionImage>(selectObjects[i]);
        }

        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/HM/Revert Image", false, int.MinValue)]
    private static void RevertImage()
    {
        var selectObjects = Selection.objects;

        for (int i = 0; i < selectObjects.Length; i++)
        {
            if (selectObjects[i] is GameObject == false)
            {
                continue;
            }

            //ConvertComponent<HMExtensionImage, Image>(selectObjects[i], false);
        }

        AssetDatabase.Refresh();
    }

    private static void ConvertComponent<TOld, TNew>(Object obj, bool check = true)
        where TOld : Component
        where TNew : Component
    {
        
        var selectObjectPath    = AssetDatabase.GetAssetPath(obj);
        var selectPrefab        = PrefabUtility.LoadPrefabContents(selectObjectPath);
        var oldComponents       = selectPrefab.GetComponentsInChildren<TOld>(true);

        for (int i = 0; i < oldComponents.Length; i++)
        {
            if (oldComponents[i] is TNew == true && check)
            {
                continue;
            }

            var parentOb        = oldComponents[i].gameObject;
            var oldSerializedOb = new SerializedObject(oldComponents[i]);
            
            Object.DestroyImmediate(parentOb.GetComponent<TOld>());
            
            var convertText     = parentOb.AddComponent<TNew>();
            var newSerializedOb = new SerializedObject(convertText);
            var iter            = oldSerializedOb.GetIterator();

            while (iter.NextVisible(true))
            {
                if (iter.name.Equals("m_Script"))
                {
                    continue;
                }

                var element = newSerializedOb.FindProperty(iter.name);

                if (element != null && element.propertyType == iter.propertyType)
                {
                    newSerializedOb.CopyFromSerializedProperty(iter);
                }
            }

            newSerializedOb.ApplyModifiedProperties();
            PrefabUtility.SaveAsPrefabAsset(selectPrefab, selectObjectPath);
        }
    }
}
