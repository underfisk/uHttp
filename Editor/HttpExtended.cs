using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(HttpConfig))]
public class HttpExtended : Editor
{
    SerializedObject so;
    SerializedProperty http_links, Token;

    public virtual void Awake()
    {
        so = new SerializedObject(target);
        http_links = so.FindProperty("HttpLinks");
        Token = so.FindProperty("Token");
    }

    public override void OnInspectorGUI()
    {
        so.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(http_links, true);
        EditorGUILayout.PropertyField(Token);
        EditorGUILayout.HelpBox("We'll establish a connection with the url in position 0 ", MessageType.Info);
        if (GUILayout.Button("Test Connection"))
        {
            Debug.Log(string.Format("Establishing connection with URL {0}", (http_links.arraySize > 0 ? http_links.GetArrayElementAtIndex(0).stringValue : "Invalid URL")));
            TestConnection();
        }


        so.ApplyModifiedProperties();
    }

    public void TestConnection()
    {
        HttpRequest request = new HttpRequest();
        request.GET(http_links.arraySize > 0 ? http_links.GetArrayElementAtIndex(0).stringValue : "Invalid URL");
        if (request.isDone)
        {
            if (!request.isError || request.statusCode == System.Net.HttpStatusCode.OK)
            {
                GUILayout.Label("Response: " + request.ContentResponse);
                Debug.Log("The test connection was a success (" + request.length + " bytes downloaded). \nReturned Status Code : " + request.statusCode + " and response content : " + request.ContentResponse);
            }
            else
            {
                GUILayout.Label("There was an erro trying to communicate with the given URL, Status Code: " + request.statusCode);
            }
        }

    }
}
