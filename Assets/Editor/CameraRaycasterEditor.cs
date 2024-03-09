// Для API редактора
using UnityEditor;

// TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor
{
    bool isLayerPrioritiesUnfolded = true; // store the UI state

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Сохраняем состояние cameraRaycaster(как json, говорилось в курсе) 

        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
        //Если вкладочка открыта: 
        if (isLayerPrioritiesUnfolded)
        {
            EditorGUI.indentLevel++; //делаем для вкладки отступ
            {
                BindArraySize(); // сопоставляем значения размера массива
                BindArrayElements();// сопоставляем значения самого массива
                //чтобы то, что в редакторе соответсвовало тому, что в коде

            }
            EditorGUI.indentLevel--;
        }

        /* просто ради урока-прикола
        string some_prompt = serializedObject.FindProperty("toPrint").stringValue;
        string required_string = EditorGUILayout.TextField("Printing this:", some_prompt);
        if (some_prompt != required_string)
        {
            serializedObject.FindProperty("toPrint").stringValue = required_string;
        }
        */

        serializedObject.ApplyModifiedProperties(); // Применяем изменения
    }

    void BindArraySize()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue; // получаем значение из кода
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize); // рисуем поле с численным значением
        if (requiredArraySize != currentArraySize)
        {
            // если они не совпадают
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize; // меняем то, что в коде на то, что в редакторе
        }
    }

    void BindArrayElements()
    {
        //аналогично коду выше
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        for (int i = 0; i < currentArraySize; i++)
        {
            var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
        }
    }
}
