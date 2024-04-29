using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGround : MonoBehaviour
{
    public float width = 1;
    public float height = 0.5f;

    public int previewWidth = 1;
    public int previewHeight = 1;

    public Vector3 GetPosition(Vector3Int point)
    {
        return transform.position + transform.TransformDirection(new Vector3(point.x * width, point.y * height, point.z * width));
    }
    public Vector3Int GetPoint(Vector3 position)
    {
        var relPos = transform.InverseTransformDirection(position - transform.position);
        return new Vector3Int(Mathf.RoundToInt(relPos.x / width), Mathf.RoundToInt(relPos.y / height), Mathf.RoundToInt(relPos.z / width));
    }

    public Quaternion GetRotation(BlockRotation rotation)
    {
        return Quaternion.Euler(0, rotation.value * 90, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        DrawGizmos();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        DrawGizmos();
    }
    private void DrawGizmos()
    {
        for (int i = -previewWidth; i <= previewWidth; i++)
        {
            for (int j = -previewWidth; j <= previewWidth; j++)
            {
                for (int k = -previewHeight; k <= previewHeight; k++)
                {
                    Gizmos.DrawWireSphere(GetPosition(new Vector3Int(i, k, j)), height / 4f);
                }
            }
        }
    }
}
