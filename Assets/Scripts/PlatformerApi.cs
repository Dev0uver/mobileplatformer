using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PlatformerApi : MonoBehaviour
{
    private static string url = "http://localhost:8080/api";
    public static string token = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ1c2VyMkBtYWlsLnJ1IiwiaWF0IjoxNzM0MTkzNjkyLCJleHAiOjE3MzY3ODU2OTJ9.u7uJn8AQce6IJTFkR70QRspjzc7LuNZ1iD4Tk8IXq9nUkUNQqkF2UTr08j5q5Me8dQ36hQ59LjnFFSKJWf9OIQ";

    /// <summary>
    /// Асинхронный метод для получения списка записей
    /// </summary>
    public async Task<RecordStruct[]> GetRecordsAsync(int level)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url + "/record?level=" + level))
        {
            request.SetRequestHeader("Authorization", "Bearer " + token);

            await SendRequestAsync(request);

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = "{\"records\":" + request.downloadHandler.text + "}";
                ResponseStruct response = JsonUtility.FromJson<ResponseStruct>(json);
                return response.records;
            }
            else
            {
                throw new System.Exception($"Failed to get records: {request.error}");
            }
        }
    }

    /// <summary>
    /// Асинхронный метод для отправки записи
    /// </summary>
    public async Task<RecordStruct> SaveRecordAsync(RecordStruct record)
    {
        string json = JsonUtility.ToJson(record);

        using (UnityWebRequest request = new UnityWebRequest(url + "/record", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + token);
            request.SetRequestHeader("Content-Type", "application/json");

            await SendRequestAsync(request);

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<RecordStruct>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogWarning($"Failed to send record: {request.error}");
                throw new Exception("Exception");
            }
        }
    }

    public async Task<TokenRequest> RegisterAsync(RegisterRequest register)
    {
        string json = JsonUtility.ToJson(register);

        using (UnityWebRequest request = new UnityWebRequest(url + "/security/register", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await SendRequestAsync(request);

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<TokenRequest>(request.downloadHandler.text);
            }
            else
            {
                throw new Exception($"Failed to send record: {request.error}");
            }
        }
    }

    public async Task<TokenRequest> AuthAsync(AuthRequest auth)
    {
        string json = JsonUtility.ToJson(auth);

        using (UnityWebRequest request = new UnityWebRequest(url + "/security/login", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await SendRequestAsync(request);

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<TokenRequest>(request.downloadHandler.text);
            }
            else
            {
                throw new Exception($"Failed to send record: {request.error}");
            }
        }
    }

    /// <summary>
    /// Асинхронная обертка для UnityWebRequest
    /// </summary>
    public Task SendRequestAsync(UnityWebRequest request)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();

        var operation = request.SendWebRequest();
        operation.completed += _ =>
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                taskCompletionSource.TrySetResult(true);
            }
            else
            {
                taskCompletionSource.TrySetException(new System.Exception(request.error));
            }
        };

        return taskCompletionSource.Task;
    }
}
