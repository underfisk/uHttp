using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(HttpConfig))]
public class LoginController : MonoBehaviour
{

    [SerializeField] public Text ResponseTarget;
    [SerializeField] public InputField userField, pwdField;
    [SerializeField] public Button syncBtn, asyncBtn;

    private HttpConfig db;
    
    public virtual void Start()
    {
        if (ResponseTarget == null) Debug.LogWarning("Response target is not defined");
        db = gameObject.GetComponent<HttpConfig>();
        syncBtn.onClick.AddListener(() => SyncRequest());
        asyncBtn.onClick.AddListener(() => StartCoroutine(AsyncRequest()));
    }


    /// <summary>
    /// Starts a coroutine with native Unity async WWW using a formData in $_POST also
    /// </summary>
    /// <returns></returns>
    public IEnumerator AsyncRequest()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection(string.Format("username={0}&password{1}&token={2}",userField.text, pwdField.text, db.Token)));

        UnityWebRequest www = UnityWebRequest.Post(db.HttpLinks[0], formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            ResponseTarget.text = "Async Response: " + www.responseCode;
            ResponseTarget.color = Color.red;
        }
        else
        {
            ResponseTarget.text = "Async Response: " + www.downloadHandler.text;
            Debug.Log("Download Reply Async : " + www.downloadHandler.text);
        }
    }

    /// <summary>
    /// Requests a syncronized http request to a determinated host and send the data in $_POST
    /// </summary>
    public void SyncRequest()
    {
        var request = new HttpRequest();
        var form = new HttpForm();
        form.AddField("username",userField.text);
        form.AddField("password", pwdField.text);
        form.AddField("token", db.Token);
        request.Post(db.HttpLinks[0], form);

        Debug.Log("Request Headers : " + request.headers);
        Debug.Log("Request post data :" + form.ToString());

        if (request.isDone)
        {
            if (!request.isError || request.statusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseTarget.text = "Success Response: " + request.ContentResponse;
                Debug.Log(request.ContentResponse);
            }
            else
            {
                ResponseTarget.text = "Response: " + request.statusCode;
                ResponseTarget.color = Color.red;
                Debug.Log("status code error : " + request.statusCode);
            }
        }
    }
}
