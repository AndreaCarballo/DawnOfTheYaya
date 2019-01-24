using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Matrix{

    public List<QMatrix> QStates;
    public List<QMatrix> QActions;

    public Matrix(List<QMatrix> _QStates, List<QMatrix> _QActions)
    {
        QStates = _QStates;
        QActions = _QActions;
    }
}
