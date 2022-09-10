using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public enum BorderType
    {
        TopBot, LeftRight
    }

    public Border oppositeBorder;
    public BorderType borderType;
}
