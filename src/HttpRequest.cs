using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class HttpRequest
{

    /// <summary>
    /// Holds the status code of this response
    /// </summary>
    public HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

    /// <summary>
    /// Returns the state of the request
    /// </summary>
    public bool isDone = false;

    /// <summary>
    /// Number of bytes downloaded
    /// </summary>
    public byte[] bytes;

    /// <summary>
    /// Content Length from streamReader
    /// </summary>
    public long length = 0;

    /// <summary>
    /// Content type (raw html/text, etc)
    /// </summary>
    public string type = String.Empty;

    /// <summary>
    /// Request Headers
    /// </summary>
    public string headers = String.Empty;

    /// <summary>
    /// Request URL just saved in case of the user wanted to know
    /// </summary>
    public string url = String.Empty;

    /// <summary>
    /// Holds the state of the content type
    /// </summary>
    public bool isJson = false;

    /// <summary>
    /// Returns any error if occured
    /// </summary>
    public bool isError = false;

    /// <summary>
    /// Holds the raw data from this request
    /// </summary>
    public string ContentResponse;


    /// <summary>
    /// Request a pure HttpRequest synchronized which do not return but his data may be accessed on the created instance
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    public void Post(string url, HttpForm data)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data.ToString());
            request.ContentLength = bytes.Length;

            Stream os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            if (resp == null) ContentResponse = String.Empty;

            StreamReader sr = new StreamReader(resp.GetResponseStream());

            //Releases the resources of the response
            resp.Close();

            //The request was successfull
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                //Set instance data
                ContentResponse = sr.ReadToEnd().Trim();
                headers = resp.Headers.ToString();
                length = resp.ContentLength;
                this.url = url;
                this.bytes = bytes;
                isJson = JsonHelper.isJson(ContentResponse);
                statusCode = resp.StatusCode;
                isError = false;
            }

        }
        catch (WebException e)
        {
            Debug.Log("\r Web Exception :  " + e.Status);
            isError = true;
        }
        catch (Exception e)
        {
            Debug.Log("\r The following exception was raised : " + e.Message);
            isError = true;
        }
        finally
        {
            isDone = true;
        }

    }

    /// <summary>
    /// Receives an url and returns the result
    /// </summary>
    /// <param name="_url"></param>
    /// <returns></returns>

    //TODO: Refactor this class if needed
    public void GET(string _url)
    {
        string content = string.Empty;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
        request.AutomaticDecompression = DecompressionMethods.GZip;
        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                content = reader.ReadToEnd();
            }

        }
        catch (WebException e)
        {
            isError = true;
        }
        catch(Exception e)
        {
            isError = true;
        }

        ContentResponse =  content;
        isDone = true;
    }
}

