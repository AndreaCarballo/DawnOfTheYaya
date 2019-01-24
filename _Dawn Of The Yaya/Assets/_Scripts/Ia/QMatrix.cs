using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QMatrix {

    public float[] array;

    public QMatrix(float[] _array)
    {
        array = _array;
    }
}
