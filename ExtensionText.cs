using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/ExtensionText")]
public class ExtensionText : Text
{
    [SerializeField] private string StringKey = null;
    [SerializeField] private int StringCode = -1;

    private bool TextInitialize = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (TextInitialize == false && Application.isPlaying)
        {
            //var str = string.IsNullOrEmpty(StringKey) ?
            //    HMTableDataController.Instance.GetStringData(StringCode) :
            //    HMTableDataController.Instance.GetStringData(StringKey);

            //text = string.IsNullOrEmpty(str) ? text : str;
            //TextInitialize = true;
        }
    }
}
