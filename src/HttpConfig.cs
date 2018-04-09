using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpConfig : MonoBehaviour
{

    /// <summary>
    /// List of urls here
    /// </summary>
    [SerializeField] public string [] HttpLinks;

    /// <summary>
    /// Communication Token
    /// </summary>
    [SerializeField,Tooltip("Communication Token (Strongly recommend)")] public string Token;

}
