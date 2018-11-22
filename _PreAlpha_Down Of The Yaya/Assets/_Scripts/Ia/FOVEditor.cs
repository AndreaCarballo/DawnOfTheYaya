using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ZombieAgent))]
public class FOVEditor : Editor
{

    void OnSceneGUI()
    {
        ZombieAgent fow = (ZombieAgent)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.FOVRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.FOVAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.FOVAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.FOVAngle);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.FOVAngle);

    }

}

