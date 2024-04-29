using UnityEngine;

namespace UnityEditor
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Fetch"))
            {
                Fetch();
            }
        }

        public void Fetch()
        {
            var target = this.target as Level;
            var blocks = target.GetComponentsInChildren<Block>();
            target.blocks = blocks;
            PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            EditorUtility.SetDirty(target);
        }
    }
}
